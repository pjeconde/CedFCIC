using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;

namespace CedFCIC
{
    public static class Funciones
    {
        //public static void PersonalizarControlesMaster(MasterPage Master, bool RefrescaDatosUsuario, Entidades.Sesion Sesion)
        //{
        //if (RefrescaDatosUsuario) RN.Sesion.RefrescarDatosUsuario(Sesion.Usuario, Sesion);
        //if (Sesion.Ambiente != "PROD")
        //{
        //    Label ambienteLabel = ((Label)Master.FindControl("AmbienteLabel"));
        //    ambienteLabel.Text = Sesion.Ambiente;
        //}

        //ContentPlaceHolder menuContentPlaceHolder = ((ContentPlaceHolder)Master.FindControl("MenuContentPlaceHolder"));

        //Literal menuPpal = ((Literal)Master.FindControl("MenuPpal"));

        //Control AdminSiteControl = (Control)Master.FindControl("AdminSiteControl");
        //AdminSiteControl.Visible = true;

        //ContentPlaceHolder usuarioContentPlaceHolder = ((ContentPlaceHolder)Master.FindControl("UsuarioContentPlaceHolder"));
        //ImageButton usuarioImageButton = ((ImageButton)usuarioContentPlaceHolder.FindControl("UsuarioImageButton"));
        //Label usuarioLabel = ((Label)usuarioContentPlaceHolder.FindControl("UsuarioLabel"));
        //HyperLink usuarioHyperLink = ((HyperLink)usuarioContentPlaceHolder.FindControl("UsuarioHyperLink"));
        //Label cUITLabel = ((Label)usuarioContentPlaceHolder.FindControl("CUITLabel"));
        //DropDownList cUITDropDownList = ((DropDownList)usuarioContentPlaceHolder.FindControl("CUITDropDownList"));
        //Label uNLabel = ((Label)usuarioContentPlaceHolder.FindControl("UNLabel"));
        //DropDownList uNDropDownList = ((DropDownList)usuarioContentPlaceHolder.FindControl("UNDropDownList"));

        //usuarioLabel.Visible = false;
        //cUITDropDownList.DataValueField = "Nro";
        //cUITDropDownList.DataTextField = "NroYRazSoc";
        //cUITDropDownList.DataSource = new List<Entidades.Cuit>();
        //cUITDropDownList.DataBind();

        //uNDropDownList.DataValueField = "Id";
        //uNDropDownList.DataTextField = "Descr";
        //uNDropDownList.DataSource = new List<Entidades.UN>();

        //menuContentPlaceHolder.Visible = false;
        //usuarioContentPlaceHolder.Visible = false;
        //cUITLabel.Visible = false;
        //cUITDropDownList.Visible = false;
        //uNLabel.Visible = false;
        //uNDropDownList.Visible = false;

        //Control salir = (Control)Master.FindControl("Salir");
        ////salir.Visible = false;

