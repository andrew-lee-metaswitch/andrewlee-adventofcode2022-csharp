using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;

namespace andrewlee_adventofcode2022_csharp
{
    public class Day2 : Day
    {
        public enum RPS
        {
            Rock,
            Paper,
            Scissors
        }

        private RPS WhatBeats(RPS r)
        {
            return r switch
            {
                RPS.Rock => RPS.Paper,
                RPS.Paper => RPS.Scissors,
                RPS.Scissors => RPS.Rock,
                _ => throw new Exception("Unexpected RPS Value"),
            };
        }

        private RPS WhatLoses(RPS r)
        {
            return r switch
            {
                RPS.Rock => RPS.Scissors,
                RPS.Paper => RPS.Rock,
                RPS.Scissors => RPS.Paper,
                _ => throw new Exception("Unexpected RPS Value"),
            };
        }

        private int ScoreRound(RPS opponent, RPS you)
        {
            // Score =
            // Your selection (1 for Rock, 2 for Paper, and 3 for Scissors)
            // plus outcome (0 if you lost, 3 if the round was a draw, and 6 if you won).
            var roundScore = 0;
            if (opponent.ToString() == you.ToString())
            {
                roundScore = 3;
            }
            else if (
                (WhatBeats(opponent).ToString() == you.ToString()))
            {
                roundScore = 6;
            }

            roundScore += you switch
            {
                RPS.Rock => 1,
                RPS.Paper => 2,
                RPS.Scissors => 3,
                _ => throw new Exception("Unexpected RPS Value"),
            };
            return roundScore;
        }

        public override int Part1Solve()
        {
            string[] inputs = LoadInputs("202202");
            int strategyGuideScore = 0;
            foreach (string input in inputs)
            {
                string pattern = @"^(?<opponent>[ABC]) (?<you>[XYZ])$";
                RPS opponent = (Regex.Match(input, pattern).Groups[1].Value) switch
                {
                    "A" => RPS.Rock,
                    "B" => RPS.Paper,
                    "C" => RPS.Scissors,
                    _ => throw (new Exception())
                } ;
 
                RPS you = (Regex.Match(input, pattern).Groups[2].Value) switch
                {
                    "X" => RPS.Rock,
                    "Y" => RPS.Paper,
                    "Z" => RPS.Scissors,
                    _ => throw (new Exception())
                };

                strategyGuideScore += ScoreRound(opponent, you);
            }
            return strategyGuideScore;
        }

        public override int Part2Solve()
        {
            string[] inputs = LoadInputs("202202");
            int strategyGuideScore = 0;
            foreach (string input in inputs)
            {
                string pattern = @"^(?<opponent>[ABC]) (?<you>[XYZ])$";
                RPS opponent = (Regex.Match(input, pattern).Groups[1].Value) switch
                {
                    "A" => RPS.Rock,
                    "B" => RPS.Paper,
                    "C" => RPS.Scissors,
                    _ => throw (new Exception())
                };
                RPS you = (Regex.Match(input, pattern).Groups[2].Value) switch
                {
                    "X" => WhatLoses(opponent),
                    "Y" => opponent,
                    "Z" => WhatBeats(opponent),
                    _ => throw (new Exception())
                };

                strategyGuideScore += ScoreRound(opponent, you);
            }
            return strategyGuideScore;
        }
    }
}
