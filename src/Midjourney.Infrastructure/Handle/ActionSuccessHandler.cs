﻿using Discord.WebSocket;
using Midjourney.Infrastructure.LoadBalancer;
using Midjourney.Infrastructure.Util;

namespace Midjourney.Infrastructure.Handle
{
    public class ActionSuccessHandler : MessageHandler
    {
        private const string CONTENT_REGEX = "\\*\\*(.*)\\*\\* - (.*?)<@\\d+> \\((.*?)\\)";

        public ActionSuccessHandler(DiscordLoadBalancer discordLoadBalancer, DiscordHelper discordHelper)
        : base(discordLoadBalancer, discordHelper)
        {
        }

        public override int Order() => 99999;

        public override void Handle(IDiscordInstance instance, MessageType messageType, SocketMessage message)
        {
            var content = GetMessageContent(message);
            var parseData = GetParseData(content);
            var parseActionData = GetActionContent(content);

            if (messageType == MessageType.CREATE && HasImage(message)
                && parseData != null && parseActionData != null
                && message.Author.IsBot && message.Author.Username.Contains("journey Bot", StringComparison.OrdinalIgnoreCase))
            {
                FindAndFinishImageTask(instance, parseActionData.Action, parseData.Prompt, message);
            }
        }

        private ContentParseData GetParseData(string content)
        {
            return ConvertUtils.ParseContent(content, CONTENT_REGEX);
        }

        private ContentActionData GetActionContent(string content)
        {
            return ConvertUtils.ParseActionContent(content);
        }
    }
}