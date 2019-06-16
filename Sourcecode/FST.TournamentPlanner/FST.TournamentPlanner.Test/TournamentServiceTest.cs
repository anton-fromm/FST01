using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FST.TournamentPlanner.API.Repositories;
using FST.TournamentPlanner.API.Services;
using FST.TournamentPlanner.DB.Contexts;
using Word = Microsoft.Office.Interop.Word;
using Xunit;

namespace FST.TournamentPlanner.Tests
{
    public class TournamentServiceTest
    {
        [Fact]
        public void GenerateWordFile()
        {
            Word._Application wApp = new Word.Application();
            Word.Documents wDocs = wApp.Documents;
            Word._Document wDoc = wDocs.Open(@"C:\Users\s.stadtler\source\repos\FST01\Sourcecode\FST.TournamentPlanner\FST.TournamentPlanner.Test\Urkunde_v1_edit.docx", ReadOnly: false);
            wDoc.Activate();

            Word.Bookmarks wBookmarks = wDoc.Bookmarks;
            Word.Bookmark wBookmark = wBookmarks["Tuniername"];
            Word.Range wRange = wBookmark.Range;
            wRange.Text = "Supertunier";


        }
    }
}