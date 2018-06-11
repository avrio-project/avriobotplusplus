﻿using System.Collections.Generic;
using System.Data.SQLite;

namespace TrtlBotSharp
{
    public partial class TrtlBotSharp
    {
        // Checks if a transaction is a pending tip
        public static bool CheckIfPending(string TransactionHash)
        {
            // Create Sql command
            SQLiteCommand Command = new SQLiteCommand("SELECT tx FROM pendingtips WHERE tx = @tx", Database);
            Command.Parameters.AddWithValue("tx", TransactionHash.ToUpper());

            // Execute command
            using (SQLiteDataReader Reader = Command.ExecuteReader())
                if (Reader.HasRows) return true;
            return false;
        }

        // Gets pending tip payment ids
        public static List<string> GetPendingPaymentIds(string TransactionHash)
        {
            // Create Sql command
            SQLiteCommand Command = new SQLiteCommand("SELECT paymentid FROM pendingtips WHERE tx = @tx", Database);
            Command.Parameters.AddWithValue("tx", TransactionHash.ToUpper());

            // Execute command
            List<string> PaymentIds = new List<string>();
            using (SQLiteDataReader Reader = Command.ExecuteReader())
                while (Reader.Read())
                    PaymentIds.Add(Reader.GetString(0));
            return PaymentIds;
        }

        // Gets pending tip amount
        public static decimal GetPendingAmount(string TransactionHash)
        {
            // Create Sql command
            SQLiteCommand Command = new SQLiteCommand("SELECT amount FROM pendingtips WHERE tx = @tx", Database);
            Command.Parameters.AddWithValue("tx", TransactionHash.ToUpper());

            // Execute command
            using (SQLiteDataReader Reader = Command.ExecuteReader())
                if (Reader.Read())
                    return Reader.GetDecimal(0);
            return 0;
        }

        // Adds a pending tip to the database
        public static void AddPending(string TransactionHash, string PaymentId, decimal Amount)
        {
            // Create Sql command
            SQLiteCommand Command = new SQLiteCommand("INSERT INTO pendingtips (tx, paymentid, amount) VALUES (@tx, @paymentid, @amount)", Database);
            Command.Parameters.AddWithValue("tx", TransactionHash.ToUpper());
            Command.Parameters.AddWithValue("paymentid", PaymentId.ToUpper());
            Command.Parameters.AddWithValue("amount", Amount);

            // Execute command
            Command.ExecuteNonQuery();
        }

        // Removes a pending tip from the database
        public static void RemovePending(string TransactionHash)
        {
            // Create Sql command
            SQLiteCommand Command = new SQLiteCommand("DELETE FROM pendingtips WHERE tx = @tx", Database);
            Command.Parameters.AddWithValue("tx", TransactionHash.ToUpper());

            // Execute command
            Command.ExecuteNonQuery();
        }
    }
}
