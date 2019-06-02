﻿// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace FST.TournamentPlanner.UI.Model.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;

    public partial class Match
    {
        /// <summary>
        /// Initializes a new instance of the Match class.
        /// </summary>
        public Match() { }

        /// <summary>
        /// Initializes a new instance of the Match class.
        /// </summary>
        public Match(int? id = default(int?), Team teamOne = default(Team), Team teamTwo = default(Team), PlayArea playArea = default(PlayArea), DateTime? start = default(DateTime?), DateTime? end = default(DateTime?), int? teamOneScore = default(int?), int? teamTwoScore = default(int?), int? matchState = default(int?), Match firstPredecessor = default(Match), Match secondPredecessor = default(Match), Team looser = default(Team), Team winner = default(Team))
        {
            Id = id;
            TeamOne = teamOne;
            TeamTwo = teamTwo;
            PlayArea = playArea;
            Start = start;
            End = end;
            TeamOneScore = teamOneScore;
            TeamTwoScore = teamTwoScore;
            MatchState = matchState;
            FirstPredecessor = firstPredecessor;
            SecondPredecessor = secondPredecessor;
            Looser = looser;
            Winner = winner;
        }

        /// <summary>
        /// Id of this match
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int? Id { get; private set; }

        /// <summary>
        /// First Team in this match
        /// </summary>
        [JsonProperty(PropertyName = "teamOne")]
        public Team TeamOne { get; private set; }

        /// <summary>
        /// Second Team in this match
        /// </summary>
        [JsonProperty(PropertyName = "teamTwo")]
        public Team TeamTwo { get; private set; }

        /// <summary>
        /// Play area this match is held
        /// </summary>
        [JsonProperty(PropertyName = "playArea")]
        public PlayArea PlayArea { get; private set; }

        /// <summary>
        /// Start time of this match
        /// </summary>
        [JsonProperty(PropertyName = "start")]
        public DateTime? Start { get; private set; }

        /// <summary>
        /// End time of this match
        /// </summary>
        [JsonProperty(PropertyName = "end")]
        public DateTime? End { get; private set; }

        /// <summary>
        /// Score of team one
        /// </summary>
        [JsonProperty(PropertyName = "teamOneScore")]
        public int? TeamOneScore { get; private set; }

        /// <summary>
        /// Score of team two
        /// </summary>
        [JsonProperty(PropertyName = "teamTwoScore")]
        public int? TeamTwoScore { get; private set; }

        /// <summary>
        /// State of this match
        /// </summary>
        [JsonProperty(PropertyName = "matchState")]
        public int? MatchState { get; private set; }

        /// <summary>
        /// First predecessor game
        /// </summary>
        [JsonProperty(PropertyName = "firstPredecessor")]
        public Match FirstPredecessor { get; private set; }

        /// <summary>
        /// Second predecessor game
        /// </summary>
        [JsonProperty(PropertyName = "secondPredecessor")]
        public Match SecondPredecessor { get; private set; }

        /// <summary>
        /// Looser team of this match
        /// </summary>
        [JsonProperty(PropertyName = "looser")]
        public Team Looser { get; private set; }

        /// <summary>
        /// Winner team of this match
        /// </summary>
        [JsonProperty(PropertyName = "winner")]
        public Team Winner { get; private set; }

    }
}