using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Path = System.IO.Path;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Xps.Packaging;
using Microsoft.Office.Interop.Word;
using System.IO;

namespace FST.TournamentPlanner.UI
{
    public static class WinnerCertificateHelper
    {
        /// <summary>
        /// Generates the winners certificates and stores them to a file
        /// 
        /// The file path is returned
        /// </summary>
        /// </summary>
        /// <param name="name"></param>
        /// <param name="rank"></param>
        /// <param name="tournamentname"></param>
        /// <param name="place"></param>
        /// <param name="date"></param>
        /// <returns>Path to generated certificate</returns>
        public static XpsDocument Generate(string name, int rank,string tournamentname, string place, string date)
        {
            Word.Application wApp = new Word.Application();
            wApp.Visible = false;

            Word.Documents wDocs = wApp.Documents;
            object missing = System.Reflection.Missing.Value;

            Word.Document wDoc = wDocs.Open(
                @"C:\Users\s.stadtler\source\repos\FST01\Sourcecode\FST.TournamentPlanner\FST.TournamentPlanner.UI\WinnerCertificatesTemplate\Urkunde_v2_final.docm",
                missing,
                ReadOnly: false,
                Visible: false,
                Revert:false);
            wApp.WindowState = WdWindowState.wdWindowStateMinimize;
            wDoc.Activate();

            Word.Bookmarks wBookmarks = wDoc.Bookmarks;
            Word.Bookmark wBookmark = wBookmarks["TournamentName"];
            Word.Range wRange = wBookmark.Range;
            wRange.Text = tournamentname;

            Word.Bookmark wBookmark2 = wBookmarks["Name"];
            Word.Range wRange2 = wBookmark2.Range;
            wRange2.Text = name;

            Word.Bookmark wBookmark3 = wBookmarks["Rank"];
            Word.Range wRange3 = wBookmark3.Range;
            wRange3.Text = rank.ToString();

            Word.Bookmark wBookmark4 = wBookmarks["Place"];
            Word.Range wRange4 = wBookmark4.Range;
            wRange4.Text = place;

            Word.Bookmark wBookmark5 = wBookmarks["Date"];
            Word.Range wRange5 = wBookmark5.Range;
            wRange5.Text = date;

            string convertedXpsDoc = string.Concat(Path.GetTempPath(), "\\", Guid.NewGuid().ToString(), ".xps");
            wDoc.SaveAs(convertedXpsDoc, WdSaveFormat.wdFormatXPS);
            XpsDocument xpsDocument = new XpsDocument(convertedXpsDoc, FileAccess.Read);

            wDoc.Close(SaveChanges:false);
            wApp.Quit();

            return xpsDocument;
        }
    }
}
