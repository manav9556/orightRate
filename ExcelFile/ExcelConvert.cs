using OfficeOpenXml;
using OrightApi.Model;
using System.Globalization;

namespace OrightApi.ExcelFile
{
    public class ExcelConvert
    {
        public List<Rate> ReadExcelData(Stream fileStream, int clientId)
        {
            List<Rate> rateCharts = new List<Rate>();

            using (var package = new ExcelPackage(fileStream))
            {
                var worksheet = package.Workbook.Worksheets[0];

                int rowCount = worksheet.Dimension.Rows;
                int columnCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++)
                {
                    for (int col = 2; col <= columnCount; col++)
                    {
                        Rate rateChart = new Rate
                        {
                            ClientId = clientId,
                            SNF = Convert.ToDecimal(worksheet.Cells[row, 1].Value, CultureInfo.InvariantCulture),
                            FAT = Convert.ToDecimal(worksheet.Cells[1, col].Value, CultureInfo.InvariantCulture),
                            Price = Convert.ToDecimal(worksheet.Cells[row, col].Value, CultureInfo.InvariantCulture)
                        };

                        rateCharts.Add(rateChart);
                    }
                }
            }

            return rateCharts;
        }
    }
}
