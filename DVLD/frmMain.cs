using DVLD.Applications;
using DVLD.Applications.Detain_License;
using DVLD.Applications.International_License;
using DVLD.Applications.ReplaceLostOrDamagedLicense;
using DVLD.Applications.Rlease_Detained_License;
using DVLD.Classes;
using DVLD.Drivers;
using DVLD.Licenses;
using DVLD.Licenses.International_License;
using DVLD.Login;
using DVLD.People;
using DVLD.Tests;
using DVLD.User;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Guna.UI2.WinForms;

namespace DVLD
{

    public partial class frmMain : Form
    {
        frmLogin _frmLogin;
        bool _IsSignOut = false;

        private void ActiveButton(Guna2Button btn)
        {
            btn.ForeColor = Color.DarkOrange;
            btn.FillColor = Color.FromArgb(50, 50, 100);
        }
        private void InActiveSidePanelButton()
        {
            foreach(var control in pnlSidePanel.Controls)
            {
                if(control is Guna2Button button)
                {
                    button.ForeColor = Color.White;
                    button.FillColor = Color.Transparent;
                }
            }
        }
        private void InActiveApplicationsPanelButton()
        {
            foreach (var control in pnlApplications.Controls)
            {
                if (control is Guna2Button button)
                {
                    button.ForeColor = Color.White;
                    button.FillColor = Color.Transparent;
                }
            }
        }
        private void InActiveAccSettingPanelButton()
        {
            foreach (var control in pnlAccSettings.Controls)
            {
                if (control is Guna2Button button)
                {
                    button.ForeColor = Color.White;
                    button.FillColor = Color.Transparent;
                }
            }
        }
        private void InActiveServicesPanelButton()
        {
            foreach (var control in pnlServices.Controls)
            {
                if (control is Guna2Button button)
                {
                    button.ForeColor = Color.White;
                    button.FillColor = Color.Transparent;
                }
            }
        }
        private void InActiveNewDrivingLicensePanelButton()
        {
            foreach (var control in pnlNewDrivingLicense.Controls)
            {
                if (control is Guna2Button button)
                {
                    button.ForeColor = Color.White;
                    button.FillColor = Color.Transparent;
                }
            }
        }
        private void InActiveManageApplicationsPanelButton()
        {
            foreach (var control in pnlManageApplications.Controls)
            {
                if (control is Guna2Button button)
                {
                    button.ForeColor = Color.White;
                    button.FillColor = Color.Transparent;
                }
            }
        }
        private void InActiveDetainLicensesPanelButton()
        {
            foreach (var control in pnlDetainLicenses.Controls)
            {
                if (control is Guna2Button button)
                {
                    button.ForeColor = Color.White;
                    button.FillColor = Color.Transparent;
                }
            }
        }
        private void HideAllSubPanels()
        {
            pnlNewDrivingLicense.Visible = false;
            pnlManageApplications.Visible = false;
            pnlServices.Visible = false;
            pnlDetainLicenses.Visible = false;
            pnlApplications.Visible = false;
            pnlAccSettings.Visible = false;
        }
        private void InActiveAllSubPanelsButtons()
        {
            InActiveApplicationsPanelButton();
            InActiveAccSettingPanelButton();
            InActiveServicesPanelButton();
            InActiveNewDrivingLicensePanelButton();
            InActiveManageApplicationsPanelButton();
            InActiveDetainLicensesPanelButton();
        }
    

        public frmMain( frmLogin frm )
        {
            InitializeComponent();
            _frmLogin= frm;

        }

        private void btnLocalLicense_Click(object sender, EventArgs e)
        {
            InActiveNewDrivingLicensePanelButton();
            ActiveButton(btnLocalLicense);
            frmAddUpdateLocalDrivingLicesnseApplication frm = new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
        }

        private void btnPeople_Click(object sender, EventArgs e)
        {
            HideAllSubPanels();
            InActiveAllSubPanelsButtons();

            InActiveSidePanelButton();
            ActiveButton(btnPeople);
            Form frm = new frmListPeople();
            frm.ShowDialog();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            HideAllSubPanels();
            InActiveAllSubPanelsButtons();

            InActiveSidePanelButton();
            ActiveButton(btnUsers);
            Form frm = new frmListUsers();
            frm.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblLoggedInUser.Text = "LoggedIn User: " + clsGlobal.CurrentUser.UserName;
            lblLoggedInUser.Visible = true;
            lblLoggedInUser.Size = new Size(100, 100);
            lblCurrentUser.Text = clsGlobal.CurrentUser.UserName;
            this.Refresh();
            this.Text = "Driving License Management System";
        }

