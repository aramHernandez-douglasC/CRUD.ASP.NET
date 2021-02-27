using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using static CRUDApplication_AramHernandez.DAO;
using System.Text.RegularExpressions;

namespace CRUDApplication_AramHernandez
{
    public partial class Default : System.Web.UI.Page
    {
        List<string> listOfGenders;
        DAO dao = new DAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Clear();
                readUsers();
                initGenders();
            }
           
        }
        void readUsers()
        {

            DataTable dt = dao.readAll();
            user_grid.DataSource = dt;
            ViewState["dt1"] = dt; 
            user_grid.DataBind();

        }

        protected void btnCreate_Click(Object sender, EventArgs e)
        {

            errorMsg.Text = "";
            string _username, _email, _password, _gender;
            DateTime _dob;
            int _userId, _role;
            if (password.Text == repPassword.Text)
            {

                if (PasswordValidation(password.Text)) {
                    _userId = Convert.ToInt32(htId.Value == "" ? "0" : htId.Value);
                    _email = email.Text.Trim();
                    _username = name.Text.Trim();
                    _password = password.Text.Trim();
                    _dob = dob.SelectedDate;
                    _role = role.Checked ? 1 : 0;
                    switch (gender.SelectedIndex)
                    {
                        case 0:
                            _gender = "male";
                            break;
                        case 1:
                            _gender = "female";
                            break;
                        case 2:
                            _gender = otherSelection.SelectedValue;
                            break;
                        default:
                            _gender = "error";
                            break;

                    }
                    try
                    {
                        dao.addorUpdate(_userId, _username, _email, _password, _gender, _dob, _role);
                        successMsg.Text = "User successfully added";
                        btnCreate.Text = "Create";
                        readUsers();
                        Clear();
                    }
                    catch (Exception l)
                    {
                        errorMsg.Text = l.Message;
                    }
                }
                else
                {
                    errorMsg.Text = "Your password must contain: \n" +
                        "-At least one number \n" +
                        "-Min 2 uppercase letter \n" +
                        "-Min 3 special character (-+_!@#$%^&*., ?)";
                }



            }            
            else
            {
                errorMsg.Text = "Your password doesn't match";
            }
        }
        protected void btnClear_Click(Object sender, EventArgs e)
        {
            Clear();
        }
        void Clear()
        {
            htId.Value = "";
            name.Text = "";
            email.Text = "";
            dob.SelectedDate = DateTime.Today;
            password.Text = "";
            gender.SelectedIndex = 0;
            role.Checked = false;


        }
        protected void btnDelete_Click(Object sender, EventArgs e)
        {
            try
            {
                dao.deleteById(Convert.ToInt32(htId.Value == "" ? "0" : htId.Value));
                readUsers();
                Clear();
                successMsg.Text = "User Deleted Successfully";
                btnDelete.Enabled = false;
            }
            catch (Exception m)
            {

                errorMsg.Text= m.Message;
            }
        }
        protected void link_Click(Object sender, EventArgs e)
        {
            //errorMsg.Text = (sender as LinkButton).CommandArgument;
            btnDelete.Enabled = true;
            int id = Convert.ToInt32((sender as LinkButton).CommandArgument);

            DataTable tab = dao.readById(id);
            try
            {
                htId.Value = tab.Rows[0][0].ToString();
                name.Text = tab.Rows[0][1].ToString();
                email.Text = tab.Rows[0][2].ToString();
                password.Text = tab.Rows[0][3].ToString();

                //Role
                if (tab.Rows[0][5].ToString() == "0")
                {
                    role.Checked = false;
                }
                else
                {
                    role.Checked = true;
                }

                //Gender
                switch (tab.Rows[0][4].ToString().ToLower())
                {
                    case "male":
                        gender.SelectedIndex = 0;
                        break;

                    case "female":
                        gender.SelectedIndex = 1;
                        break;

                    case "other":
                        otherSelection.SelectedValue = tab.Rows[0][4].ToString();
                        break;
                }
                dob.SelectedDate = DateTime.Parse(tab.Rows[0][6].ToString());
                btnCreate.Text = "Update";

            }catch (Exception p)
            {
                errorMsg.Text = p.Message;
            }

        }
        
        void initGenders()
        {

            this.listOfGenders = new List<string>();
            listOfGenders.Add("Transgender");
            listOfGenders.Add("Agender");
            listOfGenders.Add("Bigender");
            listOfGenders.Add("Gender Variant");
            foreach(var g in listOfGenders)
            {
                otherSelection.Items.Add(g);
            }



        }





        public Boolean PasswordValidation(string p)
        {
            string passwordPattern = @"((?=.*\d)(?=.*[a-z])(?=.{2,}[A-Z])(?=.{3,}[-+_!@#$%^&*., ?]).{8,64})";
            Regex rg = new Regex(passwordPattern);
            if (rg.IsMatch(p))
            {
                return true;
            }
            return false;
        }

        protected void userGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void userGrid_Sorting(object sender, GridViewSortEventArgs e)
        {
            if(ViewState["dt1"] != null)
            {
                DataTable dt = (DataTable)ViewState["dt1"];
                DataView dv = new DataView(dt);
                dv.Sort = e.SortExpression;
                user_grid.DataSource = dv;
                user_grid.DataBind();
            }
        }

        protected void userGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            user_grid.PageIndex = e.NewPageIndex;
            readUsers();
        }

        protected void searchBtn_Click(object sender, EventArgs e)
        {

            DataTable dt = dao.readAll();
           
            string searchItem = searchTxt.Text;
            DataTable newDt = dt.Select("user_name LIKE'%" + searchItem + "%'").CopyToDataTable();


            if (newDt == null)
            {

                errorMsg.Text = "No user with that name found";
            }
            else
            {
                user_grid.DataSource = newDt;
                ViewState["dt1"] = newDt;
                user_grid.DataBind();
            }
            searchTxt.Text = "";
        }
    }
}