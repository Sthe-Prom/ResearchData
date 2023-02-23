using ResearchData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ResearchData.Models.Interfaces;
using ResearchData.Models.EF;

using System.Data.Odbc;
using Microsoft.Data.SqlClient;

using System.IO;
using Microsoft.AspNetCore.Hosting;

using System.Data;
//using System.Data.Entity.Core;
//using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Net;
using System.Text.RegularExpressions;
//using System.Web;
//using Microsoft.AspNetCore.Mvc.Rendering;
// using ExcelImport.Models;
//using LinqToExcel;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;

namespace ResearchData.Controllers
{
   
    public class AnswerController : Controller
    {
        //Private Fields
        //--------------

        private IAnswer context;
        private IQuestion q_ctx;
        private IQuestionOption op_ctx;
        private IAccount acc_context;
        private IConfiguration Configuration;
        private readonly IWebHostEnvironment HostEnvironment;
        
        public string sOffCampuServer = @"Server=156.38.224.15;Database=KisCret_DB;MultipleActiveResultSets=true;User ID=xslicwih_ms_admin;password=#21MsDBUser@";
        //public string sOffCampuServer = @"Server=196.255.240.74;Database=iinfo;MultipleActiveResultSets=true;User ID=Sthembiso;password=IIS123";
        
        //public string sCampusServer = Configuration.GetConnectionString("Data:ResearchData:ConnectionString").ToString();
        // public string sOffCampuServer = Configuration.GetConnectionString("Data:ResearchData2:ConnectionString").ToString();
        
        //Constructor
        //-----------
        public AnswerController(IAnswer ctx, IAccount acc_context_,  IWebHostEnvironment hostEnvironment, 
            IConfiguration _configuration, IQuestionOption op_ctx_, IQuestion q_ctx_)
        {
            context = ctx;
            acc_context = acc_context_;
            this.HostEnvironment = hostEnvironment;
            this.Configuration = _configuration;
            op_ctx = op_ctx_;
            q_ctx = q_ctx_;
        }