        private void btnCurrentUserInfo_Click(object sender, EventArgs e)
        {
            InActiveAccSettingPanelButton();
            ActiveButton(btnCurrentUser);
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();

        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            HideAllSubPanels();
            ActiveButton(btnSignOut);
            clsGlobal.CurrentUser = null;
            _IsSignOut = true;
            this.Close();
            _frmLogin.Show();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            InActiveAccSettingPanelButton();
            ActiveButton(btnChangePassword);
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();

        }

        private void btnManageAppTypes_Click(object sender, EventArgs e)
        {
            pnlServices.Visible = false;
            InActiveServicesPanelButton();
            pnlNewDrivingLicense.Visible = false;
            InActiveNewDrivingLicensePanelButton();
            pnlManageApplications.Visible = false;
            InActiveManageApplicationsPanelButton();
            pnlDetainLicenses.Visible = false;
            InActiveDetainLicensesPanelButton();

            InActiveApplicationsPanelButton();
            ActiveButton(btnManageAppTypes);
            frmListApplicationTypes frm = new frmListApplicationTypes();
            frm.ShowDialog();
        }

        private void btnManageTestTypes_Click(object sender, EventArgs e)
        {
            pnlServices.Visible = false;
            InActiveServicesPanelButton();
            pnlNewDrivingLicense.Visible = false;
            InActiveNewDrivingLicensePanelButton();
            pnlManageApplications.Visible = false;
            InActiveManageApplicationsPanelButton();
            pnlDetainLicenses.Visible = false;
            InActiveDetainLicensesPanelButton();

            InActiveApplicationsPanelButton();
            ActiveButton(btnManageTestTypes);
            frmListTestTypes frm = new frmListTestTypes();
            frm.ShowDialog();
        }

        private void btnInternationalLicense_Click(object sender, EventArgs e)
        {
            InActiveNewDrivingLicensePanelButton();
            ActiveButton(btnInternationalLicense);
            frmNewInternationalLicenseApplication frm = new frmNewInternationalLicenseApplication();
            frm.ShowDialog();

        }

        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            pnlNewDrivingLicense.Visible = false;
            InActiveNewDrivingLicensePanelButton();
            InActiveServicesPanelButton();
            ActiveButton(btnRenewDrivingLicense);
            frmRenewLocalDrivingLicenseApplication frm = new frmRenewLocalDrivingLicenseApplication();
            frm.ShowDialog();

        }

       

        private void btnReleaseDetainedDrivingLicense_Click(object sender, EventArgs e)
        {
            pnlNewDrivingLicense.Visible = false;
            InActiveNewDrivingLicensePanelButton();

            InActiveServicesPanelButton();
            ActiveButton(btnReleaseDetainedDrivingLicense);
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();
            frm.ShowDialog();
        }

        private void btnRetakeTest_Click(object sender, EventArgs e)
        {
            pnlNewDrivingLicense.Visible = false;
            InActiveNewDrivingLicensePanelButton();

            InActiveServicesPanelButton();
            ActiveButton(btnRetakeTest);
            frmListLocalDrivingLicesnseApplications frm = new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();
        }


        private void vehiclesLicensesServicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnManageLDLApplications_Click(object sender, EventArgs e)
        {
            InActiveManageApplicationsPanelButton();
            ActiveButton(btnLocalLicenseApplications);
            frmListLocalDrivingLicesnseApplications frm = new frmListLocalDrivingLicesnseApplications();
            frm.ShowDialog();

        }

        private void btnDrivers_Click(object sender, EventArgs e)
        {
            HideAllSubPanels();
            InActiveAllSubPanelsButtons();

            InActiveSidePanelButton();
            ActiveButton(btnDrivers);
            frmListDrivers frm = new frmListDrivers();
            frm.ShowDialog();

        }

      

        private void btnManageInternationalLicenses_Click(object sender, EventArgs e)
        {
            InActiveManageApplicationsPanelButton();
            ActiveButton(btnInternationalLicenseApplications);
            frmListInternationalLicesnseApplications frm = new frmListInternationalLicesnseApplications();
            frm.ShowDialog();

        }

