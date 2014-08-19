namespace AceSoft.RetailPlus.MasterFiles._Product
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using AceSoft.RetailPlus.Data;
	
	public partial  class __EasyInsertVariation : System.Web.UI.UserControl
	{
		#region Web Form Methods

		protected void Page_Load(object sender, System.EventArgs e)
		{
            //if (!IsPostBack)
            //{
            //    if (Visible)
            //    {
            //        //lblReferrer.Text = Request.UrlReferrer == null ? Constants.ROOT_DIRECTORY : Request.UrlReferrer.ToString();
            //        //LoadOptions();
            //    }
            //}
		}


		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            //this.imgSave.Click += new System.Web.UI.ImageClickEventHandler(this.imgSave_Click);
            //this.imgSaveBack.Click += new System.Web.UI.ImageClickEventHandler(this.imgSaveBack_Click);
            //this.imgCancel.Click += new System.Web.UI.ImageClickEventHandler(this.imgCancel_Click);

		}
		#endregion

		#region Web Control Methods

		protected void imgSave_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
            EasyAddSaveRecord();
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID) + "&prodid=" + Request.QueryString["prodid"].ToString();
			Response.Redirect("Default.aspx" + stParam);	
		}

		protected void cmdSave_Click(object sender, System.EventArgs e)
		{
            EasyAddSaveRecord();
			string stParam = "?task=" + Common.Encrypt("add",Session.SessionID) + "&prodid=" + Request.QueryString["prodid"].ToString();
			Response.Redirect("Default.aspx" + stParam);			
		}


        protected void imgEasyAddSaveBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
            EasyAddSaveRecord();
            Response.Redirect(lblReferrer.Text);
		}

        protected void cmdEasyAddSaveBack_Click(object sender, System.EventArgs e)
		{
            EasyAddSaveRecord();
            Response.Redirect(lblReferrer.Text);
		}


		protected void imgCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

		protected void cmdCancel_Click(object sender, System.EventArgs e)
		{
			Response.Redirect(lblReferrer.Text);
		}

		
		#endregion

		#region Private Methods

		public void LoadOptions()
		{
            DataClass clsDataClass = new DataClass();
            //lblProductID.Text = "0"; // Common.Decrypt((string)Request.QueryString["prodid"], Session.SessionID);

			ProductVariations clsVariation = new ProductVariations();
			
			cboVariationType.DataTextField = "VariationType";
			cboVariationType.DataValueField = "VariationID";
			cboVariationType.DataSource = clsDataClass.DataReaderToDataTable(clsVariation.AvailableVariations(Convert.ToInt32(lblProductID.Text), "VariationType",SortOption.Ascending)).DefaultView;
			cboVariationType.DataBind();
			cboVariationType.SelectedIndex = cboVariationType.Items.Count - 1;

			clsVariation.CommitAndDispose();	
		}

        private void EasyAddSaveRecord()
		{
			ProductVariationDetails clsDetails = new ProductVariationDetails();

			clsDetails.ProductID = Convert.ToInt64(lblProductID.Text);
			clsDetails.VariationID = Convert.ToInt32(cboVariationType.SelectedItem.Value);
			clsDetails.VariationType = cboVariationType.SelectedItem.Text;

			ProductVariations clsProdVariation = new ProductVariations();
			int id = clsProdVariation.Insert(clsDetails);
			clsProdVariation.CommitAndDispose();
		}


		#endregion
    }
}
