using TWLumber.Client;

// Adjust the connection string to point at your Tallyworks database.
var options = new TWClientOptions
{
    ConnectionString = "Data Source=dbservername;Database=TWLumber;Integrated Security=True;TrustServerCertificate=True;",
    CommandTimeoutSeconds = 30
};

var client = new TWClient(options);

try
{
    Console.WriteLine("Testing connection to the Tallyworks database...");
    var connected = await client.TestConnectionAsync();
    Console.WriteLine(connected ? "Connection succeeded.\n" : "Connection could not be opened.\n");

    Console.WriteLine("Choose a number to test:");
    Console.WriteLine("  1. Sales");
    Console.WriteLine("  2. Transfers");
    Console.WriteLine("  0. Exit");

    var choice = Console.ReadLine();
    Console.WriteLine("-----------------------------");

    switch (choice)
    {
        case "1":
            Console.WriteLine("==Sales==");
            var sale = await client.Sales.GetBySalesOrderAsync("SAMPLES");
            Console.WriteLine(sale is null
                ? "  Sales order SAMPLES: not found."
                : $"  Sales order SAMPLES: {sale.CustCode}, status {sale.Status}, entered {sale.EntryDate:d}, items {sale.SaleItems.Count}.");
            break;
        case "2":
            Console.WriteLine("==Transfers==");
            var transfer = await client.Transfers.GetByTransferIdAsync(99);
            Console.WriteLine(transfer is null
                ? "  Transfer 99: not found."
                : $"  Transfer: {transfer.TransferId} links to Run '{transfer.Run}'");
            break;
        case "0":
            break;
        default:
            break;
    }

    Console.WriteLine("Press any key to close");
    Console.Read();
}
catch (Exception ex)
{
    Console.WriteLine($"Failed: {ex.Message}");
    Console.WriteLine("Press any key to close");
    Console.Read();
}
