using System;
using System.Net;
using System.Configuration;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

using Nethereum.Util;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.ContractHandlers;

using RSKKMS.Lib.Security;
using RSKKMS.Lib.KeyManagement;
using System.Threading;

namespace RSKKMS.WinForms
{
    public partial class FrmMain : Form
    {
        private ContractHandler contractHandler;
        private X509Certificate2 filteredCert;

        private string thumbPrint = ConfigurationManager.AppSettings["Thumbprint"].ToUpper();
        private string privateKey = ConfigurationManager.AppSettings["PrivateKey"];
        private string contractDeploymentUrl = ConfigurationManager.AppSettings["ContractDeploymentUrl"].ToString();
   
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnDeploy_Click(object sender, System.EventArgs e)
        {
            StartProgressBar();

            string kmsContractAddress = DeployContract();
            if (!string.IsNullOrEmpty(kmsContractAddress))
                MessageBox.Show("The KMS Contract Deployment was successful!", "Info",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtKMSContractAddress.Text = kmsContractAddress;

            StopProgressBar();
        }

        private void LoadCertificate()
        {
            filteredCert = X509CertificateHelper.GetRSKCertificate(thumbPrint,
                StoreLocation.LocalMachine);

            if (filteredCert == null)
            {
                System.Console.WriteLine($"Unable to find the RSK certificate by Thumbprint: " +
                    $"{thumbPrint}");
            }
            else
            {
                MessageBox.Show("Certificate loaded successfully!", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string DeployContract()
        {
            try
            {
                Web3 web3 = GetWeb3();

                // Deploy Iterable Mapping Library
                TransactionReceipt transactionReceiptDeployment;
                string contractAddress;

                RSKContractHelper.DeployIterableMappingContract(web3,
                    out transactionReceiptDeployment,
                    out contractAddress,
                    out contractHandler);

                System.Console.WriteLine($"Iterable Mapping Contarct Address: {contractAddress}");
                System.Console.WriteLine("Deploying the RSK KMS Contract");

                // Deploy the RSK Contract
                contractHandler = RSKContractHelper.DeployRSKKeyManagmentContract(web3,
                    transactionReceiptDeployment,
                    out contractAddress);

                System.Console.WriteLine("Trying to set a value in RSK KMS Contract");

                return contractAddress;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return string.Empty;
        }

        private Web3 GetWeb3()
        {
            var account = new Nethereum.Web3.Accounts.Account(privateKey);
            Web3 web3 = new Web3(account, contractDeploymentUrl);
            return web3;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            progressBar1.Visible = false;
            btnKMSGetItem.Enabled = false;

            listView1.Columns.Add("Keyname", 100);
            listView1.Columns.Add("Encrypted Value", 200);
            listView1.Columns.Add("Decrypted Value", 200);

            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            btnDeploy.Enabled = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            txtCertificateThumbprint.Text = thumbPrint;

            LoadCertificate();
        }

        private void btnEtherAmount_Click(object sender, EventArgs e)
        {
            StartProgressBar();

            Web3 web3 = GetWeb3();

            var account = new Nethereum.Web3.Accounts.Account(privateKey);
            var weiBalance = AccountHelper.GetBalance(web3, account);
            var etherAmount = Web3.Convert.FromWei(weiBalance.Value);

            txtEtherAmount.Text = etherAmount.ToString();

            decimal balanceInDecimal = decimal.Parse(txtEtherAmount.Text.Trim());
            btnDeploy.Enabled = balanceInDecimal > 0;

            StopProgressBar();
        }

        private void StartProgressBar()
        {
            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 30;
        }

        private void StopProgressBar()
        {
            progressBar1.Visible = false;
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.MarqueeAnimationSpeed = 0;
        }

        private void btnLoadCertificate_Click(object sender, EventArgs e)
        {
            LoadCertificate();
        }

        private bool ValidateKMSSet()
        {
            bool validationResult = true;
            if (string.IsNullOrEmpty(txtKeyName.Text.Trim()))
            {
                MessageBox.Show("Keyname must exist!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtKeyName.Focus();
                validationResult = false;
            }
            else if (string.IsNullOrEmpty(txtValue.Text.Trim()))
            {
                MessageBox.Show("Key value must exist!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtValue.Focus();
                validationResult = false;
            }
            else if(filteredCert == null)
            {
                MessageBox.Show("Please load the certificate!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                validationResult = false;
            }
            return validationResult;
        }

        private async void btnKMSSetItem_Click(object sender, EventArgs e)
        {
            if (ValidateKMSSet() == false)
                return;

            if(contractHandler == null &&
                !string.IsNullOrEmpty(txtKMSContractAddress.Text.Trim()))
            {
                Web3 web3 = GetWeb3();
                contractHandler = web3.Eth.GetContractHandler(txtKMSContractAddress.Text.Trim());
            }

            if(contractHandler == null)
            {
                MessageBox.Show("Problem in getting an instance of the Contract Handler. " +
                    "Please try specifying a valid Contract Address", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            StartProgressBar();

            btnKMSSetItem.Enabled = false;
            var encryptedText = RSAEncryptionHelper.Encrypt(txtValue.Text.Trim(), filteredCert);
            var account = new Nethereum.Web3.Accounts.Account(privateKey);

            var setItemRequest = new SetItemFunction
            {
                Key = txtKeyName.Text.Trim(),
                Value = encryptedText,
                FromAddress = account.Address
            };

            // Set the Gas value
            var estimate = await contractHandler
                  .EstimateGasAsync(setItemRequest);

            setItemRequest.Gas = estimate.Value;

            var setItemFunctionTxnReceipt = await contractHandler
                .SendRequestAndWaitForReceiptAsync(setItemRequest);

            if(setItemFunctionTxnReceipt != null &&
                setItemFunctionTxnReceipt.BlockNumber.Value > 0)
            {
                string[] keyCollection = new string[3];
                ListViewItem listItem;
                keyCollection[0] = txtKeyName.Text.Trim();
                keyCollection[1] = encryptedText;
                keyCollection[2] = "";
                listItem = new ListViewItem(keyCollection);
                listView1.Items.Add(listItem);
            }

            btnKMSSetItem.Enabled = true;

            StopProgressBar();
        }

        private async void btnKMSGetItem_Click(object sender, EventArgs e)
        {
            var selectedItem = listView1.SelectedItems;

            if (string.IsNullOrEmpty(txtKMSContractAddress.Text.Trim()))
            {
                MessageBox.Show("Please try specifying a valid Contract Address", "Error",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StartProgressBar();

            if (selectedItem.Count > 0)
            {
                string keyName = selectedItem[0].Text.Trim();
                string getItemResponse = await GetKMSKeyValue(keyName);

                if (!string.IsNullOrEmpty(getItemResponse))
                {
                    var decryptedText = RSAEncryptionHelper.Decrypt(getItemResponse, filteredCert);
                    int index = listView1.SelectedIndices[0];
                    listView1.Items[index].SubItems[1].Text = getItemResponse;
                    listView1.Items[index].SubItems[2].Text = decryptedText;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(txtKeyName.Text.Trim()))
                {
                    MessageBox.Show("Please specify the Key name", "Error",
                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtKeyName.Focus();
                    return;
                }

                // Get the KMS value by the Key Name
                string encryptedText = await GetKMSKeyValue(txtKeyName.Text.Trim());
                var decryptedText = RSAEncryptionHelper.Decrypt(encryptedText, filteredCert);
                txtStoredKeyValue.Text = decryptedText;
            }

            StopProgressBar();
        }

        /// <summary>
        /// Get the KMS Keyvalue
        /// </summary>
        /// <param name="keyName">Key Name</param>
        /// <returns>Persisted Value</returns>
        private async Task<string> GetKMSKeyValue(string keyName)
        {
            Web3 web3 = GetWeb3();
            var account = new Nethereum.Web3.Accounts.Account(privateKey);

            /** Function: getItem**/
            var getItemRequest = new GetItemFunction
            {
                Key = keyName,
                FromAddress = account.Address,
                GasPrice = Web3.Convert.ToWei(25, UnitConversion.EthUnit.Gwei)
            };

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls | SecurityProtocolType.Ssl3;

            var keyQueryHandler = web3.Eth.GetContractQueryHandler<GetItemFunction>();

            var getItemResponse = await keyQueryHandler
                .QueryDeserializingToObjectAsync<GetItemOutputDTO>(
                 getItemRequest, txtKMSContractAddress.Text.Trim());

            if(getItemResponse != null)
             return getItemResponse.ReturnValue1;

            return string.Empty;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnKMSGetItem.Enabled = true;
        }

        private void txtKeyName_TextChanged(object sender, EventArgs e)
        {
            btnKMSGetItem.Enabled = true;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }
    }
}
