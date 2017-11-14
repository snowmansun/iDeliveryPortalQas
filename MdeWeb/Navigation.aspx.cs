using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datapool.IBLL;
using Datapool.BLL;

namespace DatapoolWeb
{
    public partial class Navigation : BasePage
    {
        IMenuService menuService = new MenuService();
        List<string> modules = new List<string>();

        protected string LoadMenuItem()
        {
            StringBuilder builder = new StringBuilder("");
            var menus = menuService.LoadMenuInfo(UserStateInfo.UserID);

            //DataTable dt = new DataTable();
            //dt.Columns.Add("res_no", typeof(string));
            //dt.Columns.Add("MODULE_NAME", typeof(string));
            //dt.Columns.Add("MODULE_ICON", typeof(string));
            //dt.Columns.Add("ITEM_NAME", typeof(string));
            //dt.Columns.Add("ITEM_LINK", typeof(string));

            ////for (int i = 0; i < 50; i++)
            ////{
            //    DataRow dr = dt.NewRow();
            //    dr[0] = "144";
            //    dr[1] = "门店管理";
            //    dr[2] = "Resources/Images/menu/Customer.jpg";
            //    dr[3] = "门店维护";
            //    dr[4] = "View/Store/StoreView.aspx";
            //    dt.Rows.Add(dr);
            ////}

            //DataTable dt = new RoleManager().GetUserRole(MyStateManager.UserID);

            string TempModule = string.Empty;
            string Module = string.Empty;

            if (menus.Count() > 0)
            {
                int i = 0;
                foreach (var m in menus)
                {
                    if (!(modules.Contains(m.MODULE_NAME)))
                    {
                        if (modules.Count != 0)
                        {
                            builder.Append(" </ul>  </div> ");
                        }

                        modules.Add(m.MODULE_NAME);

                        builder.Append(" <div class='list bg03' id='listA_");
                        builder.Append((modules.Count).ToString());
                        builder.Append("' onclick='showListA(");
                        builder.Append((modules.Count).ToString());
                        builder.Append(");'>");
                        builder.Append(m.MODULE_NAME);
                        builder.Append("</div> ");

                        builder.Append("<div class='list02' id='listB_");
                        builder.Append((modules.Count).ToString());
                        builder.Append("' style='display: none'> ");

                        builder.Append(" <ul id='UL");
                        builder.Append((modules.Count).ToString());
                        builder.Append("'> ");

                        builder.Append(" <li><a id='A");
                        builder.Append((i + 1).ToString());
                        builder.Append("' onclick='changeColor(this);'");
                        builder.Append("href='");
                        builder.Append(m.ITEM_LINK);
                        builder.Append("' target='mainFrame'>");
                        builder.Append(m.ITEM_NAME);
                        builder.Append("</a></li> ");

                    }
                    else
                    {
                        builder.Append(" <li><a id='A");
                        builder.Append((i + 1).ToString());
                        builder.Append("' onclick='changeColor(this);'");
                        builder.Append("href='");
                        builder.Append(m.ITEM_LINK);
                        builder.Append("' target='mainFrame'>");
                        builder.Append(m.ITEM_NAME);
                        builder.Append("</a></li> ");
                    }

                    i++;
                }//end foreach 

                builder.Append(" </ul>  </div> ");

            }

            //if (dt != null)
            //{
            //    DataView dv = dt.DefaultView;
            //    for (int i = 0; i < dv.Count; i++)
            //    {
            //        if (!(modules.Contains(dv[i]["MODULE_NAME"].ToString())))
            //        {
            //            if (modules.Count != 0)
            //            {
            //                builder.Append(" </ul>  </div> ");
            //            }

            //            modules.Add(dv[i]["MODULE_NAME"].ToString());

            //            builder.Append(" <div class='list bg03' id='listA_");
            //            builder.Append((modules.Count).ToString());
            //            builder.Append("' onclick='showListA(");
            //            builder.Append((modules.Count).ToString());
            //            builder.Append(");'>");
            //            builder.Append(dv[i]["MODULE_NAME"].ToString());
            //            builder.Append("</div> ");

            //            builder.Append("<div class='list02' id='listB_");
            //            builder.Append((modules.Count).ToString());
            //            builder.Append("' style='display: none'> ");

            //            builder.Append(" <ul id='UL");
            //            builder.Append((modules.Count).ToString());
            //            builder.Append("'> ");

            //            builder.Append(" <li><a id='A");
            //            builder.Append((i + 1).ToString());
            //            builder.Append("' onclick='changeColor(this);'");
            //            builder.Append("href='");
            //            builder.Append(dv[i]["ITEM_LINK"].ToString());
            //            builder.Append("' target='mainFrame'>");
            //            builder.Append(dv[i]["ITEM_NAME"].ToString());
            //            builder.Append("</a></li> ");

            //        }
            //        else
            //        {
            //            builder.Append(" <li><a id='A");
            //            builder.Append((i + 1).ToString());
            //            builder.Append("' onclick='changeColor(this);'");
            //            builder.Append("href='");
            //            builder.Append(dv[i]["ITEM_LINK"].ToString());
            //            builder.Append("' target='mainFrame'>");
            //            builder.Append(dv[i]["ITEM_NAME"].ToString());
            //            builder.Append("</a></li> ");
            //        }
            //    }

            //    builder.Append(" </ul>  </div> ");

            //}
            return builder.ToString();
        }

