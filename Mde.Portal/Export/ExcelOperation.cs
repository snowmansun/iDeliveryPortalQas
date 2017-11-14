using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Data;
using System.Drawing;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace SFA.Portal.Export
{
    public class ExcelOperation
    {
        private ExportConfig config;
        private const int START_ROW = 5;

        public ExcelOperation(ExportConfig excelConfig)
        {
            config = excelConfig;
        }

        /// <summary>
        /// 生成Excel模板
        /// </summary>
        /// <returns></returns>
        public byte[] GenerateTemplate()
        {
            byte[] outputBytes = null;
            if (config.Columns.Count == 0)
                return outputBytes;

            using (ExcelPackage ep = new ExcelPackage())
            {
                ExcelWorksheet ws = ep.Workbook.Worksheets.Add("Sheet1");
                ws.DefaultRowHeight = 22;

                #region 第1行，说明

                ExcelRange row1 = ws.Cells[1, 1, 1, config.Columns.Count];
                ws.Row(1).Height = 50;
                row1.Style.WrapText = true;
                row1.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                row1.Style.Font.Color.SetColor(Color.FromArgb(128, 128, 128));

                //上边框线
                row1.Style.Border.Top.Style = ExcelBorderStyle.Dotted;

                ws.Cells[1, 1].Style.Border.Left.Style = ExcelBorderStyle.Dotted;
                ws.Cells[1, config.Columns.Count].Style.Border.Right.Style = ExcelBorderStyle.Dotted;

                #endregion

                #region 第2行，范例

                ExcelRange row2 = ws.Cells[2, 1, 2, config.Columns.Count];
                row2.Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

                ws.Cells[2, 1].Style.Border.Left.Style = ExcelBorderStyle.Dotted;
                ws.Cells[2, config.Columns.Count].Style.Border.Right.Style = ExcelBorderStyle.Dotted;

                #endregion

                #region 第3行，注意事项

                ExcelRange row3 = ws.Cells[3, 1, 3, config.Columns.Count];
                ws.Row(3).Height = 32;
                row3.Merge = true;
                row3.Value = Resources.ImportError.txt_Tip;
                row3.Style.Font.Color.SetColor(Color.Red);
                row3.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                #endregion

                #region 第4行，字段标题

                ExcelRange row4 = ws.Cells[4, 1, 4, config.Columns.Count];
                ws.Row(4).Height = 32;
                row4.Style.Font.Bold = true;
                row4.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                row4.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                #endregion

                for (int i = 0; i < config.Columns.Count; i++)
                {
                    ExcelRange cellComment = ws.Cells[1, i + 1];
                    cellComment.Value = config.Columns[i].SampleComment;

                    ExcelRange cellSample = ws.Cells[2, i + 1];
                    cellSample.Value = config.Columns[i].Sample;

                    ExcelRange cellColumnName = ws.Cells[4, i + 1];
                    cellColumnName.Value = config.Columns[i].DisplayName;
                    cellColumnName.Style.Fill.PatternType = ExcelFillStyle.Solid;

                    //必填和非必填
                    if (config.Columns[i].Required)
                    {
                        cellComment.Value = Resources.ImportError.txt_Require + cellComment.Value;
                        cellColumnName.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 255, 153));
                    }
                    else
                        cellColumnName.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 255, 204));

                    //自动列宽
                    if (config.Columns[i].Width > 0)
                        ws.Column(i + 1).Width = config.Columns[i].Width;
                    else
                        ws.Column(i + 1).AutoFit();
                }

                ws.Cells[4, 1, 5000, config.Columns.Count].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                ws.Cells[4, 1, 5000, config.Columns.Count].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                ws.Cells[4, 1, 5000, config.Columns.Count].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                ws.Cells[4, 1, 5000, config.Columns.Count].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                ws.Cells[4, 1, 5000, config.Columns.Count].Style.Numberformat.Format = "@";
                //保存
                outputBytes = ep.GetAsByteArray();
            }

            return outputBytes;
        }

        /// <summary>
        /// 从DataTable充填进Excel
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public byte[] FillFromDataTable(DataTable table)
        {
            byte[] inputBytes = GenerateTemplate();
            byte[] outputBytes = null;
            using (ExcelPackage ep = new ExcelPackage())
            {
                ExcelWorksheet ws;
                if (inputBytes != null)
                {
                    ep.Load(new MemoryStream(inputBytes));
                    ws = ep.Workbook.Worksheets[1];
                }
                else
                    ws = ep.Workbook.Worksheets.Add("Sheet1");

                if (table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        for (int j = 0; j < config.Columns.Count(); j++)
                        {
                            string fieldName = config.Columns[j].FieldName;
                            if (table.Columns[fieldName] != null)
                            {
                                ExcelRange cell = ws.Cells[START_ROW + i, j + 1];
                                cell.Value = table.Rows[i][config.Columns[j].FieldName].ToString();
                            }
                        }
                    }

                    int count = START_ROW + table.Rows.Count - 1;
                    if (count < 5000)
                        count = 5000;

                    //ExcelRange dataRange = ws.Cells[START_ROW, 1, START_ROW + table.Rows.Count - 1, config.Columns.Count];
                    ExcelRange dataRange = ws.Cells[START_ROW, 1, count, config.Columns.Count];
                    dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    dataRange.Style.Numberformat.Format = "@";
                }

                outputBytes = ep.GetAsByteArray();
            }

            return outputBytes;
        }

        public DataTable ConvertToDataTable(string excelPath)
        {
            if (!ValidateTemplate(excelPath))
                throw new Exception(ReturnError(ExcelOperationErrorType.IncorrectTemplate, null));

            DataTable dtResult = CreateSchema();
            if (dtResult == null)
                return null;

            using (ExcelPackage ep = new ExcelPackage(new FileInfo(excelPath)))
            {
                ExcelWorksheet ws = ep.Workbook.Worksheets[1];
                for (int rowIndex = 5-1; rowIndex < ws.Dimension.End.Row; rowIndex++)
                {
                    DataRow newRow = dtResult.NewRow();
                    bool skip = true;
                    for (int colIndex = 0; colIndex < ws.Dimension.End.Column; colIndex++)
                    {
                        //if (config.Columns[colIndex].Required && ws.Cells[rowIndex + 1, colIndex + 1].Value == null)
                        //{
                        //    skip = true;
                        //    break;
                        //}
                        newRow[colIndex] = ws.Cells[rowIndex + 1, colIndex + 1].Value;
                    }

                    for (int colIndex = 0; colIndex < dtResult.Columns.Count; colIndex++)
                    {
                        if (newRow[colIndex].ToString().Trim() != "")
                        {
                            skip = false;
                        }
                    }

                    if (skip)
                        continue;

                    dtResult.Rows.Add(newRow);
                }
                
            }

            return dtResult;
        }

        /// <summary>
        /// 创建DataTable的架构
        /// </summary>
        /// <returns></returns>
        private DataTable CreateSchema()
        {
            if (config.Columns.Count == 0)
                return null;

            DataTable dtEmpty = new DataTable(config.TableName);
            foreach (ExportColumn column in config.Columns)
                dtEmpty.Columns.Add(column.FieldName);

            return dtEmpty;
        }

        /// <summary>
        /// 检查Excel应用的模板是否正确
        /// </summary>
        /// <param name="excelPath"></param>
        /// <returns></returns>
        private bool ValidateTemplate(string excelPath)
        {
            if (!File.Exists(excelPath))
                return false;

            using (ExcelPackage ep = new ExcelPackage(new FileInfo(excelPath)))
            {
                //是否存在至少一个工作簿
                if (ep.Workbook.Worksheets.Count == 0)
                    return false;

                ExcelWorksheet ws = ep.Workbook.Worksheets[1];

                if (ws.Dimension == null)
                    return false;

                //检查行列数
                if (ws.Dimension.End.Row < START_ROW-1 || ws.Dimension.End.Column != config.Columns.Count)
                    return false;

                //检查每列的字段名
                for(int i=0;i<config.Columns.Count;i++)
                {
                    if (ws.Cells[START_ROW-1, i + 1].Value.ToString() != config.Columns[i].DisplayName)
                        return false;
                }
            }

            return true;
        }

        private string ReturnError(ExcelOperationErrorType errorType, string desc)
        {
            string returnStr = "";
            switch (errorType)
            {
                case ExcelOperationErrorType.IncorrectTemplate:
                    returnStr = Resources.Message.msg_TemplateError;
                    break;
                case ExcelOperationErrorType.CustomizeError:
                    returnStr = desc;
                    break;
            }
            return returnStr;
        }

        private enum ExcelOperationErrorType
        {
            /// <summary>
            /// 模板错误
            /// </summary>
            IncorrectTemplate,
            /// <summary>
            /// 自定义错误类型
            /// </summary>
            CustomizeError
        }
    }
}