        //if (Sesion != null)
        //{
        //    //foreach (string s in Sesion.OpcionesHabilitadas)
        //    //{
        //    //    MenuItem mItemFind = menu.FindItem(s);
        //    //    if (mItemFind != null)
        //    //    {
        //    //        mItemFind.Selectable = true;
        //    //    }
        //    //}
        //    //Nuevo Menu
        //    foreach (Entidades.Opcion o in Sesion.Opciones)
        //    {
        //        System.Web.UI.HtmlControls.HtmlAnchor mItemFind = (System.Web.UI.HtmlControls.HtmlAnchor)Master.FindControl(o.Nombre);
        //        if (mItemFind != null)
        //        {
        //            mItemFind.Visible = true;
        //            if (o.Habilitada)
        //            {
        //                mItemFind.Style.Add("Color", "#111111");
        //                mItemFind.HRef = o.Vinculo;
        //            }
        //        }
        //    }
        //    menuContentPlaceHolder.Visible = true;
        //    usuarioContentPlaceHolder.Visible = true;
        //    if (Sesion.Usuario.Id != null)
        //    {
        //        usuarioLabel.Visible = true;
        //        String path = Master.Server.MapPath("~/ImagenesSubidas/");
        //        string[] archivos = System.IO.Directory.GetFiles(path, Sesion.Usuario.Id + ".*", System.IO.SearchOption.TopDirectoryOnly);
        //        usuarioImageButton.Visible = true;
        //        if (archivos.Length > 0)
        //        {
        //            usuarioImageButton.ImageUrl = "~/ImagenesSubidas/" + archivos[0].Replace(Master.Server.MapPath("~/ImagenesSubidas/"), String.Empty);
        //        }
        //        else
        //        {
        //            usuarioImageButton.ImageUrl = "~/Imagenes/SiluetaHombre.jpg";
        //        }
        //        usuarioHyperLink.Text = Sesion.Usuario.Nombre.Replace(" ", "&nbsp;");
        //        //menu.Items[menu.Items.Count - 1].Selectable = true;
        //        if (Sesion.CuitsDelUsuario.Count != 0)
        //        {
        //            cUITDropDownList.DataSource = Sesion.CuitsDelUsuario;
        //            cUITDropDownList.DataBind();
        //            if (Sesion.Cuit != null)
        //            {
        //                cUITDropDownList.SelectedValue = Sesion.Cuit.Nro;
        //                if (Sesion.Cuit.WF.Estado != "Vigente")
        //                {
        //                    cUITLabel.ForeColor = Color.Red;
        //                }
        //                else
        //                {
        //                    cUITLabel.ForeColor = Color.Black;
        //                }
        //            }
        //            cUITLabel.Visible = true;
        //            cUITDropDownList.Visible = true;
        //        }
        //        if (Sesion.Cuit.UNs.Count != 0)
        //        {
        //            uNDropDownList.DataSource = Sesion.Cuit.UNs;
        //            uNDropDownList.DataBind();
        //            if (Sesion.UN != null)
        //            {
        //                uNDropDownList.SelectedValue = Sesion.UN.Id.ToString();
        //                if (Sesion.UN.WF.Estado != "Vigente")
        //                {
        //                    uNLabel.ForeColor = Color.Red;
        //                }
        //                else
        //                {
        //                    uNLabel.ForeColor = Color.Black;
        //                }
        //            }
        //            uNLabel.Visible = true;
        //            uNDropDownList.Visible = true;
        //        }
        //        Control c = (Control)Master.FindControl("Salir");
        //        if (c != null)
        //        {
        //            c.Visible = true;
        //        }
        //    }
        //}
        //if (Sesion.Usuario.Id == null)
        //{
        //    //for (int i = menu.Items.Count - 1; i > 0; i--)
        //    //{
        //    //    RemoverMenuItem(menu, menu.Items[i]);
        //    //}
        //    //Control c = (Control)Master.FindControl("btnUsuarioLogin");
        //    //if (c != null)
        //    //{
        //    //    c.Visible = true;
        //    //}
        //}
        ////MenuItem menuItem = menu.FindItem("Iniciar sesión");
        ////if (menuItem != null && !menuItem.Selectable) RemoverMenuItem(menu, menuItem);
        ////MenuItem menuItemRef = menu.FindItem("Administración Site|Comprobantes");
        ////MenuItem menuItem = menu.FindItem("Administración Site");
        ////if (menuItem != null && !menuItemRef.Selectable) RemoverMenuItem(menu, menuItem);
        //Master.DataBind();
        //}
        //private static void RemoverMenuItem(Menu Menu, MenuItem MenuItem)
        //{
        //    for (int j = MenuItem.ChildItems.Count - 1; j >= 0; j--)
        //    {
        //        MenuItem.ChildItems.Remove(MenuItem.ChildItems[0]);
        //    }
        //    Menu.Items.Remove(MenuItem);
        //}
        //public static void RemoverMenuItem(Menu Menu, string IdMenuItem)
        //{
        //    MenuItem menuItem = Menu.FindItem(IdMenuItem);
        //    if (menuItem != null) RemoverMenuItem(Menu, menuItem);
        //}
        //public static void GenerarImagenCaptcha(System.Web.SessionState.HttpSessionState Session, System.Web.UI.WebControls.Image CaptchaImage, TextBox CaptchaTextBox)
        //{
        //    string s = RandomText.Generate();
        //    string ens = Encryptor.Encrypt(s, "srgerg$%^bg", Convert.FromBase64String("srfjuoxp"));
        //    Session["captcha"] = s.ToLower();
        //    string color = "#ffffff";
        //    CaptchaImage.ImageUrl = "~/Captcha.ashx?w=305&h=92&c=" + ens + "&bc=" + color;
        //    CaptchaTextBox.Text = String.Empty;
        //}
        //public static bool SessionTimeOut(System.Web.SessionState.HttpSessionState Session)
        //{
        //    return ((Entidades.Sesion)Session["Sesion"]).Usuario.Id == null;
        //}

