using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Models;
using TheatricalPlayersRefactoringKata.Services;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests
{
    public class StatementPrinterTests
    {
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void TestStatementExampleLegacy()
        {
            var playCategories = new Dictionary<string, IPlayCategory>
            {
                { "tragedy", new TragedyCategory() },
                { "comedy", new ComedyCategory() },
                { "history", new HistoricalCategory() }
            };

            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, "tragedy") },
                { "as-like", new Play("As You Like It", 2670, "comedy") },
                { "othello", new Play("Othello", 3560, "tragedy") }
            };

            var invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40)
                }
            );

            var statementCalculator = new StatementCalculator(playCategories, plays);
            var statementPrinter = new StatementPrinter(statementCalculator);
            var result = statementPrinter.Print(invoice, plays);

            Approvals.Verify(result);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void TestTextStatementExample()
        {
            var playCategories = new Dictionary<string, IPlayCategory>
            {
                { "tragedy", new TragedyCategory() },
                { "comedy", new ComedyCategory() },
                { "history", new HistoricalCategory() }
            };

            var plays = new Dictionary<string, Play>
            {
                { "hamlet", new Play("Hamlet", 4024, "tragedy") },
                { "as-like", new Play("As You Like It", 2670, "comedy") },
                { "othello", new Play("Othello", 3560, "tragedy") },
                { "henry-v", new Play("Henry V", 3227, "history") },
                { "john", new Play("King John", 2648, "history") },
                { "richard-iii", new Play("Richard III", 3718, "history") }
            };

            var invoice = new Invoice(
                "BigCo",
                new List<Performance>
                {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40),
                    new Performance("henry-v", 20),
                    new Performance("john", 39),
                    new Performance("henry-v", 20)
                }
            );

            var statementCalculator = new StatementCalculator(playCategories, plays);
            var statementPrinter = new StatementPrinter(statementCalculator);
            var result = statementPrinter.Print(invoice, plays);

            Approvals.Verify(result);
        }
    }
}
