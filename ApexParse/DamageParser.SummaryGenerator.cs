﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexParse
{
    partial class DamageParser
    {
        private string summary_generateSummary()
        {
            StringBuilder result = new StringBuilder($"ApexParse Summary Generated on {DateTime.Now:F}");
            result.AppendLine($"{GetElapsedTime():h\\:mm\\:ss} | Total Damage : {TotalFriendlyDamage:#,##0} | Total DPS : {FormatDPSNumber(TotalFriendlyDPS)}");
            result.AppendLine().AppendLine()
            .AppendLine("[ Overview ]")
            .AppendLine();

            summary_AddAllPlayersOverview(result);
            summary_AddEachPlayerOverview(result);
            return result.ToString();
        }

        private void summary_AddEachPlayerOverview(StringBuilder result)
        {
            foreach (var player in Players.OrderByDescending(p => p.FilteredDamage.TotalDamage))
            {
                result.Append(player.GenerateDamageSummary());
                result.AppendLine();
            }
            if (IsZanverseSplit && ZanversePlayer != null)
            {
                result.Append(ZanversePlayer.GenerateDamageSummary());
                result.AppendLine();
            }
        }

        private void summary_AddAllPlayersOverview(StringBuilder result)
        {
            foreach (var player in Players.OrderByDescending(p => p.FilteredDamage.TotalDamage))
            {
                summary_GenerateAllPlayerSummaryForPlayer(player, result);
            }
            if (IsZanverseSplit && ZanversePlayer != null)
            {
                summary_GenerateAllPlayerSummaryForPlayer(ZanversePlayer, result);
            }
        }

        private void summary_GenerateAllPlayerSummaryForPlayer(PSO2Player player, StringBuilder result)
        {
            result.AppendLine($"> {player.Name}");
            result.Append($"> Contrib : {player.RelativeDPS * 100:#00.00}% dmg")
            .Append("\t|\t")
            .Append($"Dealt : {player.FilteredDamage.TotalDamage:#,##0}")
            .Append("\t|\t")
            .Append($"Taken : {player.DamageTaken.TotalDamage:#,##0}")
            .Append("\t|\t")
            .Append($"{player.FilteredDamage.TotalDPS:#,##0.##} DPS")
            .Append("\t|\t")
            .Append($"JA : {player.FilteredDamage.JustAttackPercent:#00.00}%")
            .Append("\t|\t")
            .Append($"Crit : {player.FilteredDamage.CritPercent:#00.00}%")
            .Append("\t|\t")
            .Append($"Max : {player.MaxHit:#,##0} ({player.MaxHitName})")
            .AppendLine()
            .AppendLine();
        }
    }
}
