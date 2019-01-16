using Discord;
using Discord.Commands;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace TrtlBotSharp
{
    public partial class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task HelpAsync([Remainder]string Remainder = "")
        {
            // Begin building a response
            EmbedBuilder Response = new EmbedBuilder();
            Response.WithTitle("Help");
            string Output = "";

            // Requesting additional help
            /*if (Remainder.ToLower() == "faucet")
            {
                Response.Title += string.Format(" - {0}faucet", TrtlBotSharp.botPrefix);
                Response.AddField("Usage:", string.Format("{0}faucet", TrtlBotSharp.botPrefix));
                Response.AddField("Description:", "Gives faucet information, including the donation address, a link to the faucet, and how much it has left");
            }
             
	    else if (Remainder.ToLower() == "hashrate")
            */ 
	    
	    if (Remainder.ToLower() == "hashrate") 
	    {
                Response.Title += string.Format(" - {0}hashrate", TrtlBotSharp.botPrefix);
                Response.AddField("Usage:", string.Format("{0}hashrate", TrtlBotSharp.botPrefix));
                Response.AddField("Description:", "Gives the current network hashrate");
            }
	    
	    else if (Remainder.ToLower() == "difficulty")
            {
                Response.Title += string.Format(" - {0}difficulty", TrtlBotSharp.botPrefix);
                Response.AddField("Usage:", string.Format("{0}difficulty", TrtlBotSharp.botPrefix));
                Response.AddField("Description:", "Gives the current network difficulty");
            }
            else if (Remainder.ToLower() == "height")
            {
                Response.Title += string.Format(" - {0}height", TrtlBotSharp.botPrefix);
                Response.AddField("Usage:", string.Format("{0}height", TrtlBotSharp.botPrefix));
                Response.AddField("Description:", "Gives the current network height");
            }
            else if (Remainder.ToLower() == "supply")
            {
                Response.Title += string.Format(" - {0}supply", TrtlBotSharp.botPrefix);
                Response.AddField("Usage:", string.Format("{0}supply", TrtlBotSharp.botPrefix));
                Response.AddField("Description:", string.Format("Gives the total circulating supply of {0}", TrtlBotSharp.coinSymbol));
            }
            else if (Remainder.ToLower() == "reward")
	    {
	        Response.Title += string.Format(" - {0}reward", TrtlBotSharp.botPrefix);
	        Response.AddField("Usage:", string.Format("{0}reward <Value in H/s>", TrtlBotSharp.botPrefix));
                Response.AddField("Description:", string.Format("Calculates a theoretical reward to the given \nhashrate at current difficulty"));
	    }
	    else if (Remainder.ToLower() == "registerwallet")
            {
                Response.Title += string.Format(" - {0}registerwallet", TrtlBotSharp.botPrefix);
                Response.AddField("Usage:", string.Format("{0}registerwallet <{1} Address>", TrtlBotSharp.botPrefix, TrtlBotSharp.coinSymbol));
                Response.AddField("Description:", "Registers your address with the bot so you may send and recieve tips");
            }
            else if (Remainder.ToLower() == "updatewallet")
            {
                Response.Title += string.Format(" - {0}updatewallet", TrtlBotSharp.botPrefix);
                Response.AddField("Usage:", string.Format("{0}updatewallet <{1} Address>", TrtlBotSharp.botPrefix, TrtlBotSharp.coinSymbol));
                Response.AddField("Description:", "Updates your registered wallet to a new address");
            }
            else if (Remainder.ToLower() == "wallet")
            {
                Response.Title += string.Format(" - {0}uwallet", TrtlBotSharp.botPrefix);
                Response.AddField("Usage:", string.Format("{0}wallet\n{0}wallet <{1} Address>", TrtlBotSharp.botPrefix, TrtlBotSharp.coinSymbol));
                Response.AddField("Description:", "Gets a specified user's registered wallet address, or your own if no address is specified");
            }
            else if (Remainder.ToLower() == "deposit")
            {
                Response.Title += string.Format(" - {0}deposit", TrtlBotSharp.botPrefix);
                Response.AddField("Usage:", string.Format("{0}deposit", TrtlBotSharp.botPrefix));
                Response.AddField("Description:", string.Format("DMs you with your deposit information, including the address to send to, " +
                    "and the payment ID you **must** use when sending {0}", TrtlBotSharp.coinSymbol));
            }
            else if (Remainder.ToLower() == "withdraw")
            {
                Response.Title += string.Format(" - {0}withdraw", TrtlBotSharp.botPrefix);
                Response.AddField("Usage:", string.Format("{0}withdraw <Amount of {1}>", TrtlBotSharp.botPrefix, TrtlBotSharp.coinSymbol));
                Response.AddField("Description:", "Withdraws a specified amount from your tip jar into your registered wallet");
            }
            else if (Remainder.ToLower() == "balance")
            {
                Response.Title += string.Format(" - {0}balance", TrtlBotSharp.botPrefix);
                Response.AddField("Usage:", string.Format("{0}balance", TrtlBotSharp.botPrefix, TrtlBotSharp.coinSymbol));
                Response.AddField("Description:", "Gets your current tip jar balance");
            }
            else if (Remainder.ToLower() == "tip")
            {
                Response.Title += string.Format(" - {0}tip", TrtlBotSharp.botPrefix);
                Response.AddField("Usage:", string.Format("{0}tip <Amount of {1}> @Users1 @User2...\n{0}tip <Amount of {1}> <{1} Address>", 
                    TrtlBotSharp.botPrefix, TrtlBotSharp.coinSymbol));
                Response.AddField("Description:", string.Format("Sends a tip of a specified amount to one or more users *or* a specified {0} address", 
                    TrtlBotSharp.coinSymbol));
            }
            else if (Remainder.ToLower() == "redirecttips")
            {
                Response.Title += string.Format(" - {0}redirecttips", TrtlBotSharp.botPrefix);
                Response.AddField("Usage:", string.Format("{0}redirecttips\n{0}redirecttips <True or False>", TrtlBotSharp.botPrefix));
                Response.AddField("Description:", "Sets whether you'd like to have tips sent to you to go directly to your registered wallet " +
                    "(default) or redirected into your tip jar balance");
            }

            else if (Remainder.ToLower() == "price" && (Context.Guild == null || !TrtlBotSharp.marketDisallowedServers.Contains(Context.Guild.Id)))
            {
                Response.Title += string.Format(" - {0}price", TrtlBotSharp.botPrefix);
                Response.AddField("Usage:", string.Format("{0}price", TrtlBotSharp.botPrefix));
                Response.AddField("Description:", string.Format("Gives the current price of {0} in USD and BTC", TrtlBotSharp.coinSymbol));
            }
            else if (Remainder.ToLower() == "mcap" && (Context.Guild == null || !TrtlBotSharp.marketDisallowedServers.Contains(Context.Guild.Id)))
            {
                Response.Title += string.Format(" - {0}mcap", TrtlBotSharp.botPrefix);
                Response.AddField("Usage:", string.Format("{0}mcap", TrtlBotSharp.botPrefix));
                Response.AddField("Description:", string.Format("Gives {0}'s current market capitalization", TrtlBotSharp.coinSymbol));
            }

            // No requested command
            else
            {
                Output += "Informational:\n";
                Output += " {0}help\tLists all available commands\n\n";
                //Output += "  faucet\tGives faucet information\n";
                Output += "Network:\n";
                Output += " {0}hashrate\tReturns current network hashrate\n";
                Output += " {0}difficulty\tReturns current network difficulty\n";
                Output += " {0}height\tReturns current highest blocknumber\n";
                Output += " {0}supply\tReturns current circulating supply\n";
                Output += " {0}dynamit\tReturns current network stats\n";
		Output += " {0}reward\tCalculates reward to given H/s value\n\n"; 
		/*if (Context.Guild == null || !TrtlBotSharp.marketDisallowedServers.Contains(Context.Guild.Id))
                {
                    Output += "Market:\n";
                    Output += "  price\tGives current price\n";
                    Output += "  mcap\tGives current global marketcap\n";
                }
               */ 
		Output += "Tipping:\n";
                Output += " {0}registerwallet\tRegisters your wallet address\n\twith the tip bot\n";
                Output += " {0}updatewallet\tUpdates your registered wallet\n";
                Output += " {0}wallet\tReturns your own wallet address\n";
                Output += " {0}deposit\tGives information on how to deposit\n\tinto your tipping balance\n";
                Output += " {0}withdraw\tWithdraws a specified amount from\n\tyour tip jar to your registered wallet\n";
                Output += " {0}balance\tGives your current tip jar balance\n";
                Output += " {0}tip\tTips one or more users\n\ta specified amount\n";
                Output += " {0}redirecttips\tSets whether you'd like tips sent\n\tdirectly to your wallet or\n\tredirected back into your tip jar";
                Output = string.Format("\n```" + TrtlBotSharp.Prettify(Output) + "```**Note:** You can use *{0}help <Name of Command (wihout \"{0}\")>* for " +
                    "additional help with any command. \nNote: usage of commands **without < > -brackets** !", TrtlBotSharp.botPrefix);
                Response.WithDescription(Output);
                Response.WithTitle("Available Commands:");
            }

            // Send reply
            await ReplyAsync("", false, Response);
        }

	/*
        [Command("faucet")]
        public async Task FaucetAsync([Remainder]string Remainder = "")
        {
            // Get faucet balance
            JObject FaucetBalance = Request.GET(TrtlBotSharp.faucetEndpoint);
            if (FaucetBalance.Count < 1)
            {
                await ReplyAsync("Failed to connect to faucet");
                return;
            }

            // Begin building a response
            var Response = new EmbedBuilder();
            Response.WithTitle(string.Format("This faucet has {0:N} {1} left", (decimal)FaucetBalance["available"], TrtlBotSharp.coinSymbol));
            Response.WithUrl(TrtlBotSharp.faucetHost);
            Response.Description = "```Donations:\n" + TrtlBotSharp.faucetAddress + "```\n";

            // Send reply
            await ReplyAsync("", false, Response);
        }
	*/
    }
}
