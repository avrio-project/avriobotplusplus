using Discord.Commands;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace TrtlBotSharp
{
    public partial class Commands : ModuleBase<SocketCommandContext>
    {
        
	[Command("reward")]
	public async Task CalculateReward(decimal hashvalue, [Remainder]string Remainder = "")
	{

     	if (!(hashvalue > 0))
		{
	     	//await Context.Message.Author.SendMessageAsync(string.Format("No valid Hashrate entered !", TrtlBotSharp.coinName));
		}
	else
		{
	   		decimal diff = TrtlBotSharp.GetDifficulty(); 
	   		decimal avgsec = (diff / hashvalue);
	   		
			// max 720 blocks / day fehlt noch 
			decimal blockperday = (86400 / avgsec);
	   		decimal rewardperday = (blockperday * 1862621)/10000;
			
			

	   		await ReplyAsync(string.Format("Depending on difficulty and your hashrate you might find around **{0:N2}** Blocks per Day \n this  corresponds to a reward of **{1:N4}** {2} per day", blockperday, rewardperday, TrtlBotSharp.coinSymbol));
        
		}			
	}

	[Command("hashrate")]
        public async Task HashrateAsync([Remainder]string Remainder = "")
        {
	    decimal Hashrate = TrtlBotSharp.GetHashrate();	
            // Send reply
            await ReplyAsync("The current global hashrate is **" + TrtlBotSharp.FormatHashrate(Hashrate) + "**");
        }

        [Command("difficulty")]
        public async Task DifficultyAsync([Remainder]string Remainder = "")
        {
	    decimal Difficulty = TrtlBotSharp.GetDifficulty();
            // Send reply
            await ReplyAsync(string.Format("The current difficulty is **{0:N0}**", Difficulty));
        }

        [Command("height")]
        public async Task HeightAsync([Remainder]string Remainder = "")
        {
           decimal Height = TrtlBotSharp.GetHeight();

            // Send reply
            await ReplyAsync(string.Format("The current block height is **{0:N0}**", Height));
        }

        [Command("supply")]
        public async Task SupplyAsync([Remainder]string Remainder = "")
        {
            // Get supply
            decimal Supply = TrtlBotSharp.GetSupply();

            // Send reply
            await ReplyAsync(string.Format("The current circulating supply is **{0:N4}** {1}", Supply, TrtlBotSharp.coinSymbol));
        }

	[Command("dynamit")]
        public async Task DynamitAsync([Remainder]string Remainder = "")
        {
            // Get supply
            decimal Supply = TrtlBotSharp.GetSupply();
	    decimal Height = TrtlBotSharp.GetHeight();
	    decimal Hashrate = TrtlBotSharp.GetHashrate();
            decimal Difficulty = TrtlBotSharp.GetDifficulty(); 
	   
	    string Message =string.Format(  "The current block height is **{0:N0}**" + 
		   	     "\nThe current global hashrate is **" + TrtlBotSharp.FormatHashrate(Hashrate) + "**" + 
			     "\nThe current difficulty is **{1:N0}**" + 
			     "\nThe current circulating supply is **{2:N4}** {3}", Height, Difficulty, Supply, TrtlBotSharp.coinSymbol ); 
	    
	    await ReplyAsync(string.Format(Message));

	}
    }
}
