using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchedulingApplication_10_23_2018
{
    public partial class auditors : System.Web.UI.Page
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-3AT0919R\SQLEXPRESS;Initial Catalog=seniorProject;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) //avoids executing first Time
            {
            btnDelete.Enabled = false;
            FillGridView();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            hfAuditorID.Value = "";
            txtName.Text = txtLevel.Text = "";
            lblErrorMessage.Text = lblSuccessMessage.Text = "";
            btnSave.Text = "Save";
            btnDelete.Enabled = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCmd = new SqlCommand("AuditorCreateOrUpdate", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@AuditorID",(hfAuditorID.Value==""?0:Convert.ToInt32(hfAuditorID.Value)));
            sqlCmd.Parameters.AddWithValue("@AuditorName", txtName.Text.Trim());
            sqlCmd.Parameters.AddWithValue("@AuditorLevel", txtLevel.Text.Trim());
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            string contactID = hfAuditorID.Value;
            Clear();
            if (contactID == "")
                lblSuccessMessage.Text = "Saved Successfully";
            else
                lblSuccessMessage.Text = "Updated Successfully";

            FillGridView();
        }

        void FillGridView()
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlDataAdapter sqlDa = new SqlDataAdapter("AuditorViewAll", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            sqlCon.Close();
            gvAuditor.DataSource = dtbl;
            gvAuditor.DataBind();
        }

        protected void lnk_OnClick(object sender, EventArgs e)
        {
            int AuditorID = Convert.ToInt32((sender as LinkButton).CommandArgument);

            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlDataAdapter sqlDa = new SqlDataAdapter("AuditorViewByID", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@AuditorID", AuditorID);
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            sqlCon.Close();
            hfAuditorID.Value = AuditorID.ToString();
            txtName.Text = dtbl.Rows[0]["AuditorName"].ToString();
            txtLevel.Text = dtbl.Rows[0]["AuditorLevel"].ToString();
            btnSave.Text = "Update " + txtName.Text;
            btnDelete.Enabled = true;
            //newly added
            txtleveldemo.Text = rdLevel.SelectedItem.Value.ToString();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (sqlCon.State == ConnectionState.Closed)
            {
                sqlCon.Open();
            }
            SqlCommand sqlCmd = new SqlCommand("AuditorDeleteByID", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@AuditorID", Convert.ToInt32(hfAuditorID.Value));
            sqlCmd.ExecuteNonQuery();
            sqlCon.Close();
            Clear();
            FillGridView();
            lblSuccessMessage.Text = "Deleted Sucessfully";
        }
    }
}