        //READ
        // public ViewResult Index() => View(context.Answers);
        [HttpGet]
        public ActionResult Index(string QuestionID)
        {
            AnswerViewModel vm = new AnswerViewModel();
            vm.Accounts = acc_context.Accounts;
            vm.SurveysSelect = getSurveys();
            vm.QuestionsSelect = getQuestions();
            //vm.QuestionsSelect2 = GetQuestions_();
            // vm.QuestionsSelect3 = GetQuestions_();
             vm.OptionsSelect = getOptions();
            vm.QuestionsSelect4 = GetQuestionsTest();
            //vm.OptionsSelect3 = GetOptionsTest(QuestionID);
          
            string sWebRootFolder = HostEnvironment.WebRootPath;
            string sFileName = @"ReseachResponseTemplate.xlsx";
           // string URL = string.Format("{0}://{1}/{2}",Microsoft.AspNetCore.Http.Request.Scheme, Microsoft.AspNetCore.Http.Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            if (file.Exists)
            {
                file.Delete();
                //file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            }
            ViewBag.Message = "";
            
            return View(vm);
        }

        //TEMPLATE DOWNLOAD
        /// <summary>
        /// This function is used to download excel format.
        /// </summary>
        /// <param name="Path"></param>
        /// <returns>file</returns>
        public FileResult DownloadExcel()
        {
            string path = Path.Combine( HostEnvironment.WebRootPath, "files" ) + "ReseachResponseTemplate.xlsx" ;
            //string path = "~/files/ReseachResponseTemplate.xlsx";
           // return File(path, "application/vnd.ms-excel", "ReseachResponseTemplate.xlsx"); 

            //Create a Folder.
            //string path = Path.Combine(HostEnvironment.WebRootPath, "files/templates");
           
            // string Files = "~/files/templates/ReseachResponseTemplate.xlsx";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            // System.IO.File.WriteAllBytes(Files, fileBytes);
            // MemoryStream ms = new MemoryStream(fileBytes);
             return File(fileBytes, "application/vnd.ms-excel", "ReseachResponseTemplate.xlsx");           
        }
      
        //UPLOAD RESPONSES
        [HttpPost]
        public ActionResult Index(IFormFile postedFile, string QuestionID, string OptionID, string ScaleOptionID, string SurveyID )
        {
            int QuestionID_ = Convert.ToInt32(QuestionID);
            //QuestionID_ = 13;
            //int OptionID_ = Convert.ToInt32(Request.Form["OptionID"]);
            int OptionID_= Convert.ToInt32(OptionID);
            //OptionID_ = ViewBag.OptionID2; 
            int ScaleOptionID_ = Convert.ToInt32(ScaleOptionID);
            int SurveyID_ = Convert.ToInt32(SurveyID);

            AnswerViewModel vm = new AnswerViewModel();
            vm.Accounts = acc_context.Accounts;
            vm.SurveysSelect = getSurveys();
            // vm.QuestionsSelect = getQuestions();
            // vm.QuestionsSelect3 = GetQuestions_();
            vm.QuestionsSelect4 = GetQuestionsTest();
            vm.OptionsSelect = getOptions();
            //vm.OptionsSelect3 = GetOptionsTest(QuestionID);
   
            string filename = null;
            string targetpath = null;

            if(postedFile!= null)
            {
                //Create a Folder.
                string path = Path.Combine(HostEnvironment.WebRootPath, "files");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string uploadsFolder = Path.Combine(HostEnvironment.WebRootPath, "files");
                filename = postedFile.FileName;
                targetpath = Path.Combine(uploadsFolder, filename);

                using(var fileStream = new FileStream(targetpath, FileMode.Create))
                {                    
                    // using (StreamWriter sw = new StreamWriter(fileStream, Encoding.UTF8))
                    // {
                    //     MySerializableClass msc = new MySerializableClass();
                    //     XmlSerializer serializer = new XmlSerializer(typeof(MySerializableClass));
                    //     serializer.Serialize(sw, msc);
                    // }

                    postedFile.CopyTo(fileStream);
                }            

                List<string> data = new List<string>();
         
                // tdata.ExecuteCommand("truncate table OtherCompanyAssets");
                if (postedFile.ContentType == "application/vnd.ms-excel" || postedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {                 
                    // Check Excel Conn string 

                    var connectionString = "";
                    if (filename.EndsWith(".xls"))
                    {
                        //connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", pathToExcelFile);
                        connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'"; //this.Configuration.GetConnectionString("Data:ExcelConString:Generic");
                    }
                    else if (filename.EndsWith(".xlsx"))
                    {
                        //connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", pathToExcelFile);
                        connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";  //this.Configuration.GetConnectionString("Data:ExcelConString:Generic");
                        //Configuration["Data:ResearchData:ConnectionString"])
                    }

                     DataTable dt = new DataTable();
                    connectionString = string.Format(connectionString, targetpath);

                    using (OleDbConnection connExcel = new OleDbConnection(connectionString))
                    {
                        using (OleDbCommand cmdExcel = new OleDbCommand())
                        {
                            using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                            {
                                cmdExcel.Connection = connExcel;
        
                                //Get the name of First Sheet.
                                connExcel.Open();
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                connExcel.Close();
        
                                //Read Data from First Sheet and Map New columns.
                                // connExcel.Open();
                                //     string sSql = "Create Table [Sheet1$] (Answer_ Varchar(255), SheetID Int, OptionID Int, QuestionID Int, ScaleOptionID Int)";
                                //     cmdExcel.CommandText = sSql;
                                //     cmdExcel.ExecuteNonQuery();
                                // connExcel.Close();

                                // connExcel.Open();
                                //     cmdExcel.CommandText = @"Insert into [Sheet1$] (Answer_, SheetID, OptionID, QuestionID, ScaleOptionID) values('Ans','1',1,2,3);";
                                                                                                                            
                                //        // + "VALUES ('" + 17 + "','" + 2 + "','"+ 100 +"');";                
                                //     cmdExcel.ExecuteNonQuery();
                                // connExcel.Close();

                                //Load Data
                                connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "] where [Answer_] <> NULL";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                connExcel.Close();
                            }
                        }
                    }

                    //Insert the Data read from the Excel file to Database Table.
                    //conString = this.Configuration.GetConnectionString("constr");
                    using (SqlConnection con = new SqlConnection(sOffCampuServer))
                    {
                        using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        {
                            // // //Set the database table name.
                            // sqlBulkCopy.DestinationTableName = "dbo.Answer";
        
                            // //[OPTIONAL]: Map the Excel columns with that of the database table.
                            // //sqlBulkCopy.ColumnMappings.Add("Id", "CustomerId");
                            // sqlBulkCopy.ColumnMappings.Add("Answer_", "Answer_");
                            // sqlBulkCopy.ColumnMappings.Add("SheetID", "SheetID");                            
                            // sqlBulkCopy.ColumnMappings.Add("OptionID", "OptionID");
                            // sqlBulkCopy.ColumnMappings.Add("QuestionID", "QuestionID");
                            // sqlBulkCopy.ColumnMappings.Add("ScaleOptionID", "ScaleOptionID");

                            // foreach (DataColumn col in dt.Columns)
                            // {
                            //     sqlBulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                            // }

                            // con.Open();
                            // sqlBulkCopy.WriteToServer(dt);
                            // con.Close();

                            //Find Selected Questions and Options


                            con.Open();

                            SqlCommand cmdinsert = new SqlCommand();
                            cmdinsert.Connection = con;


                            string sql = "";
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                sql = sql + "insert into Answer (Answer_, SheetID, OptionID, QuestionID, ScaleOptionID, SurveyID) values( '"
                                    + dt.Rows[i]["Answer_"].ToString().Trim() + "','"
                                    + dt.Rows[i]["SheetID"].ToString().Trim() + "','" 
                                    + OptionID_ + "','"
                                    + QuestionID_ + "','"
                                    + ScaleOptionID_ + "','"
                                    + SurveyID + "')";

                                    // + dt.Rows[i]["OptionID"].ToString().Trim() + "','" 
                                    // + dt.Rows[i]["QuestionID"].ToString().Trim() + "','" 
                                    // + dt.Rows[i]["ScaleOptionID"].ToString().Trim() + "')";
                            
                            }

                            cmdinsert.CommandText = sql;
                            int ret = 0;
                            ret = cmdinsert.ExecuteNonQuery();
                           
                            sql = "";

                            con.Close();

                            if(ret != 0)
                            {
                                ViewBag.Message = "File Upload Successful";
                            }
                            else
                            {
                                ViewBag.Message = "File Upload Failed";
                            }

                        }
                    }        

                    // var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);                 
                }
                
            }
            else
                {
                    ViewBag.Message = "An Error Occured, please check file and try again";
                }

            return View(vm);
        }

            //D R O P - D O W N S

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList getSurveys()
        {
            List<Survey> models = new List<Survey>();
                       
            using(SqlConnection connection = new SqlConnection(sOffCampuServer)) 
            {
                using(SqlCommand cmd = new SqlCommand("", connection))
                {
                    connection.Open();
                    cmd.CommandText = "Select SurveyID, SurveyName from Survey";
                    using(var reader = cmd.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                var m = new Survey();
                                m.SurveyID = reader.GetInt32(reader.GetOrdinal("SurveyID"));
                                m.SurveyName = reader.GetString(reader.GetOrdinal("SurveyName"));
                                models.Add(m);
                            }
                        }
                    }
                }
            };

