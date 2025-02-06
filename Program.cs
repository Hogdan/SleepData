// ask for input
Console.WriteLine("Enter 1 to create data file.");
Console.WriteLine("Enter 2 to parse data.");
Console.WriteLine("Enter anything else to quit.");
// input response
string? resp = Console.ReadLine();
string file = "data.txt";

if (resp == "1")
{
    // create data file

    // ask a question
    Console.WriteLine("How many weeks of data is needed?");
    // input the response (convert to int)
    int weeks = Convert.ToInt32(Console.ReadLine());
    // determine start and end date
    DateTime today = DateTime.Now;
    // we want full weeks sunday - saturday
    DateTime dataEndDate = today.AddDays(-(int)today.DayOfWeek);
    // subtract # of weeks from endDate to get startDate
    DateTime dataDate = dataEndDate.AddDays(-(weeks * 7));
    // random number generator
    Random rnd = new();
    // create file
    StreamWriter sw = new("data.txt");

    // loop for the desired # of weeks
    while (dataDate < dataEndDate)
    {
        // 7 days in a week
        int[] hours = new int[7];
        for (int i = 0; i < hours.Length; i++)
        {
            // generate random number of hours slept between 4-12 (inclusive)
            hours[i] = rnd.Next(4, 13);
        }
        // M/d/yyyy,#|#|#|#|#|#|#
        // Console.WriteLine($"{dataDate:M/d/yy},{string.Join("|", hours)}");
        sw.WriteLine($"{dataDate:M/d/yyyy},{string.Join("|", hours)}");
        // add 1 week to date
        dataDate = dataDate.AddDays(7);
    }
    sw.Close();
}
else if (resp == "2")
{
    // TODO: parse data file
    if (File.Exists(file))
    {
        StreamReader sr = new(file);

        while (!sr.EndOfStream)
        {
            int total = 0;
            double average = 0;
            string? line = sr.ReadLine();
            string[] arr = String.IsNullOrEmpty(line) ? [] : line.Split(",");
            DateTime fileDate = DateTime.Parse(arr[0]);
            string[] hArr = arr[1].Split("|");
            for (int i = 0; i < hArr.Length; i++) {
                total += Convert.ToInt32(hArr[i]);
            }
            average = Convert.ToDouble(total)/7;
            Console.WriteLine($"Week of {fileDate:MMM}, {fileDate:dd}, {fileDate:yyyy}");
            Console.WriteLine(" Su Mo Tu We Th Fr Sa Tot Avg");
            Console.WriteLine(" -- -- -- -- -- -- -- --- ---");
            Console.WriteLine($"{hArr[0],3}{hArr[1],3}{hArr[2],3}{hArr[3],3}{hArr[4],3}{hArr[5],3}{hArr[6],3} {total,3} {average:n1}");
            Console.WriteLine();
        }
    }
}
