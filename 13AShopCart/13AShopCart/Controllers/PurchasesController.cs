using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _13AShopCart.Models;
using _13AShopCart.DB;
using System.Data.OleDb;
using System.Data;

namespace _13AShopCart.Controllers
{
    public class PurchasesController : Controller
    {
        // GET: Purchases
        public ActionResult GetPurchaseHistory()
        {
            List<Purchase> purchases = PurchaseData.GetPurchaseHistory();

            //string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/CA_My_Folder/13AShopCart/13AShopCart/Uploadfile/SQL_Table.xlsx;" + "Extended Properties='Excel 8.0;IMEX = 1'"; ;
            //DataSet ds = new DataSet();
            //OleDbDataAdapter oada = new OleDbDataAdapter("select * from [Sheet1$B13:G25]", strConn);
            //oada.Fill(ds);

            //List<Purchase> results = new List<Purchase>();
            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //{
            //    Purchase item = new Purchase();
            //    item.PurchaseId = Convert.ToInt32(ds.Tables[0].Rows[i]["PurchaseID"]);
            //    item.UserId = Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"]);
            //    item.Date = Convert.ToInt32(ds.Tables[0].Rows[i]["Date"]);
            //    item.ProductId = Convert.ToInt32(ds.Tables[0].Rows[i]["ProductId"]);
            //    item.Qty = Convert.ToInt32(ds.Tables[0].Rows[i]["Quantity"]);
            //    item.Code = Convert.ToString(ds.Tables[0].Rows[i]["Code"]);
            //    results.Add(item);
            //}

            //ViewData["sessionId"] = sessionId;
            ViewData["purchases"] = purchases;
            //ViewData["results"] = results;
                    
            return View();
        }

    }

}