        private void btnReplacementLostOrDamagedDrivingLicense_Click(object sender, EventArgs e)
        {
            pnlNewDrivingLicense.Visible = false;
            InActiveNewDrivingLicensePanelButton();

            InActiveServicesPanelButton();
            ActiveButton(btnReplacementForLostOrDamaged);
            frmReplaceLostOrDamagedLicenseApplication frm = new frmReplaceLostOrDamagedLicenseApplication();
            frm.ShowDialog();

        }

        private void btnManageDetainedLicenses_Click(object sender, EventArgs e)
        {
            InActiveDetainLicensesPanelButton();
            ActiveButton(btnManageDetainedLicenses);
            frmListDetainedLicenses frm = new frmListDetainedLicenses();
            frm.ShowDialog();

        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            InActiveDetainLicensesPanelButton();
            ActiveButton(btnDetainLicense);
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();

        }

        private void btnReleaseDetainLicense_Click(object sender, EventArgs e)
        {
            InActiveDetainLicensesPanelButton();
            ActiveButton(btnReleaseDetainedLicense);
            frmReleaseDetainedLicenseApplication frm = new frmReleaseDetainedLicenseApplication();   
            frm.ShowDialog();

        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {

            using (LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle,
                                                               Color.FromArgb(24, 30, 54), // اللون العلوي
                                                               Color.FromArgb(46, 51, 73), // اللون السفلي
                                                               45F)) // زاوية الميل
            {
            e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

        }

        private void btnAccSettings_Click(object sender, EventArgs e)
        {
            HideAllSubPanels();
            InActiveAllSubPanelsButtons();

            pnlAccSettings.Visible = true;
            InActiveSidePanelButton();
            ActiveButton(btnAccSettings);
        }

        private void btnApplications_Click(object sender, EventArgs e)
        {
            HideAllSubPanels();
            InActiveAllSubPanelsButtons();
            pnlApplications.Visible = true;
            InActiveSidePanelButton();
            ActiveButton(btnApplications);
        }

        private void btnNewDrivingLicense_Click(object sender, EventArgs e)
        {
            InActiveServicesPanelButton();
            ActiveButton(btnNewDrivingLicense);
            pnlNewDrivingLicense.Visible = true;
        }

        private void btnServices_Click(object sender, EventArgs e)
        {
            InActiveApplicationsPanelButton();
            ActiveButton(btnServices);
            pnlManageApplications.Visible = false;
            pnlDetainLicenses.Visible = false;
            InActiveManageApplicationsPanelButton();
            InActiveDetainLicensesPanelButton();
            pnlServices.Visible = true;
        }

        private void btnLicenses_Click(object sender, EventArgs e)
        {
            HideAllSubPanels();
            InActiveAllSubPanelsButtons();

            InActiveSidePanelButton();
            ActiveButton(btnLicenses);

            frmListLicenses frm = new frmListLicenses();
            frm.ShowDialog();
        }

        private void btnTests_Click(object sender, EventArgs e)
        {
            HideAllSubPanels();
            InActiveAllSubPanelsButtons();
            InActiveSidePanelButton();
            ActiveButton(btnTests);

            frmListTests frm = new frmListTests();
            frm.ShowDialog();
        }

        private void btnManageApps_Click(object sender, EventArgs e)
        {
            InActiveApplicationsPanelButton();
            ActiveButton(btnManageApps);
            pnlDetainLicenses.Visible = false;
            InActiveDetainLicensesPanelButton();
            pnlServices.Visible = false;
            InActiveServicesPanelButton();
            pnlNewDrivingLicense.Visible=false;
            InActiveNewDrivingLicensePanelButton();
            pnlManageApplications.Visible = true;
        }

        private void btnDetainLicenses_Click(object sender, EventArgs e)
        {
            InActiveApplicationsPanelButton();
            ActiveButton(btnDetainLicenses);
            pnlServices.Visible=false;
            InActiveServicesPanelButton();
            pnlNewDrivingLicense.Visible = false;
            InActiveNewDrivingLicensePanelButton();
            pnlManageApplications.Visible=false;
            InActiveManageApplicationsPanelButton();
            pnlDetainLicenses.Visible=true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDate.Text = DateTime.Now.ToString("dddd, MMMM dd, yyyy");
        }

        private void frmMain_Click(object sender, EventArgs e)
        {
            HideAllSubPanels();
            InActiveAllSubPanelsButtons();
            InActiveSidePanelButton();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!_IsSignOut)
            {
                Environment.Exit(0);
            }
        }
    }
}