            Microsoft.AspNetCore.Mvc.Rendering.SelectList SurveySelect = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(models, "SurveyID", "SurveyName");
            return SurveySelect;
        }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList getQuestions()
        {
            List<Question> models = new List<Question>();
                       
            using(SqlConnection connection = new SqlConnection(sOffCampuServer)) 
            {
                using(SqlCommand cmd = new SqlCommand("", connection))
                {
                    connection.Open();
                    cmd.CommandText = "Select QuestionID, Question_ from Question";
                    using(var reader = cmd.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                var m = new Question();
                                m.QuestionID = reader.GetInt32(reader.GetOrdinal("QuestionID"));
                                m.Question_ = reader.GetString(reader.GetOrdinal("Question_"));
                                models.Add(m);
                            }
                        }
                    }
                }
            };

            Microsoft.AspNetCore.Mvc.Rendering.SelectList QuestionSelect = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(models, "QuestionID", "Question_");
            return QuestionSelect;
        }

        [HttpGet]
        public JsonResult GetOptions2(string QuestionID)
        {
            //int QuestionID_ = Convert.ToInt32(QuestionID);

            if (!string.IsNullOrWhiteSpace(QuestionID))
            {               
                IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> options = GetOptions2_(QuestionID);
                return Json(options);
            }
            return null;
        }

        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GetOptions2()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> options = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>()
            {
                new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = null,
                    Text = " "
                }
            };
            return options;
        }
    
        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GetOptions2_(string QuestionID)
        {
            int QuestionID_ = Convert.ToInt32(QuestionID);

            if (!String.IsNullOrWhiteSpace(QuestionID))
            {              
                IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> options = op_ctx.QuestionOptions
                    .OrderBy(o => o.Option)
                    .Where(o => o.QuestionID == QuestionID_)
                    .Select(o =>
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                        {
                            Value = o.QuestionOptionID.ToString(),
                            Text = o.Option
                        }).ToList();
                return new Microsoft.AspNetCore.Mvc.Rendering.SelectList(options, "Value", "Text");                
            }
            return null;
        }         

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList getOptions()
        {

            List<QuestionOption> models = new List<QuestionOption>();
                       
            using(SqlConnection connection = new SqlConnection(sOffCampuServer)) 
            {
                using(SqlCommand cmd = new SqlCommand("", connection))
                {
                    connection.Open();
                    //cmd.Parameters.AddWithValue("@QuestionID", QuestionID_);
                    cmd.CommandText = "Select QuestionOptionID, [Option] from QuestionOption";
                   
                    using(var reader = cmd.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                var m = new QuestionOption();
                                m.QuestionOptionID = reader.GetInt32(reader.GetOrdinal("QuestionOptionID"));
                                m.Option = reader.GetString(reader.GetOrdinal("Option"));
                                models.Add(m);
                            }
                        }
                    }
                }
            };

            Microsoft.AspNetCore.Mvc.Rendering.SelectList OptionsSelect = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(models, "QuestionOptionID", "Option");
            return OptionsSelect;
        }

        public Microsoft.AspNetCore.Mvc.Rendering.SelectList GetQuestions_()
        {          
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> questions = q_ctx.Questions
                .OrderBy(q => q.Question_)
                .Select(q =>
                new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = q.QuestionID.ToString(),
                    Text = q.Question_
                    }).ToList();
                var questions_ = new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                {
                    Value = null,
                    Text = "select Question"
                };
                questions.Insert(0, questions_);
                return new Microsoft.AspNetCore.Mvc.Rendering.SelectList(questions, "Value", "Text");

            //return View(questions);
            
        }

        public ActionResult Index2()
        {          
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> questions = q_ctx.Questions
                .OrderBy(q => q.Question_)
                .Select(q =>
                new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = q.QuestionID.ToString(),
                    Text = q.Question_
                }).ToList();
           
                return View(questions);                        
        }

      
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GetQuestionsTest()
        {
             List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> questions = q_ctx.Questions
                .OrderBy(q => q.Question_)
                .Select(q =>
                new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = q.QuestionID.ToString(),
                    Text = q.Question_
                }).ToList();
           
                return questions; 
        }
       
        public PartialViewResult GetOptionsTest(string QuestionID)
        {
            int QuestionID_ = Convert.ToInt32(QuestionID);

            // if (!String.IsNullOrWhiteSpace(QuestionID))
            // {              
                IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> options = op_ctx.QuestionOptions
                    .OrderBy(o => o.Option)
                    .Where(o => o.QuestionID == QuestionID_)
                    .Select(o =>
                        new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                        {
                            Value = o.QuestionOptionID.ToString(),
                            Text = o.Option
                        }).ToList();
                return PartialView("OptionsPartial", options ); //Microsoft.AspNetCore.Mvc.Rendering.SelectList(options, "Value", "Text");                
           // }
            //return null;
        }       

    }
}

