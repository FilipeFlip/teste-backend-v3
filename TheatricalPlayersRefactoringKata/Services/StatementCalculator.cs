﻿using System;
using System.Collections.Generic;
using System.Linq;
using TheatricalPlayersRefactoringKata.Models;

namespace TheatricalPlayersRefactoringKata.Services
{
    public class StatementCalculator
    {
        private readonly Dictionary<string, IPlayCategory> _playCategories;
        private readonly Dictionary<int, Play> _plays;

        public StatementCalculator(Dictionary<string, IPlayCategory> playCategories, Dictionary<int, Play> plays)
        {
            _playCategories = playCategories;
            _plays = plays;
        }

        public string Calculate(Invoice invoice)
        {
            var result = $"Statement for {invoice.Customer}\n";
            foreach (var performance in invoice.Performances)
            {
                var play = _plays[performance.PlayId];
                var thisAmount = CalculateAmount(play, performance.Seats);
                var thisPoints = CalculatePoints(play, performance.Seats);

                result += $"- {play.Title}: {thisAmount:C} ({performance.Seats} seats)\n";
                result += $"  Credits: {thisPoints}\n";
            }

            result += $"Total amount: {CalculateTotalAmount(invoice.Performances):C}\n";
            result += $"Total points: {CalculateTotalPoints(invoice.Performances)}\n";

            return result;
        }

        public decimal CalculateAmount(Play play, int seats)
        {
            var category = _playCategories[play.Category];
            return category.CalculateAmount(seats, 0);
        }

        public int CalculatePoints(Play play, int seats)
        {
            var category = _playCategories[play.Category];
            return category.CalculatePoints(seats);
        }

        public decimal CalculateTotalAmount(List<Performance> performances)
        {
            return performances.Sum(performance =>
            {
                var play = _plays[performance.PlayId];
                return CalculateAmount(play, performance.Seats);
            });
        }

        public int CalculateTotalPoints(List<Performance> performances)
        {
            return performances.Sum(performance =>
            {
                var play = _plays[performance.PlayId];
                return CalculatePoints(play, performance.Seats);
            });
        }
    }
}
