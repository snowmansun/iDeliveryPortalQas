using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Threading;

public class UserState
{

    // Key Constants 
    private const string KEY_USER_ID = "UserID";
    private const string KEY_USER_NAME = "Name";
    private const string KEY_USER_CODE = "Code";
    private const string KEY_PERSON_ID = "PersonID";
    private const string KEY_POSITION = "Position";
    private const string KEY_DOMAIN_ID = "DomainID";
    private const string KEY_GROUP_ID = "GroupID";
    private const string KEY_RIGHT = "Right";
    private const string KEY_GROUP_NAME = "GroupName";
    private const string LANGUAGE = "zh-cn";
    private const string KEY_ORG_ID = "OrgID";

    private HttpContext mCurrentContext;
   // private MembershipUser theCurrentUser;

    /// <summary> 
    /// Gets or sets the user ID. 
    /// </summary> 
    /// <value>The user ID.</value> 
    public decimal UserID
    {
        get
        {
            if (mCurrentContext.Request.Cookies[KEY_USER_ID] == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    decimal userID;
                    decimal.TryParse(mCurrentContext.Request.Cookies[KEY_USER_ID].Value,out userID);
                    return userID;
                }
                catch
                {
                    return 0;
                }
            }
        }

        set { SetCookie(KEY_USER_ID, value.ToString()); }
    }

    public decimal GroupID
    {
        get
        {
            if (mCurrentContext.Request.Cookies[KEY_GROUP_ID] == null)
            {
                return 0;
            }
            else
            {
                try
                {
                    decimal groupID;
                    decimal.TryParse(mCurrentContext.Request.Cookies[KEY_GROUP_ID].Value, out groupID);
                    return groupID;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
        }

        set { SetCookie(KEY_GROUP_ID, value.ToString()); }
    }

    /// <summary> 
    /// Gets or sets the program ID. 
    /// </summary> 
    /// <value>The program ID.</value> 
    public decimal DomainID
    {
        get
        {
            decimal domainID = 0;
            if (mCurrentContext.Request.Cookies[KEY_DOMAIN_ID] == null)
            {
                return 0;
            }
            else
            {
                decimal.TryParse(mCurrentContext.Request.Cookies[KEY_DOMAIN_ID].Value, out domainID);
                return domainID;
            }
        }
        set { SetCookie(KEY_DOMAIN_ID, value.ToString()); }
    }

    /// <summary> 
    /// Gets or sets the user program ID. 
    /// </summary> 
    /// <value>The user program ID.</value> 
    public string Code
    {
        get
        {
            if (mCurrentContext.Request.Cookies[KEY_USER_CODE] == null)
            {
                return "";
            }
            else
            {
                return HttpUtility.UrlDecode(mCurrentContext.Request.Cookies[KEY_USER_CODE].Value.ToString());
            }
        }
        set { SetCookie(KEY_USER_CODE, HttpUtility.UrlEncode(value.ToString())); }
    }

    /// <summary> 
    /// Gets or sets the user program ID. 
    /// </summary> 
    /// <value>The user program ID.</value> 
    public string Name
    {
        get
        {
            if (mCurrentContext.Request.Cookies[KEY_USER_NAME] == null)
            {
                return "";
            }
            else
            {
                return HttpUtility.UrlDecode(mCurrentContext.Request.Cookies[KEY_USER_NAME].Value.ToString());
            }
        }
        set { SetCookie(KEY_USER_NAME, HttpUtility.UrlEncode(value.ToString())); }
    }

    /// <summary>
    /// 
    /// </summary>
    public string Position
    {
        get
        {
            if (mCurrentContext.Request.Cookies[KEY_POSITION] == null)
            {
                return "";
            }
            else
            {
                return HttpUtility.UrlDecode(mCurrentContext.Request.Cookies[KEY_POSITION].Value.ToString());
            }
        }
        set { SetCookie(KEY_POSITION, HttpUtility.UrlEncode(value.ToString())); }
    }

    /// <summary>
    /// 
    /// </summary>
    public string PersonID
    {
        get
        {
            if (mCurrentContext.Request.Cookies[KEY_PERSON_ID] == null)
            {
                return "";
            }
            else
            {
                return HttpUtility.UrlDecode(mCurrentContext.Request.Cookies[KEY_PERSON_ID].Value.ToString());
            }
        }
        set { SetCookie(KEY_PERSON_ID, HttpUtility.UrlEncode(value.ToString())); }
    }

    public string Rights
    {
        get
        {
            if (mCurrentContext.Request.Cookies[KEY_RIGHT] == null)
            {
                return "";
            }
            else
            {
                return mCurrentContext.Request.Cookies[KEY_RIGHT].Value.ToString();
            }
        }
        set { SetCookie(KEY_RIGHT, value.ToString()); }
    }

    public string GroupName
    {
        get
        {
            if (mCurrentContext.Request.Cookies[KEY_GROUP_NAME] == null)
            {
                return "";
            }
            else
            {
                return HttpUtility.UrlDecode(mCurrentContext.Request.Cookies[KEY_GROUP_NAME].Value.ToString());
            }
        }
        set { SetCookie(KEY_GROUP_NAME, HttpUtility.UrlEncode(value.ToString())); }
    }

    public string Language
    {
        get
        {
            if (mCurrentContext.Request.Cookies[LANGUAGE] == null)
            {
                return Thread.CurrentThread.CurrentUICulture.Name;

            }
            else
            {
                return mCurrentContext.Request.Cookies[LANGUAGE].Value.ToString();
            }
        }
        set { SetCookie(LANGUAGE, value.ToString()); }
    }

    public int OrgID
    {
        get
        {
            int orgID = 0;
            if (mCurrentContext.Request.Cookies[KEY_ORG_ID] == null)
            {
                return 0;
            }
            else
            {
                int.TryParse(mCurrentContext.Request.Cookies[KEY_ORG_ID].Value, out orgID);
                return orgID;
            }
        }
        set { SetCookie(KEY_ORG_ID, value.ToString()); }

    }


    /// <summary> 
    /// Initializes a new instance of the <see cref="T:StateManager" /> class. 
    /// </summary> 
    public UserState()
    {
        mCurrentContext = HttpContext.Current;
    }
    //********************************************************************************************** 
    //** Procedure: Abandon 
    //** 
    //** Description: 
    //********************************************************************************************** 
    public void Abandon()
    {
        System.Web.Security.FormsAuthentication.SignOut();
        RemoveCookie(KEY_USER_ID);
        RemoveCookie(KEY_USER_NAME);
        RemoveCookie(KEY_DOMAIN_ID);
        RemoveCookie(KEY_GROUP_ID);
        RemoveCookie(KEY_RIGHT);
    }

    //********************************************************************************************** 
    //** Procedure: SetCookie 
    //** 
    //** Description: Set cookie value. All of them are done here for consistency of 
    //** secure/expiration properties. 
    //********************************************************************************************** 
    private void SetCookie(string sName, string sValue)
    {

        //mCurrentContext.Request.Cookies.Remove(sName) 
        //mCurrentContext.Response.Cookies[sName].Value = sValue 
        //mCurrentContext.Response.Cookies[sName].Secure = True 

        if ((mCurrentContext.Request.Cookies[sName] != null))
        {
            mCurrentContext.Request.Cookies.Remove(sName);
        }

        mCurrentContext.Response.Cookies[sName].Value = sValue;
        mCurrentContext.Response.Cookies[sName].Expires = DateTime.MinValue;

        if (mCurrentContext.Request.Cookies[sName] == null)
        {
            System.Web.HttpCookie newRequestCookie = new System.Web.HttpCookie(sName);
            newRequestCookie.Value = sValue;
            mCurrentContext.Request.Cookies.Add(newRequestCookie);
        }

    }

    //********************************************************************************************** 
    //** Procedure: RemoveCookie 
    //** 
    //** Description: Expire the provided cookie so the browser deletes it. 
    //********************************************************************************************** 
    private void RemoveCookie(string sKey)
    {

        mCurrentContext.Response.Cookies[sKey].Expires =DateTime.Now .AddDays(-1);
        if (mCurrentContext.Request.Cookies[sKey] == null)
        {
            mCurrentContext.Response.Cookies.Set(new HttpCookie(sKey));
            mCurrentContext.Response.Cookies[sKey].Expires = DateTime.Now.AddYears(-30);
        }
        else
        {
            mCurrentContext.Response.Cookies[sKey].Expires = DateTime.Now.AddYears(-30);
        }

    }
} 
