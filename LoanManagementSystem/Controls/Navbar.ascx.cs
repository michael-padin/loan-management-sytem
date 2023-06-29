﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Navbar : System.Web.UI.UserControl
{
    public string Title { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        LiteralTitle.Text = Title;

        lblFullName.Text = Session["FullName"].ToString();
        lblEmail.Text = Session["email"].ToString();

    }
}