        protected string LoadTopMenu()
        {
            //DataTable dt = null;


            DataTable dt = new DataTable();
            dt.Columns.Add("res_no", typeof(string));
            dt.Columns.Add("MODULE_NAME", typeof(string));
            dt.Columns.Add("MODULE_ICON", typeof(string));
            dt.Columns.Add("ITEM_NAME", typeof(string));
            dt.Columns.Add("ITEM_LINK", typeof(string));

            //for (int i = 0; i < 50; i++)
            //{
            DataRow dr = dt.NewRow();
            dr[0] = "144";
            dr[1] = "门店管理";
            dr[2] = "Resources/Images/menu/Customer.jpg";
            dr[3] = "门店维护";
            dr[4] = "Customer/CustomerList.aspx";
            dt.Rows.Add(dr);
            //}

            string TempModule = string.Empty;

            if (dt.Rows.Count == 0)
            {
                return "<script>alert('未给该用户分配角色')</script>";
            }
            else
            {
                return string.Empty;
            }
        }

        protected string WriteScript()
        {
            StringBuilder sb = new StringBuilder(@"<script language='JavaScript' type='text/javascript'>
            var preId = ''
            var Aflag = 1;//开关标识 1关　0开
            var Nflag = 0;
            var channel = ");

            sb.Append(modules.Count.ToString());

            sb.Append(@";
                    function showListA(n) {
                        for (var i = 1; i <= channel; i++) {
                            if (i == n)//对应的选项
                            {
                                if (Aflag == 1 || Nflag != n)//判断是否以及打开   或是关闭 aflag=1 +     aflag=0 -
                                {
                                    $('listA_' + i).className = 'list bg04';
                                    $('listB_' + i).style.display = '';
                                    Aflag = 0;
                                }
                                else //关闭此项
                                {
                                    $('listA_' + i).className = 'list bg03';
                                    $('listB_' + i).style.display = 'none';
                                    Aflag = 1;
                                }
                            }
                            else //其余的选项 都关闭
                            {
                                $('listA_' + i).className = 'list bg03';
                                $('listB_' + i).style.display = 'none';
                            }
                        }
                        Nflag = n;
                    }

                    function changeColor(cur) {
                        if (pre = $(preId)) {
                            pre.style.color = '';
                        }
                        preId = cur.id;
                        cur.style.color = 'red';
                    }
                </script> ");

            return sb.ToString();
        }
    }
}