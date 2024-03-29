﻿using System;
using System.Collections;
using System.Security.Permissions;
using MySql.Data.MySqlClient;

namespace AceSoft.RetailPlus.Data
{

    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
         PublicKey = "002400000480000094000000060200000024000" +
         "052534131000400000100010053D785642F9F960B43157E0380" +
         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
         "FF52834EAFB5A7A1FDFD5851A3")]
    public struct BranchInventoryDetails
    {
        public long BranchInventoryID;
        public long ProductID;
        public string ProductCode;
        public long VariationMatrixID;
        public string MatrixDescription;
        public decimal Quantity;
        public int BranchID;
        public string BranchCode;
    }

    public struct BranchInventoryColumns
    {
        public bool BranchInventoryID;
        public bool ProductID;
        public bool ProductCode;
        public bool VariationMatrixID;
        public bool MatrixDescription;
        public bool Quantity;
        public bool BranchID;
        public bool BranchCode;
    }

    public struct BranchInventoryColumnNames
    {
        public const string BranchInventoryID = "BranchInventoryID";
        public const string ProductID = "ProductID";
        public const string ProductCode = "ProductCode";
        public const string VariationMatrixID = "VariationMatrixID";
        public const string MatrixDescription = "MatrixDescription";
        public const string Quantity = "Quantity";
        public const string ConvertedQuantity = "ConvertedQuantity";
        public const string BranchID = "BranchID";
        public const string BranchCode = "BranchCode";
    }

    [StrongNameIdentityPermissionAttribute(SecurityAction.LinkDemand,
         PublicKey = "002400000480000094000000060200000024000" +
         "052534131000400000100010053D785642F9F960B43157E0380" +
         "F393BEE53E8DFAFBF441366C1B6F8B48D9DDF0D527B1F3B21EA" +
         "E85D2FDB664CE81EB8A87DBE4C589D6F4202FE2B7D4B978BB69" +
         "684874612CB9B8DB7A0339400A9C4E68277884B07817363D242" +
         "E3696F9FACDBEA831810AE6DC9EDCA91A7B5DA12FE7BF65D113" +
         "FF52834EAFB5A7A1FDFD5851A3")]
    public class BranchInventory : POSConnection
    {
		#region Constructors and Destructors

		public BranchInventory()
            : base(null, null)
        {
        }

        public BranchInventory(MySqlConnection Connection, MySqlTransaction Transaction) 
            : base(Connection, Transaction)
		{

		}

		#endregion

        #region Insert and Update

        public bool CreateToAllBranches(long ProductID)
        {
            try
            {
                bool boRetValue = false;

                string SQL = "CALL procProductBranchInventoryInsert(@lngProductID);";

                

                MySqlCommand cmd = new MySqlCommand();
                
                
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = SQL;

                cmd.Parameters.AddWithValue("@lngProductID", ProductID);

                if (base.ExecuteNonQuery(cmd) > 0) boRetValue = true;

                return boRetValue;
            }

            catch (Exception ex)
            {
                
                
                {
                    
                    
                    
                    
                }

                throw base.ThrowException(ex);
            }
        }


        #endregion

        #region Streams

        public System.Data.DataTable ListAsDataTable(int BranchID, long ProductID, string SortField, System.Data.SqlClient.SortOrder SortOrder)
        {
            string SQL = "SELECT a.BranchID, BranchCode, Quantity " +
                            "FROM tblBranchInventory a " +
                            "   INNER JOIN tblBranch b ON a.BranchID = b.BranchID WHERE 1=1 ";

            if (BranchID != 0) SQL += "AND a.BranchID = @BranchID ";
            if (ProductID != 0) SQL += "AND ProductID = @ProductID ";

            if (SortField != string.Empty && SortField != null)
            {
                SQL += "ORDER BY " + SortField + " ";

                if (SortOrder != System.Data.SqlClient.SortOrder.Descending) SQL += "ASC ";
                else SQL += "DESC ";
            }

            

            MySqlCommand cmd = new MySqlCommand();
            
            
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            if (BranchID != 0) cmd.Parameters.AddWithValue("@BranchID", BranchID);
            if (ProductID != 0) cmd.Parameters.AddWithValue("@ProductID", ProductID);

            System.Data.DataTable dt = new System.Data.DataTable("tblBranch");
            base.MySqlDataAdapterFill(cmd, dt);
            

            return dt;
        }

        private string SQLSelect(BranchInventoryColumns clsBranchInventoryColumns)
        {
            string stSQL = "SELECT ";

            if (clsBranchInventoryColumns.BranchInventoryID) stSQL += "tblBranchInventory." + BranchInventoryColumnNames.BranchInventoryID + ", ";
            if (clsBranchInventoryColumns.ProductID) stSQL += "tblBranchInventory." + BranchInventoryColumnNames.ProductID + ", ";
            if (clsBranchInventoryColumns.ProductCode) stSQL += "tblBranchInventory." + BranchInventoryColumnNames.ProductCode + ", ";
            if (clsBranchInventoryColumns.VariationMatrixID) stSQL += "tblBranchInventory." + BranchInventoryColumnNames.VariationMatrixID + ", ";
            if (clsBranchInventoryColumns.MatrixDescription) stSQL += "tblBranchInventory." + BranchInventoryColumnNames.MatrixDescription + ", ";
            if (clsBranchInventoryColumns.Quantity) stSQL += "tblBranchInventory." + BranchInventoryColumnNames.Quantity + ", ";
            if (clsBranchInventoryColumns.Quantity) stSQL += "fnProductQuantityConvert(tblBranchInventory.ProductID, tblBranchInventory." + BranchInventoryColumnNames.Quantity + ", 0) AS ConvertedQuantity, ";
            if (clsBranchInventoryColumns.BranchCode) stSQL += "tblBranch." + BranchInventoryColumnNames.BranchCode + ", ";

            stSQL += "tblBranchInventory.BranchID FROM tblBranchInventory ";

            if (clsBranchInventoryColumns.BranchCode)
                stSQL += "INNER JOIN tblBranch ON tblBranchInventory.BranchID = tblBranch.BranchID ";

            return stSQL;
        }

        public System.Data.DataTable ListAsDataTable(BranchInventoryColumns clsBranchInventoryColumns, BranchInventoryColumns SearchColumns, int BranchID, long ProductID, string SortField, System.Data.SqlClient.SortOrder SortOrder)
        {
            string SQL =  SQLSelect(clsBranchInventoryColumns);

            if (BranchID != 0) SQL += "AND tblBranchInventory.BranchID = @BranchID ";
            if (ProductID != 0) SQL += "AND tblBranchInventory.ProductID = @ProductID ";

            if (SortField != string.Empty && SortField != null)
            {
                SQL += "ORDER BY " + SortField + " ";

                if (SortOrder != System.Data.SqlClient.SortOrder.Descending) SQL += "ASC ";
                else SQL += "DESC ";
            }

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = SQL;

            if (BranchID != 0) cmd.Parameters.AddWithValue("@BranchID", BranchID);
            if (ProductID != 0) cmd.Parameters.AddWithValue("@ProductID", ProductID);

            System.Data.DataTable dt = new System.Data.DataTable("tblBranch");
            base.MySqlDataAdapterFill(cmd, dt);
            

            return dt;
        }

        #endregion
    }
}