        //public static void GrabarLogTexto(string archivo, string mensaje)
        //{
        //    try
        //    {
        //        using (FileStream fs = File.Open(HttpContext.Current.Server.MapPath(archivo), FileMode.Append, FileAccess.Write))
        //        {
        //            using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8))
        //            {
        //                sw.WriteLine(DateTime.Now.ToString("yyyyMMdd hh:mm:ss") + "  " + mensaje);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}
        public static string TextoScript(string Contenido)
        {
            return "<SCRIPT LANGUAGE='javascript'>alert('" + Contenido.Replace("'", "").Replace("\r\n", "  ") + "');</SCRIPT>";
        }
        public static void FechasPredefinidas(string Elegida, out string FechaDsd, out string FechaHst)
        {
            FechaDsd = "";
            FechaHst = "";
            switch (Elegida)
            {
                case "MesActual":
                    FechaDsd = DateTime.Now.ToString("yyyyMM01");
                    FechaHst = DateTime.Now.ToString("yyyyMMdd");
                    break;
                case "MesAnterior":
                    FechaDsd = DateTime.Now.AddMonths(-1).ToString("yyyyMM01");
                    FechaHst = Convert.ToDateTime(DateTime.Now.ToString("01/MM/yyyy")).AddDays(-1).ToString("yyyyMMdd");
                    break;
                case "TresMesesUltimos":
                    FechaDsd = DateTime.Now.AddMonths(-2).ToString("yyyyMM01");
                    FechaHst = DateTime.Now.ToString("yyyyMMdd");
                    break;
                case "TresMesesAnteriores":
                    FechaDsd = DateTime.Now.AddMonths(-3).ToString("yyyyMM01");
                    FechaHst = Convert.ToDateTime(DateTime.Now.ToString("01/MM/yyyy")).AddDays(-1).ToString("yyyyMMdd");
                    break;
                case "AnualActual":
                    FechaDsd = DateTime.Now.ToString("yyyy") + "0101";
                    FechaHst = DateTime.Now.ToString("yyyyMMdd");
                    break;
                case "AnualAnterior":
                    FechaDsd = DateTime.Now.AddYears(-1).ToString("yyyy") + "0101";
                    FechaHst = DateTime.Now.AddYears(-1).ToString("yyyy") + "1231";
                    break;
                case "DesdeSiempre":
                    FechaDsd = "19800101";
                    FechaHst = FechaHst = DateTime.Now.ToString("yyyyMMdd");
                    break;
                default:
                    throw new CedFCIC.EX.Validaciones.ValorInvalido("FechaPredefinida");
            }
        }
        public static bool SessionOK(Object sesion)
        {
            bool resp = false;
            try
            {
                CedFCIC.Entidades.Sesion s = (CedFCIC.Entidades.Sesion)sesion;
                if (s != null && s.Usuario != null && s.Usuario.Id != null)
                {
                    resp = true;
                }
            }
            catch
            {

            }
            return resp;
        }
    }
}