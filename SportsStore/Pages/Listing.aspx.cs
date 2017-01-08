using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SportsStore.Models;
using SportsStore.Models.Repository;

namespace SportsStore.Pages
{
    public partial class Listing : System.Web.UI.Page
    {
        private Repository repo = new Repository();
        private int pageSize = 4;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected IEnumerable<Product> GetProducts()
        {
            return FilterProducts().OrderBy(p => p.ProductID).Skip((CurrentPage - 1)*pageSize).Take(pageSize);
        }

        protected int CurrentPage
        {
            get
            {
                int page = GetPageFromRequest();
                return Math.Min(MaxPage, page);
            }
        }

        protected int MaxPage
        {
            get
            {
                int prodCount = FilterProducts().Count();
                return (int)Math.Ceiling((decimal)prodCount / pageSize);
            }
        }

        private IEnumerable<Product> FilterProducts()
        {
            IEnumerable<Product> products = repo.Products;

            string categoryValue = (string)RouteData.Values["category"] ?? Request.QueryString["category"];

            return categoryValue == null ? products :
                products.Where(p => p.Category == categoryValue);
        }

        private int GetPageFromRequest()
        {
            int page;
            string regValue = (string)RouteData.Values["page"] ?? Request.QueryString["page"];
            return regValue != null && int.TryParse(regValue, out page) ? page : 1;
        }
    }
}