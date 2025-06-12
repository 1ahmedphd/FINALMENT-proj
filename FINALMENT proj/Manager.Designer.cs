namespace FINALMENT_proj
{
    partial class ManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtManagerResponse = new System.Windows.Forms.TextBox();
            this.btnRespond = new System.Windows.Forms.Button();
            this.rbApprove = new System.Windows.Forms.RadioButton();
            this.rbReject = new System.Windows.Forms.RadioButton();
            this.btnUpdateRefund = new System.Windows.Forms.Button();
            this.lblResponse = new System.Windows.Forms.Label();
            this.tabControlManager = new System.Windows.Forms.TabControl();
            this.tabPageFeedback = new System.Windows.Forms.TabPage();
            this.dataGridViewFeedback = new System.Windows.Forms.DataGridView();
            this.feedbackIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usernameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.feedbackTextDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.managerResponseDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isRespondedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.createdAtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.feedbackBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.iOOPGADataSet = new FINALMENT_proj.IOOPGADataSet();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.rbUnresponded = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbResponded = new System.Windows.Forms.RadioButton();
            this.tabPageRefund = new System.Windows.Forms.TabPage();
            this.dataGridViewRefunds = new System.Windows.Forms.DataGridView();
            this.requestIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usernameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reasonDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reviewedAtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.refundRequestsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnRefresh2 = new System.Windows.Forms.Button();
            this.gbFilter2 = new System.Windows.Forms.GroupBox();
            this.rbAll2 = new System.Windows.Forms.RadioButton();
            this.rbPending = new System.Windows.Forms.RadioButton();
            this.rbRejected = new System.Windows.Forms.RadioButton();
            this.rbApproved = new System.Windows.Forms.RadioButton();
            this.tabPageTopUp = new System.Windows.Forms.TabPage();
            this.lbUsername = new System.Windows.Forms.ListBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblName2 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.btnTopUp = new System.Windows.Forms.Button();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.tabPageManagerProfile = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.feedbackTableAdapter = new FINALMENT_proj.IOOPGADataSetTableAdapters.FeedbackTableAdapter();
            this.refundRequestsTableAdapter = new FINALMENT_proj.IOOPGADataSetTableAdapters.RefundRequestsTableAdapter();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControlManager.SuspendLayout();
            this.tabPageFeedback.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFeedback)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.feedbackBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iOOPGADataSet)).BeginInit();
            this.gbFilter.SuspendLayout();
            this.tabPageRefund.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRefunds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.refundRequestsBindingSource)).BeginInit();
            this.gbFilter2.SuspendLayout();
            this.tabPageTopUp.SuspendLayout();
            this.tabPageManagerProfile.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtManagerResponse
            // 
            this.txtManagerResponse.BackColor = System.Drawing.SystemColors.Info;
            this.txtManagerResponse.Location = new System.Drawing.Point(366, 634);
            this.txtManagerResponse.Margin = new System.Windows.Forms.Padding(6);
            this.txtManagerResponse.Multiline = true;
            this.txtManagerResponse.Name = "txtManagerResponse";
            this.txtManagerResponse.Size = new System.Drawing.Size(994, 136);
            this.txtManagerResponse.TabIndex = 1;
            // 
            // btnRespond
            // 
            this.btnRespond.Font = new System.Drawing.Font("Impact", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRespond.Location = new System.Drawing.Point(1394, 634);
            this.btnRespond.Margin = new System.Windows.Forms.Padding(6);
            this.btnRespond.Name = "btnRespond";
            this.btnRespond.Size = new System.Drawing.Size(292, 105);
            this.btnRespond.TabIndex = 2;
            this.btnRespond.Text = "Submit";
            this.btnRespond.UseVisualStyleBackColor = true;
            this.btnRespond.Click += new System.EventHandler(this.btnRespond_Click);
            // 
            // rbApprove
            // 
            this.rbApprove.AutoSize = true;
            this.rbApprove.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbApprove.Location = new System.Drawing.Point(16, 634);
            this.rbApprove.Margin = new System.Windows.Forms.Padding(6);
            this.rbApprove.Name = "rbApprove";
            this.rbApprove.Size = new System.Drawing.Size(206, 46);
            this.rbApprove.TabIndex = 7;
            this.rbApprove.Text = "Approve";
            this.rbApprove.UseVisualStyleBackColor = true;
            // 
            // rbReject
            // 
            this.rbReject.AutoSize = true;
            this.rbReject.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbReject.Location = new System.Drawing.Point(244, 634);
            this.rbReject.Margin = new System.Windows.Forms.Padding(6);
            this.rbReject.Name = "rbReject";
            this.rbReject.Size = new System.Drawing.Size(167, 46);
            this.rbReject.TabIndex = 8;
            this.rbReject.Text = "Reject";
            this.rbReject.UseVisualStyleBackColor = true;
            // 
            // btnUpdateRefund
            // 
            this.btnUpdateRefund.Font = new System.Drawing.Font("Impact", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateRefund.Location = new System.Drawing.Point(450, 634);
            this.btnUpdateRefund.Margin = new System.Windows.Forms.Padding(6);
            this.btnUpdateRefund.Name = "btnUpdateRefund";
            this.btnUpdateRefund.Size = new System.Drawing.Size(292, 105);
            this.btnUpdateRefund.TabIndex = 9;
            this.btnUpdateRefund.Text = "Submit";
            this.btnUpdateRefund.UseVisualStyleBackColor = true;
            this.btnUpdateRefund.Click += new System.EventHandler(this.btnUpdateRefund_Click);
            // 
            // lblResponse
            // 
            this.lblResponse.AutoSize = true;
            this.lblResponse.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResponse.Location = new System.Drawing.Point(18, 634);
            this.lblResponse.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblResponse.Name = "lblResponse";
            this.lblResponse.Size = new System.Drawing.Size(300, 42);
            this.lblResponse.TabIndex = 10;
            this.lblResponse.Text = "Type Response:";
            // 
            // tabControlManager
            // 
            this.tabControlManager.Controls.Add(this.tabPageFeedback);
            this.tabControlManager.Controls.Add(this.tabPageRefund);
            this.tabControlManager.Controls.Add(this.tabPageTopUp);
            this.tabControlManager.Controls.Add(this.tabPageManagerProfile);
            this.tabControlManager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlManager.Location = new System.Drawing.Point(0, 0);
            this.tabControlManager.Margin = new System.Windows.Forms.Padding(6);
            this.tabControlManager.Name = "tabControlManager";
            this.tabControlManager.SelectedIndex = 0;
            this.tabControlManager.Size = new System.Drawing.Size(2663, 944);
            this.tabControlManager.TabIndex = 11;
            // 
            // tabPageFeedback
            // 
            this.tabPageFeedback.BackColor = System.Drawing.Color.Gold;
            this.tabPageFeedback.Controls.Add(this.dataGridViewFeedback);
            this.tabPageFeedback.Controls.Add(this.btnRefresh);
            this.tabPageFeedback.Controls.Add(this.gbFilter);
            this.tabPageFeedback.Controls.Add(this.lblResponse);
            this.tabPageFeedback.Controls.Add(this.txtManagerResponse);
            this.tabPageFeedback.Controls.Add(this.btnRespond);
            this.tabPageFeedback.Location = new System.Drawing.Point(10, 48);
            this.tabPageFeedback.Margin = new System.Windows.Forms.Padding(6);
            this.tabPageFeedback.Name = "tabPageFeedback";
            this.tabPageFeedback.Padding = new System.Windows.Forms.Padding(6);
            this.tabPageFeedback.Size = new System.Drawing.Size(2643, 886);
            this.tabPageFeedback.TabIndex = 0;
            this.tabPageFeedback.Text = "Feedback";
            // 
            // dataGridViewFeedback
            // 
            this.dataGridViewFeedback.AllowUserToResizeColumns = false;
            this.dataGridViewFeedback.AllowUserToResizeRows = false;
            this.dataGridViewFeedback.AutoGenerateColumns = false;
            this.dataGridViewFeedback.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewFeedback.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dataGridViewFeedback.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFeedback.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.feedbackIDDataGridViewTextBoxColumn,
            this.usernameDataGridViewTextBoxColumn,
            this.feedbackTextDataGridViewTextBoxColumn,
            this.managerResponseDataGridViewTextBoxColumn,
            this.isRespondedDataGridViewCheckBoxColumn,
            this.createdAtDataGridViewTextBoxColumn});
            this.dataGridViewFeedback.DataSource = this.feedbackBindingSource;
            this.dataGridViewFeedback.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridViewFeedback.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewFeedback.Margin = new System.Windows.Forms.Padding(6);
            this.dataGridViewFeedback.Name = "dataGridViewFeedback";
            this.dataGridViewFeedback.ReadOnly = true;
            this.dataGridViewFeedback.RowHeadersWidth = 51;
            this.dataGridViewFeedback.RowTemplate.Height = 24;
            this.dataGridViewFeedback.Size = new System.Drawing.Size(2631, 616);
            this.dataGridViewFeedback.TabIndex = 16;
            this.dataGridViewFeedback.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewFeedback_RowHeaderMouseDoubleClick);
            // 
            // feedbackIDDataGridViewTextBoxColumn
            // 
            this.feedbackIDDataGridViewTextBoxColumn.DataPropertyName = "FeedbackID";
            this.feedbackIDDataGridViewTextBoxColumn.HeaderText = "FeedbackID";
            this.feedbackIDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.feedbackIDDataGridViewTextBoxColumn.Name = "feedbackIDDataGridViewTextBoxColumn";
            this.feedbackIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // usernameDataGridViewTextBoxColumn
            // 
            this.usernameDataGridViewTextBoxColumn.DataPropertyName = "username";
            this.usernameDataGridViewTextBoxColumn.HeaderText = "username";
            this.usernameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.usernameDataGridViewTextBoxColumn.Name = "usernameDataGridViewTextBoxColumn";
            this.usernameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // feedbackTextDataGridViewTextBoxColumn
            // 
            this.feedbackTextDataGridViewTextBoxColumn.DataPropertyName = "FeedbackText";
            this.feedbackTextDataGridViewTextBoxColumn.HeaderText = "FeedbackText";
            this.feedbackTextDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.feedbackTextDataGridViewTextBoxColumn.Name = "feedbackTextDataGridViewTextBoxColumn";
            this.feedbackTextDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // managerResponseDataGridViewTextBoxColumn
            // 
            this.managerResponseDataGridViewTextBoxColumn.DataPropertyName = "ManagerResponse";
            this.managerResponseDataGridViewTextBoxColumn.HeaderText = "ManagerResponse";
            this.managerResponseDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.managerResponseDataGridViewTextBoxColumn.Name = "managerResponseDataGridViewTextBoxColumn";
            this.managerResponseDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isRespondedDataGridViewCheckBoxColumn
            // 
            this.isRespondedDataGridViewCheckBoxColumn.DataPropertyName = "IsResponded";
            this.isRespondedDataGridViewCheckBoxColumn.HeaderText = "IsResponded";
            this.isRespondedDataGridViewCheckBoxColumn.MinimumWidth = 6;
            this.isRespondedDataGridViewCheckBoxColumn.Name = "isRespondedDataGridViewCheckBoxColumn";
            this.isRespondedDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // createdAtDataGridViewTextBoxColumn
            // 
            this.createdAtDataGridViewTextBoxColumn.DataPropertyName = "CreatedAt";
            this.createdAtDataGridViewTextBoxColumn.HeaderText = "CreatedAt";
            this.createdAtDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.createdAtDataGridViewTextBoxColumn.Name = "createdAtDataGridViewTextBoxColumn";
            this.createdAtDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // feedbackBindingSource
            // 
            this.feedbackBindingSource.DataMember = "Feedback";
            this.feedbackBindingSource.DataSource = this.iOOPGADataSet;
            // 
            // iOOPGADataSet
            // 
            this.iOOPGADataSet.DataSetName = "IOOPGADataSet";
            this.iOOPGADataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Impact", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(1698, 634);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(6);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(292, 105);
            this.btnRefresh.TabIndex = 15;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.rbUnresponded);
            this.gbFilter.Controls.Add(this.rbAll);
            this.gbFilter.Controls.Add(this.rbResponded);
            this.gbFilter.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.Location = new System.Drawing.Point(2182, 626);
            this.gbFilter.Margin = new System.Windows.Forms.Padding(6);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Padding = new System.Windows.Forms.Padding(6);
            this.gbFilter.Size = new System.Drawing.Size(294, 178);
            this.gbFilter.TabIndex = 14;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter";
            // 
            // rbUnresponded
            // 
            this.rbUnresponded.AutoSize = true;
            this.rbUnresponded.Checked = true;
            this.rbUnresponded.Location = new System.Drawing.Point(12, 37);
            this.rbUnresponded.Margin = new System.Windows.Forms.Padding(6);
            this.rbUnresponded.Name = "rbUnresponded";
            this.rbUnresponded.Size = new System.Drawing.Size(217, 34);
            this.rbUnresponded.TabIndex = 11;
            this.rbUnresponded.TabStop = true;
            this.rbUnresponded.Text = "Unresponded";
            this.rbUnresponded.UseVisualStyleBackColor = true;
            this.rbUnresponded.CheckedChanged += new System.EventHandler(this.rbUnresponded_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Location = new System.Drawing.Point(12, 120);
            this.rbAll.Margin = new System.Windows.Forms.Padding(6);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(85, 34);
            this.rbAll.TabIndex = 13;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.rbAll_CheckedChanged);
            // 
            // rbResponded
            // 
            this.rbResponded.AutoSize = true;
            this.rbResponded.Location = new System.Drawing.Point(12, 78);
            this.rbResponded.Margin = new System.Windows.Forms.Padding(6);
            this.rbResponded.Name = "rbResponded";
            this.rbResponded.Size = new System.Drawing.Size(191, 34);
            this.rbResponded.TabIndex = 12;
            this.rbResponded.Text = "Responded";
            this.rbResponded.UseVisualStyleBackColor = true;
            this.rbResponded.CheckedChanged += new System.EventHandler(this.rbResponded_CheckedChanged);
            // 
            // tabPageRefund
            // 
            this.tabPageRefund.BackColor = System.Drawing.Color.Gold;
            this.tabPageRefund.Controls.Add(this.dataGridViewRefunds);
            this.tabPageRefund.Controls.Add(this.btnRefresh2);
            this.tabPageRefund.Controls.Add(this.gbFilter2);
            this.tabPageRefund.Controls.Add(this.btnUpdateRefund);
            this.tabPageRefund.Controls.Add(this.rbReject);
            this.tabPageRefund.Controls.Add(this.rbApprove);
            this.tabPageRefund.Location = new System.Drawing.Point(10, 48);
            this.tabPageRefund.Margin = new System.Windows.Forms.Padding(6);
            this.tabPageRefund.Name = "tabPageRefund";
            this.tabPageRefund.Padding = new System.Windows.Forms.Padding(6);
            this.tabPageRefund.Size = new System.Drawing.Size(2643, 886);
            this.tabPageRefund.TabIndex = 1;
            this.tabPageRefund.Text = "Refund";
            // 
            // dataGridViewRefunds
            // 
            this.dataGridViewRefunds.AllowUserToResizeColumns = false;
            this.dataGridViewRefunds.AllowUserToResizeRows = false;
            this.dataGridViewRefunds.AutoGenerateColumns = false;
            this.dataGridViewRefunds.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewRefunds.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dataGridViewRefunds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewRefunds.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.requestIDDataGridViewTextBoxColumn,
            this.usernameDataGridViewTextBoxColumn1,
            this.amountDataGridViewTextBoxColumn,
            this.reasonDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn,
            this.reviewedAtDataGridViewTextBoxColumn});
            this.dataGridViewRefunds.DataSource = this.refundRequestsBindingSource;
            this.dataGridViewRefunds.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridViewRefunds.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewRefunds.Margin = new System.Windows.Forms.Padding(6);
            this.dataGridViewRefunds.Name = "dataGridViewRefunds";
            this.dataGridViewRefunds.ReadOnly = true;
            this.dataGridViewRefunds.RowHeadersWidth = 51;
            this.dataGridViewRefunds.RowTemplate.Height = 24;
            this.dataGridViewRefunds.Size = new System.Drawing.Size(2631, 616);
            this.dataGridViewRefunds.TabIndex = 17;
            this.dataGridViewRefunds.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewRefunds_RowHeaderMouseDoubleClick);
            // 
            // requestIDDataGridViewTextBoxColumn
            // 
            this.requestIDDataGridViewTextBoxColumn.DataPropertyName = "RequestID";
            this.requestIDDataGridViewTextBoxColumn.HeaderText = "RequestID";
            this.requestIDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.requestIDDataGridViewTextBoxColumn.Name = "requestIDDataGridViewTextBoxColumn";
            this.requestIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // usernameDataGridViewTextBoxColumn1
            // 
            this.usernameDataGridViewTextBoxColumn1.DataPropertyName = "username";
            this.usernameDataGridViewTextBoxColumn1.HeaderText = "username";
            this.usernameDataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.usernameDataGridViewTextBoxColumn1.Name = "usernameDataGridViewTextBoxColumn1";
            this.usernameDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // amountDataGridViewTextBoxColumn
            // 
            this.amountDataGridViewTextBoxColumn.DataPropertyName = "Amount";
            this.amountDataGridViewTextBoxColumn.HeaderText = "Amount";
            this.amountDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.amountDataGridViewTextBoxColumn.Name = "amountDataGridViewTextBoxColumn";
            this.amountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // reasonDataGridViewTextBoxColumn
            // 
            this.reasonDataGridViewTextBoxColumn.DataPropertyName = "Reason";
            this.reasonDataGridViewTextBoxColumn.HeaderText = "Reason";
            this.reasonDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.reasonDataGridViewTextBoxColumn.Name = "reasonDataGridViewTextBoxColumn";
            this.reasonDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // reviewedAtDataGridViewTextBoxColumn
            // 
            this.reviewedAtDataGridViewTextBoxColumn.DataPropertyName = "ReviewedAt";
            this.reviewedAtDataGridViewTextBoxColumn.HeaderText = "ReviewedAt";
            this.reviewedAtDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.reviewedAtDataGridViewTextBoxColumn.Name = "reviewedAtDataGridViewTextBoxColumn";
            this.reviewedAtDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // refundRequestsBindingSource
            // 
            this.refundRequestsBindingSource.DataMember = "RefundRequests";
            this.refundRequestsBindingSource.DataSource = this.iOOPGADataSet;
            // 
            // btnRefresh2
            // 
            this.btnRefresh2.Font = new System.Drawing.Font("Impact", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh2.Location = new System.Drawing.Point(754, 634);
            this.btnRefresh2.Margin = new System.Windows.Forms.Padding(6);
            this.btnRefresh2.Name = "btnRefresh2";
            this.btnRefresh2.Size = new System.Drawing.Size(292, 105);
            this.btnRefresh2.TabIndex = 16;
            this.btnRefresh2.Text = "Refresh";
            this.btnRefresh2.UseVisualStyleBackColor = true;
            this.btnRefresh2.Click += new System.EventHandler(this.btnRefresh2_Click);
            // 
            // gbFilter2
            // 
            this.gbFilter2.Controls.Add(this.rbAll2);
            this.gbFilter2.Controls.Add(this.rbPending);
            this.gbFilter2.Controls.Add(this.rbRejected);
            this.gbFilter2.Controls.Add(this.rbApproved);
            this.gbFilter2.Font = new System.Drawing.Font("Arial", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter2.Location = new System.Drawing.Point(2062, 626);
            this.gbFilter2.Margin = new System.Windows.Forms.Padding(6);
            this.gbFilter2.Name = "gbFilter2";
            this.gbFilter2.Padding = new System.Windows.Forms.Padding(6);
            this.gbFilter2.Size = new System.Drawing.Size(414, 178);
            this.gbFilter2.TabIndex = 15;
            this.gbFilter2.TabStop = false;
            this.gbFilter2.Text = "Filter";
            // 
            // rbAll2
            // 
            this.rbAll2.AutoSize = true;
            this.rbAll2.Location = new System.Drawing.Point(12, 116);
            this.rbAll2.Margin = new System.Windows.Forms.Padding(6);
            this.rbAll2.Name = "rbAll2";
            this.rbAll2.Size = new System.Drawing.Size(85, 34);
            this.rbAll2.TabIndex = 14;
            this.rbAll2.TabStop = true;
            this.rbAll2.Text = "All";
            this.rbAll2.UseVisualStyleBackColor = true;
            this.rbAll2.CheckedChanged += new System.EventHandler(this.rbAll2_CheckedChanged);
            // 
            // rbPending
            // 
            this.rbPending.AutoSize = true;
            this.rbPending.Checked = true;
            this.rbPending.Location = new System.Drawing.Point(12, 48);
            this.rbPending.Margin = new System.Windows.Forms.Padding(6);
            this.rbPending.Name = "rbPending";
            this.rbPending.Size = new System.Drawing.Size(154, 34);
            this.rbPending.TabIndex = 11;
            this.rbPending.TabStop = true;
            this.rbPending.Text = "Pending";
            this.rbPending.UseVisualStyleBackColor = true;
            this.rbPending.CheckedChanged += new System.EventHandler(this.rbPending_CheckedChanged);
            // 
            // rbRejected
            // 
            this.rbRejected.AutoSize = true;
            this.rbRejected.Location = new System.Drawing.Point(198, 116);
            this.rbRejected.Margin = new System.Windows.Forms.Padding(6);
            this.rbRejected.Name = "rbRejected";
            this.rbRejected.Size = new System.Drawing.Size(158, 34);
            this.rbRejected.TabIndex = 13;
            this.rbRejected.Text = "Rejected";
            this.rbRejected.UseVisualStyleBackColor = true;
            this.rbRejected.CheckedChanged += new System.EventHandler(this.rbRejected_CheckedChanged);
            // 
            // rbApproved
            // 
            this.rbApproved.AutoSize = true;
            this.rbApproved.Location = new System.Drawing.Point(198, 48);
            this.rbApproved.Margin = new System.Windows.Forms.Padding(6);
            this.rbApproved.Name = "rbApproved";
            this.rbApproved.Size = new System.Drawing.Size(172, 34);
            this.rbApproved.TabIndex = 12;
            this.rbApproved.Text = "Approved";
            this.rbApproved.UseVisualStyleBackColor = true;
            this.rbApproved.CheckedChanged += new System.EventHandler(this.rbApproved_CheckedChanged);
            // 
            // tabPageTopUp
            // 
            this.tabPageTopUp.BackColor = System.Drawing.Color.Gold;
            this.tabPageTopUp.Controls.Add(this.lbUsername);
            this.tabPageTopUp.Controls.Add(this.btnClear);
            this.tabPageTopUp.Controls.Add(this.lblName2);
            this.tabPageTopUp.Controls.Add(this.lblName);
            this.tabPageTopUp.Controls.Add(this.label1);
            this.tabPageTopUp.Controls.Add(this.lblUsername);
            this.tabPageTopUp.Controls.Add(this.btnTopUp);
            this.tabPageTopUp.Controls.Add(this.txtAmount);
            this.tabPageTopUp.Location = new System.Drawing.Point(10, 48);
            this.tabPageTopUp.Margin = new System.Windows.Forms.Padding(6);
            this.tabPageTopUp.Name = "tabPageTopUp";
            this.tabPageTopUp.Padding = new System.Windows.Forms.Padding(6);
            this.tabPageTopUp.Size = new System.Drawing.Size(2643, 886);
            this.tabPageTopUp.TabIndex = 2;
            this.tabPageTopUp.Text = "Top-Up";
            // 
            // lbUsername
            // 
            this.lbUsername.BackColor = System.Drawing.SystemColors.Info;
            this.lbUsername.FormattingEnabled = true;
            this.lbUsername.ItemHeight = 31;
            this.lbUsername.Location = new System.Drawing.Point(470, 157);
            this.lbUsername.Margin = new System.Windows.Forms.Padding(6);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(460, 469);
            this.lbUsername.TabIndex = 14;
            this.lbUsername.SelectedIndexChanged += new System.EventHandler(this.lbUsername_SelectedIndexChanged);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Impact", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(1772, 515);
            this.btnClear.Margin = new System.Windows.Forms.Padding(6);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(286, 114);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblName2
            // 
            this.lblName2.AutoSize = true;
            this.lblName2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName2.Location = new System.Drawing.Point(1624, 157);
            this.lblName2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblName2.Name = "lblName2";
            this.lblName2.Size = new System.Drawing.Size(87, 45);
            this.lblName2.TabIndex = 12;
            this.lblName2.Text = "N/A";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(1364, 161);
            this.lblName.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(130, 42);
            this.lblName.TabIndex = 11;
            this.lblName.Text = "Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1194, 337);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 42);
            this.label1.TabIndex = 9;
            this.label1.Text = "Top-Up Amount:";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.Location = new System.Drawing.Point(216, 157);
            this.lblUsername.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(209, 42);
            this.lblUsername.TabIndex = 8;
            this.lblUsername.Text = "Username:";
            // 
            // btnTopUp
            // 
            this.btnTopUp.Font = new System.Drawing.Font("Impact", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTopUp.Location = new System.Drawing.Point(1312, 515);
            this.btnTopUp.Margin = new System.Windows.Forms.Padding(6);
            this.btnTopUp.Name = "btnTopUp";
            this.btnTopUp.Size = new System.Drawing.Size(286, 114);
            this.btnTopUp.TabIndex = 7;
            this.btnTopUp.Text = "Top Up";
            this.btnTopUp.UseVisualStyleBackColor = true;
            this.btnTopUp.Click += new System.EventHandler(this.btnTopUp_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.BackColor = System.Drawing.SystemColors.Info;
            this.txtAmount.Location = new System.Drawing.Point(1632, 339);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(6);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(536, 38);
            this.txtAmount.TabIndex = 6;
            // 
            // tabPageManagerProfile
            // 
            this.tabPageManagerProfile.Controls.Add(this.button1);
            this.tabPageManagerProfile.Controls.Add(this.groupBox2);
            this.tabPageManagerProfile.Controls.Add(this.groupBox1);
            this.tabPageManagerProfile.Location = new System.Drawing.Point(10, 48);
            this.tabPageManagerProfile.Margin = new System.Windows.Forms.Padding(6);
            this.tabPageManagerProfile.Name = "tabPageManagerProfile";
            this.tabPageManagerProfile.Padding = new System.Windows.Forms.Padding(6);
            this.tabPageManagerProfile.Size = new System.Drawing.Size(2643, 886);
            this.tabPageManagerProfile.TabIndex = 3;
            this.tabPageManagerProfile.Text = "Profile";
            this.tabPageManagerProfile.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox5);
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Controls.Add(this.button6);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.groupBox2.Location = new System.Drawing.Point(24, 489);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(2398, 295);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Change password";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(507, 209);
            this.textBox5.Name = "textBox5";
            this.textBox5.PasswordChar = '*';
            this.textBox5.Size = new System.Drawing.Size(541, 48);
            this.textBox5.TabIndex = 12;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(344, 135);
            this.textBox4.Name = "textBox4";
            this.textBox4.PasswordChar = '*';
            this.textBox4.Size = new System.Drawing.Size(541, 48);
            this.textBox4.TabIndex = 11;
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button6.Location = new System.Drawing.Point(1420, 135);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(321, 63);
            this.button6.TabIndex = 7;
            this.button6.Text = "Change password";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(332, 57);
            this.textBox3.Name = "textBox3";
            this.textBox3.PasswordChar = '*';
            this.textBox3.Size = new System.Drawing.Size(541, 48);
            this.textBox3.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(65, 209);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(413, 40);
            this.label10.TabIndex = 9;
            this.label10.Text = "Confirm New Password: ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(65, 135);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(273, 40);
            this.label9.TabIndex = 8;
            this.label9.Text = "New password: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(65, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(261, 40);
            this.label8.TabIndex = 7;
            this.label8.Text = "Old Password: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox7);
            this.groupBox1.Controls.Add(this.textBox6);
            this.groupBox1.Controls.Add(this.button5);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.richTextBox3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F);
            this.groupBox1.Location = new System.Drawing.Point(9, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(2470, 466);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Profile";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(291, 144);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(503, 48);
            this.textBox7.TabIndex = 14;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(221, 75);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(484, 48);
            this.textBox6.TabIndex = 13;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button5.Location = new System.Drawing.Point(2294, 258);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(130, 63);
            this.button5.TabIndex = 6;
            this.button5.Text = "Save";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button4.Location = new System.Drawing.Point(2294, 156);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(108, 63);
            this.button4.TabIndex = 5;
            this.button4.Text = "Edit";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(1002, 75);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.ReadOnly = true;
            this.richTextBox3.Size = new System.Drawing.Size(1257, 363);
            this.richTextBox3.TabIndex = 4;
            this.richTextBox3.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(907, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 40);
            this.label7.TabIndex = 2;
            this.label7.Text = "Bio: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(82, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(203, 40);
            this.label6.TabIndex = 1;
            this.label6.Text = "Username: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(82, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 40);
            this.label5.TabIndex = 0;
            this.label5.Text = "Name: ";
            // 
            // feedbackTableAdapter
            // 
            this.feedbackTableAdapter.ClearBeforeFill = true;
            // 
            // refundRequestsTableAdapter
            // 
            this.refundRequestsTableAdapter.ClearBeforeFill = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Location = new System.Drawing.Point(2303, 805);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(321, 63);
            this.button1.TabIndex = 13;
            this.button1.Text = "logout";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2663, 944);
            this.Controls.Add(this.tabControlManager);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ManagerForm";
            this.Text = "Manager";
            this.Load += new System.EventHandler(this.ManagerForm_Load);
            this.tabControlManager.ResumeLayout(false);
            this.tabPageFeedback.ResumeLayout(false);
            this.tabPageFeedback.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFeedback)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.feedbackBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iOOPGADataSet)).EndInit();
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.tabPageRefund.ResumeLayout(false);
            this.tabPageRefund.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewRefunds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.refundRequestsBindingSource)).EndInit();
            this.gbFilter2.ResumeLayout(false);
            this.gbFilter2.PerformLayout();
            this.tabPageTopUp.ResumeLayout(false);
            this.tabPageTopUp.PerformLayout();
            this.tabPageManagerProfile.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtManagerResponse;
        private System.Windows.Forms.Button btnRespond;
        private System.Windows.Forms.RadioButton rbApprove;
        private System.Windows.Forms.RadioButton rbReject;
        private System.Windows.Forms.Button btnUpdateRefund;
        private System.Windows.Forms.Label lblResponse;
        private System.Windows.Forms.TabControl tabControlManager;
        private System.Windows.Forms.TabPage tabPageFeedback;
        private System.Windows.Forms.TabPage tabPageRefund;
        private System.Windows.Forms.TabPage tabPageTopUp;
        private System.Windows.Forms.Button btnTopUp;
        private System.Windows.Forms.TabPage tabPageManagerProfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblName2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbResponded;
        private System.Windows.Forms.RadioButton rbUnresponded;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.GroupBox gbFilter2;
        private System.Windows.Forms.RadioButton rbPending;
        private System.Windows.Forms.RadioButton rbRejected;
        private System.Windows.Forms.RadioButton rbApproved;
        private System.Windows.Forms.ListBox lbUsername;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.RadioButton rbAll2;
        private System.Windows.Forms.Button btnRefresh2;
        private System.Windows.Forms.DataGridView dataGridViewFeedback;
        private System.Windows.Forms.DataGridView dataGridViewRefunds;
        private IOOPGADataSet iOOPGADataSet;
        private System.Windows.Forms.BindingSource feedbackBindingSource;
        private IOOPGADataSetTableAdapters.FeedbackTableAdapter feedbackTableAdapter;
        private System.Windows.Forms.BindingSource refundRequestsBindingSource;
        private IOOPGADataSetTableAdapters.RefundRequestsTableAdapter refundRequestsTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn username;
        private System.Windows.Forms.DataGridViewTextBoxColumn username1;
        private System.Windows.Forms.DataGridViewTextBoxColumn requestIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usernameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn reasonDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn reviewedAtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn feedbackIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn usernameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn feedbackTextDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn managerResponseDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isRespondedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdAtDataGridViewTextBoxColumn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
    }
}

