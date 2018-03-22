namespace Program
{
    public static class LuckyVoyageDB
    {
        static public void DeleteFromDB(string uuid)
        {
            using (DBConnect connection = new DBConnect())
            {
                connection.NonResultQuery($"DELETE FROM `users` WHERE `users`.`unique_id` = '{uuid}'");
                connection.NonResultQuery($"DELETE FROM `sailors` WHERE `sailors`.`unique_id` = '{uuid}'");
            }
        }
    }
}
