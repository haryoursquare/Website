﻿namespace Recipes.SeedData
{
    public static partial class BlogPostsProgramming
    {
        //"Playing with Google Search Results "
        public const string content_07092012_b = "<p>You will need:<ul><li><a href=\"http://htmlagilitypack.codeplex.com/\">HtmlAgilityPack</a> HTML Parser</li><li>Development environment</li><li>Internet connection</li></ul></p><p>Create a Visual Studio project, for example C# Windows Forms application. Drop a <strong>TextBox</strong>, a <strong>Button</strong> and a <strong>ListView</strong> on the form. Creat a class for the methods to be used, let's say <strong>Helper.cs</strong>. First, I'm using the <strong>System.Net.Webclient</strong> to call Google and get a page of search results.</p><pre class=\"brush: csharp\">" +

            @"public static WebClient webClient = new WebClient();

            public static string GetSearchResultHtlm(string keywords)
            {
                StringBuilder sb = new StringBuilder(""http://www.google.com/search?q="");
                sb.Append(keywords);
                return webClient.DownloadString(sb.ToString());
            }" + "</pre>";
        public const string content_07092012_r = "<p>The string that is returned is the html of the first page of the Google search for the string that is passed to the method. Opened in the web browser, it will look something like this</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/07092012_Google_Search_Results.png\" alt=\"Google Search Results\" /></div><p align=\"center\">Google search result page</p><p>What I want to extract is the actual links, which are marked in red on the screenshot above. Here I'm going to use <strong>HtmlAgilityPack</strong> to load the string into the <strong>HtmlDocument</strong> object. After the string is loaded, I will use a simple LINQ query to extract the nodes that match certain conditions: They are html links (a href), and the URL of the link contains either <strong>\"/url?\"</strong> or <strong>\"?url=\"</strong>. By this point, I get quite and unreadable list of values.</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/07092012_Result_list.png\" alt=\"Result list\" /></div><p align=\"center\">Raw URLs</p><p>To bring it into readable form, I'll match it to a regular expression and then load the results into the <strong>ListView</strong>. Here is the code:</p><pre class=\"brush: csharp\">" +

            @"public static Regex extractUrl = new Regex(@""[&?](?:q|url)=([^&]+)"", RegexOptions.Compiled);

            public static List&lt;String&gt; ParseSearchResultHtml(string html)
            {
                List&lt;String&gt; searchResults = new List&lt;string&gt;();

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);

                var nodes = (from node in doc.DocumentNode.SelectNodes(""//a"")
                             let href = node.Attributes[""href""]
                             where null != href
                             where href.Value.Contains(""/url?"") || href.Value.Contains(""?url="")
                             select href.Value).ToList();

                foreach (var node in nodes)
                {
                    var match = extractUrl.Match(node);
                    string test = HttpUtility.UrlDecode(match.Groups[1].Value);
                    searchResults.Add(test);
                }

                return searchResults;
            }" + "</pre><p>Here is the result:</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/07092012_test_form.png\" alt=\"test form\" /></div><p align=\"center\">Final Results</p><p>I'm not quite sure why this may be useful, but as an exercise it is possible to add an option to parse through a certain number of pages, rather than just the first page. But if you try to run those queries in an automated mode, Google will soon start serving 503 errors to you.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_07092012_k = "C# MVC Google HtmlAgilityPack WebClient";
        public const string content_07092012_d = "Using WebClient control to retrieve and parse search results from Google, and then display them in a user-friendly format";

        //"jQuery Show/Hide Toggle"
        public const string content_03102012_b = "<p>When I have a long list on a web page, I would like to give the user an option to hide it. Turns out that is simple to do with a little bit of <strong>jQuery</strong>. All the contents that I would like to hide or show would go into a div element. Next to it, I will add a link to toggle the hide/show behaviour. Here's how it looks in my code:</p><pre class=\"brush: xml\">" +
            @"&lt;div class=""buttonlink""&gt;
             &lt;a href=""#"" class=""show_hide""&gt;Show/Hide List&lt;/a&gt;
             &lt;/div&gt;
            &lt;div class=""slidingDiv""&gt;
            @foreach (var item in Model.Recipes)
            {
              &lt;ol&gt;
                &lt;li class=""styled""&gt;
                &lt;div class=""display-button""&gt;@Html.ActionLink(""Edit"", ""Edit"", new { id = item.RecipeID })
                &lt;/div&gt;
                &lt;div class=""display-button""&gt;@Html.ActionLink(""Details"", ""Details"", new { id = item.RecipeID })&lt;/div&gt;
                &lt;div class=""display-button""&gt;@Html.ActionLink(""Delete"", ""Delete"", new { id = item.RecipeID })&lt;/div&gt;
                &lt;div class=""display-info""&gt;@item.RecipeName&lt;/div&gt;
                &lt;/li&gt;<br/>
                &lt;/ol&gt;<br/>
            }<br/>
            &lt;a href=""#"" cla<br/>ss=""show_hide""&gt;Hide&lt;/a&gt;
            &lt;/div&gt;<br/>" +
            "</pre>";
        public const string content_03102012_r = "<p>Here is the jQuery function that is called when the link is clicked</p><pre class=\"brush: jscript\">" +
            @"$(document).ready(function () {<br/>
                    $("".slidingDiv"").hide();<br/>
                    $("".show_hide"").show();<br/>
                    $('.show_hide').click(function () {<br/>
                    $("".slidingDiv"").slideToggle();<br/>
                    });<br/>
                });<br/>" +
            "</pre><p>And the styles for the classes that I've just introduced above.</p><pre class=\"brush: css\">" +
            @".slidingDiv {<br/>
                    height:300px;<br/>
                    padding:20px;<br/>
                    margin-top:35px;<br/>
                    margin-bottom:10px;<br/>
                }<br/>
                .show_hide {<br/>
                    display:none;<br/>
                }<br/>" +
            "</pre><p>That's it.</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/03102012_Show-hide_show.png\" alt=\"Show-hide show\" /></div><p align=\"center\">Shown</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/03102012_Show-hide_hide.png\" alt=\"Show-hide hide\" /></div><p align=\"center\">Hidden</p>" +
            "<p><strong>References</strong></p><a href=\"http://stackoverflow.com/questions/3394996/jquery-show-hide-div\">jquery show/hide div</a><br/><a href=\"http://papermashup.com/simple-jquery-showhide-div/\">Simple jQuery Show/Hide Div</a><br/><a href=\"http://jsfiddle.net/mattball/rPYLK/\">Fiddle</a><br/><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_03102012_k = "C# MVC jQuery show hide div";
        public const string content_03102012_d = "A simple technique to show and hide a div using jQuery";

        //"Securing Access to Windows 7 Folder from Everyone but a Single User"
        public const string content_12082012_b = "<p>Today I had to perform a fairly specific task: restrict access to a Windows 7 folder to a single user. I think I found the way to do it properly, and it is not a straightforward task. Before I forget, I might keep a record of all actions required because I did not find a clear sequence anywhere on the net. It will only take 10 easy steps.</p>";
        public const string content_12082012_r = "<p>Let's assume there is a folder called <strong>Bob's Documents</strong> where only Bob, and not even the <strong>Admin</strong>, should have access.</p><ol><li>Right-click on <strong>Bob's Documents</strong> and select <strong>Properties</strong></li>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/12082012_Bob_Documents.png\" alt=\"Bob Documents\" /></div>" +
            "<p align=\"center\">Select Bob's Documents <strong>Properties</strong></p><li><strong>Bob's Documents Properties</strong> window will open. Switch to <strong>Security</strong> tab and click <strong>Advanced</strong> button.</li>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/12082012_Bob_Documents_Security.png\" alt=\"Bob Documents Security\" /></div>"
            + "<p align=\"center\">Bob's Documents Properties</p><li><strong>Advanced Security Settings for Bob's Documents</strong> will open. On the <strong>Permissions</strong> tab, Click <strong>Change Permissions</strong> button.</li>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/12082012_Change_permissions.png\" alt=\"Change permissions\" /></div>"
            + "<p align=\"center\">Advanced Security Settings for Bob's Documents</p><li>Another window will open. Unfortunately, it's too called <strong>Advanced Security Settings for Bob's Documents</strong>, adding to confusion. In this new window, untick <strong>Include inheritable permissions from this object's parent</strong> - that will simplify things a lot, because we only care about permissions to this folder, not its parent folder.</li>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/12082012_Include_inheritable_permissions.png\" alt=\"Include inheritable permissions\" /></div>"
            + "<p align=\"center\">Advanced Security Settings for Bob's Documents - but not the same one!</p><li>As soon as the chechbox is unticked, a warning called <strong>Windows Security</strong> will pop up. Since we're getting rid of parent permissions, click <strong>Remove</strong>.</li>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/12082012_Windows_security_remove.png\" alt=\"Windows security remove\" /></div>" +
            "<p align=\"center\"><strong>Windows Security</strong> warning</p><li>All permissions should have disappeared from the <strong>Permission entries</strong>. Now click <strong>Add</strong>.</li><li><strong>Select User or Group</strong> window will open. In <strong>Enter the object name to select</strong>, type Bob and click <strong>Check Names</strong> to make sure there is no typo. "
            + "Bob's name should resolve to PCName\\Bob.</li>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/12082012_Select_user_or_group.png\" alt=\"Select user or group\" /></div>" +
            "<p align=\"center\">Select User or Group</p><li>Click <strong>OK</strong>. Now the <strong>Permissions Entry for Bob's Documents</strong> window will pop up. Let's give Bob full control - click the checkbox across from <strong>Full Control</strong> under <strong>Allow</strong>. All other checkboxes under <strong>Allow</strong> will select automatically. Click <strong>OK</strong> to close this window.</li>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/12082012_Permission_entry.png\" alt=\"Permission entry\" /></div>" +
            "<p align=\"center\">Permissions Entry for Bob's Documents</p><li>About done. Click <strong>OK</strong> in <strong>Advanced Security Settings for Bob's Documents</strong> to close it, and in another <strong>Advanced Security Settings for Bob's Documents</strong> to close it too, and <strong>OK</strong> in <strong>Bob's Documents Properties</strong>.</li><li>Try to browse to <strong>Bob's Documents</strong>. Even if you're on <strong>Administrator</strong> account, you should not be able to, but you should if you are logged in as Bob.</li>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/12082012_You_dont_have_permissions.png\" alt=\"You dont have permissions\" /></div>" +
            "<p align=\"center\">Permissions are set</p></ol><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_12082012_k = "Windows 7 security give access to a single user";
        public const string content_12082012_d = "A guide on restricting access to a Windows 7 folder to everyone but a single user";

        //"Yahoo Data Download"
        public const string content_08112012_b = "<p>Stock data can be downloaded from <strong>http://finance.yahoo.com/d/quotes.csv?s=[stock symbol string]&f=[special tags]</strong>. Some tags are listed in the table at the end of the post, but that's not the point. I'll be using a static url for a code example, such as <a href=\"http://download.finance.yahoo.com/d/quotes.csv?s=GOOG+AAPL+MSFT+YHOO&f=snd1l1t1vb3b2hg\">http://download.finance.yahoo.com/d/quotes.csv?s=GOOG+AAPL+MSFT+YHOO&f=snd1l1t1vb3b2hg</a> which will return values for Symbol, Name, Last trade date, Last trade (price only), Last trade time, Volume, Bid (real-time), Ask (real-time), Day's High and Day's Low.</p>";
        public const string content_08112012_r = "<p>The plan is to have a list of symbols (configurable), to get the data from yahoo and dynamically load the data into the <strong>WebGrid</strong> control. Therefore, I started with the basic <strong></strong>ViewModel that has two sets of entities - one for the symbols and one for the data itself. Eventually the list of symbols will be made configurable.</p><pre class=\"brush: csharp\">" +
            @"//ViewModel
            public class YahooViewModel
            {
             public List&lt;YahooData&gt; Datas { get; set; }
             public List&lt;YahooSymbol&gt; Symbols { get; set; }
             public YahooSymbol Symbol { get; set; }
             public int YahooSymbolID { get; set; }

             public YahooViewModel(int symbolid, YahooSymbol symbol, List&lt;YahooSymbol&gt; symbols, List&lt;YahooData&gt; datas)
             {
              Symbol = symbol;
              YahooSymbolID = symbolid;
              Symbols = symbols;
              Datas = datas;
             }
            }" + "</pre><p>The controller requests and populates the data, and later the automatic authentication may be added as described in the previous post.</p><pre class=\"brush: csharp\">" +
                    @"//Controller
            public ActionResult Index()
            {
             List&lt;YahooData&gt; datas = GetData();
             List&lt;YahooSymbol&gt; symbols = db.YahooSymbols.ToList();
             YahooSymbol symbol = symbols.First();
             int id = symbol.YahooSymbolID;
             return View(new YahooViewModel(id, symbol, symbols, datas));
            }

            public List&lt;YahooData&gt; GetData()
            {
             List&lt;YahooData&gt; datas = new List&lt;YahooData&gt;();

             HttpWebRequest req = (HttpWebRequest)WebRequest.Create('http://download.finance.yahoo.com/d/quotes.csv?s=GOOG+AAPL+MSFT+YHOO&f=snd1l1t1vb3b2hg');
             HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

             using (StreamReader streamReader = new StreamReader(resp.GetResponseStream()))
             {
              string t = streamReader.ReadToEnd();
              string[] strings = t.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
              datas = InsertData(strings);
             }
             return datas;
            }

            private List&lt;YahooData&gt; InsertData(string[] lines)
            {
             List&lt;YahooData&gt; datas = new List&lt;YahooData&gt;();

             foreach (string line in lines)
             {
              if (!String.IsNullOrEmpty(line))
              {
               YahooData datum = GetDatum(line);
               datas.Add(datum);
              }
             }
             return datas;
            }

            private YahooData GetDatum(string line)
            {
             var datum = new YahooData();
             string[] splitLine = line.Split(',');
             datum = new YahooData()
             {
              DataName = splitLine[1].Replace('\'', ''),
              Date = DateTime.ParseExact(splitLine[2].Replace('\'', ''), 'MM/d/yyyy', CultureInfo.InvariantCulture),
              LTP = decimal.Parse(splitLine[3]),
              Time = DateTime.Parse(splitLine[4].Replace('\'', '')),
              Volume = decimal.Parse(splitLine[5]),
              Ask = decimal.Parse(splitLine[6]),
              Bid = decimal.Parse(splitLine[7]),
              High = decimal.Parse(splitLine[8]),
              Low = decimal.Parse(splitLine[9])
             };
             return datum;
            }" + "</pre><p>The symbols are seeded initially, and may be later made configurable.</p><pre class=\"brush: csharp\">" +
                    @"//Seeding database with initial values
            public class SampleData : DropCreateDatabaseIfModelChanges&lt;RecipesEntities&gt;
            {
             protected override void Seed(RecipesEntities context)
             {
              AddSymbols(context);
             }
            }

            public static void AddSymbols(RecipesEntities context)
            {
             List&lt;YahooSymbol&gt; symbols = new List&lt;YahooSymbol&gt;
             {
              new YahooSymbol {YahooSymbolID = 1, YahooSymbolName = 'GOOG'},
              new YahooSymbol {YahooSymbolID = 2, YahooSymbolName = 'AAPL'},
              new YahooSymbol {YahooSymbolID = 3, YahooSymbolName = 'MSFT'},
              new YahooSymbol {YahooSymbolID = 4, YahooSymbolName = 'YHOO'}
             };

             symbols.ForEach(p =&gt; context.YahooSymbols.Add(p));
             context.SaveChanges();
            }" + @"</pre><p>Finally, the table of tags and their meanings - just for the interest.</p>

            <TABLE BORDER=""1""><TD>
            <strong>a </strong> </TD><TD>  Ask </TD><TD>
            <strong>a2</strong> </TD><TD>  Average Daily Volume </TD><TD>
            <strong>a5</strong> </TD><TD>  Ask Size </TD><TR><TD>
            <strong>b </strong> </TD><TD>  Bid </TD><TD>
            <strong>b2</strong> </TD><TD>  Ask (Real-time) </TD><TD>
            <strong>b3</strong> </TD><TD>  Bid (Real-time) </TD><TR><TD>
            <strong>b4</strong> </TD><TD>  Book Value </TD><TD>
            <strong>b6</strong> </TD><TD>  Bid Size </TD><TD>
            <strong>c </strong> </TD><TD>  Change & Percent Change </TD><TR><TD>
            <strong>c1</strong> </TD><TD>  Change </TD><TD>
            <strong>c3</strong> </TD><TD>  Commission </TD><TD>
            <strong>c6</strong> </TD><TD>  Change (Real-time) </TD><TR><TD>
            <strong>c8</strong> </TD><TD>  After Hours Change (Real-time) </TD><TD>
            <strong>d </strong> </TD><TD>  Dividend/Share </TD><TD>
            <strong>d1</strong> </TD><TD>  Last Trade Date </TD><TR><TD>
            <strong>d2</strong> </TD><TD>  Trade Date </TD><TD>
            <strong>e </strong> </TD><TD>  Earnings/Share </TD><TD>
            <strong>e1</strong> </TD><TD>  Error Indication (returned for symbol changed / invalid) </TD><TR><TD>
            <strong>e7</strong> </TD><TD>  EPS Estimate Current Year </TD><TD>
            <strong>e8</strong> </TD><TD>  EPS Estimate Next Year </TD><TD>
            <strong>e9</strong> </TD><TD>  EPS Estimate Next Quarter </TD><TR><TD>
            <strong>f6</strong> </TD><TD>  Float Shares </TD><TD>
            <strong>g </strong> </TD><TD>  Day's Low </TD><TD>
            <strong>h </strong> </TD><TD>  Day's High </TD><TR><TD>
            <strong>j </strong> </TD><TD>  52-week Low </TD><TD>
            <strong>k </strong> </TD><TD>  52-week High </TD><TD>
            <strong>g1</strong> </TD><TD>  Holdings Gain Percent </TD><TR><TD>
            <strong>g3</strong> </TD><TD>  Annualized Gain </TD><TD>
            <strong>g4</strong> </TD><TD>  Holdings Gain </TD><TD>
            <strong>g5</strong> </TD><TD>  Holdings Gain Percent (Real-time) </TD><TR><TD>
            <strong>g6</strong> </TD><TD>  Holdings Gain (Real-time) </TD><TD>
            <strong>i </strong> </TD><TD>  More Info </TD><TD>
            <strong>i5</strong> </TD><TD>  Order Book (Real-time) </TD><TR><TD>
            <strong>j1</strong> </TD><TD>  Market Capitalization </TD><TD>
            <strong>j3</strong> </TD><TD>  Market Cap (Real-time) </TD><TD>
            <strong>j4</strong> </TD><TD>  EBITDA </TD><TR><TD>
            <strong>j5</strong> </TD><TD>  Change From 52-week Low </TD><TD>
            <strong>j6</strong> </TD><TD>  Percent Change From 52-week Low </TD><TD>
            <strong>k1</strong> </TD><TD>  Last Trade (Real-time) With Time </TD><TR><TD>
            <strong>k2</strong> </TD><TD>  Change Percent (Real-time) </TD><TD>
            <strong>k3</strong> </TD><TD>  Last Trade Size </TD><TD>
            <strong>k4</strong> </TD><TD>  Change From 52-week High </TD><TR><TD>
            <strong>k5</strong> </TD><TD>  Percebt Change From 52-week High </TD><TD>
            <strong>l </strong> </TD><TD>  Last Trade (With Time) </TD><TD>
            <strong>l1</strong> </TD><TD>  Last Trade (Price Only) </TD><TR><TD>
            <strong>l2</strong> </TD><TD>  High Limit </TD><TD>
            <strong>l3</strong> </TD><TD>  Low Limit </TD><TD>
            <strong>m </strong> </TD><TD>  Day's Range </TD><TR><TD>
            <strong>m2</strong> </TD><TD>  Day's Range (Real-time) </TD><TD>
            <strong>m3</strong> </TD><TD>  50-day Moving Average </TD><TD>
            <strong>m4</strong> </TD><TD>  200-day Moving Average </TD><TR><TD>
            <strong>m5</strong> </TD><TD>  Change From 200-day Moving Average </TD><TD>
            <strong>m6</strong> </TD><TD>  Percent Change From 200-day Moving Average </TD><TD>
            <strong>m7</strong> </TD><TD>  Change From 50-day Moving Average </TD><TR><TD>
            <strong>m8</strong> </TD><TD>  Percent Change From 50-day Moving Average </TD><TD>
            <strong>n </strong> </TD><TD>  Name </TD><TD>
            <strong>n4</strong> </TD><TD>  Notes </TD><TR><TD>
            <strong>o </strong> </TD><TD>  Open </TD><TD>
            <strong>p </strong> </TD><TD>  Previous Close </TD><TD>
            <strong>p1</strong> </TD><TD>  Price Paid </TD><TR><TD>
            <strong>p2</strong> </TD><TD>  Change in Percent </TD><TD>
            <strong>p5</strong> </TD><TD>  Price/Sales </TD><TD>
            <strong>p6</strong> </TD><TD>  Price/Book </TD><TR><TD>
            <strong>q </strong> </TD><TD>  Ex-Dividend Date </TD><TD>
            <strong>r </strong> </TD><TD>  P/E Ratio </TD><TD>
            <strong>r1</strong> </TD><TD>  Dividend Pay Date </TD><TR><TD>
            <strong>r2</strong> </TD><TD>  P/E Ratio (Real-time) </TD><TD>
            <strong>r5</strong> </TD><TD>  PEG Ratio </TD><TD>
            <strong>r6</strong> </TD><TD>  Price/EPS Estimate Current Year </TD><TR><TD>
            <strong>r7</strong> </TD><TD>  Price/EPS Estimate Next Year </TD><TD>
            <strong>s </strong> </TD><TD>  Symbol </TD><TD>
            <strong>s1</strong> </TD><TD>  Shares Owned </TD><TR><TD>
            <strong>s7</strong> </TD><TD>  Short Ratio </TD><TD>
            <strong>t1</strong> </TD><TD>  Last Trade Time </TD><TD>
            <strong>t6</strong> </TD><TD>  Trade Links </TD><TR><TD>
            <strong>t7</strong> </TD><TD>  Ticker Trend </TD><TD>
            <strong>t8</strong> </TD><TD>  1 yr Target Price </TD><TD>
            <strong>v </strong> </TD><TD>  Volume </TD><TR><TD>
            <strong>v1</strong> </TD><TD>  Holdings Value </TD><TD>
            <strong>v7</strong> </TD><TD>  Holdings Value (Real-time) </TD><TD>
            <strong>w </strong> </TD><TD>  52-week Range </TD><TR><TD>
            <strong>w1</strong> </TD><TD>  Day's Value Change </TD><TD>
            <strong>w4</strong> </TD><TD>  Day's Value Change (Real-time) </TD><TD>
            <strong>x </strong> </TD><TD>  Stock Exchange </TD><TR><TD>
            <strong>y </strong> </TD><TD>  Dividend Yield </TD><TD>
            <strong>  </strong> </TD><TD></TD></TABLE>" + "<br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_08112012_k = "Yahoo stock data C# HttpWebRequest HttpWebResponse";
        public const string content_08112012_d = "Programmatically downloading stock data from Yahoo website";

        //"Automating Website Authentication"
        public const string content_25102012_b = "<p>Recently I had to implement automated logging on the website. In my particular case, that was Yahoo.com website, so the code snippets will be specific to this site. It should not be hard to modify them for other purposes. I developed two separate ways to achieve that, the first one has more code and is more complex (have to subscribe to two events and make more logical checks), but I figured it out first. It makes use of the <strong>WebBrowser</strong> class.</p>";
        public const string content_25102012_r = "<p>Create an instance of the <strong>WebBrowser</strong> and subscribe to <strong>Navigated</strong> and <strong>DocumentCompleted</strong> events</p><pre class=\"brush: csharp\">" +
            @"_browser = new WebBrowser();
            _browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
            _browser.Navigated += new WebBrowserNavigatedEventHandler(browser_Navigated);" + "</pre><p>On a timeline, first meaningful event that will be caught is <strong>browser_DocumentCompleted</strong> on the <strong>login.yahoo.com</strong>. The code then will analyse the controls on the page. For successful operation, I need to know actual names of the login and password input elements. I find them by name, and set the values to actual login and password. Then I simulate the click on the login button.</p><p>Next meaningful event is <strong>browser_Navigated</strong> on <strong>my.yahoo.com</strong> page - see below.</p><p>After that, I'll point the browser to the url of the document I want to read or download. I'll catch <strong>browser_DocumentCompleted</strong> again, on that page, and read the contents using the <strong>WebBrowser.Document.Body.InnerText</strong> (end of the code snippet).</p><pre class=\"brush: csharp\">" +
                    @"void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
            {
             //loaded the Yahoo login page
             if (_browser.Url.AbsoluteUri.Contains(LoginUrl))
             {
              if (_browser.Document != null)
              {
               //Find and fill the 'username' textbox
               HtmlElementCollection collection = _browser.Document.GetElementsByTagName('input');
               foreach (HtmlElement element in collection)
               {
                string name = element.GetAttribute('id');
                if (name == 'username')
                {
                 element.SetAttribute('value', _login);
                 break;
                }
               }

               //Find and fill the 'password' field
               foreach (HtmlElement element in collection)
               {
                string name = element.GetAttribute('id');
                if (name == 'passwd')
                {
                 element.SetAttribute('value', _password);
                 break;
                }
               }

               //Submit the form
               collection = _browser.Document.GetElementsByTagName('button');
               foreach (HtmlElement element in collection)
               {
                string name = element.GetAttribute('id');
                if (name == '.save')
                {
                 element.InvokeMember('click');
                 break;
                }
               }
              }
             }
 
             //downloaded 'quote.csv'
             if(_browser.Url.AbsoluteUri.Contains('.csv'))
             {
              if (_browser.Document != null && _browser.Document.Body != null)
              {
               string s = _browser.Document.Body.InnerText;
              }
             }
            }" + "</pre><p></p>Here I actually copy the cookies, but that is not necessary. The <strong></strong>WebBrowser will keep them internally and use them. The purpose of this code is to check if the browser is redirected to \"my.yahoo.com\", which is the indication of successful login. Further logic may be applied from here. <pre class=\"brush: csharp\">" +
                    @"void browser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
            {
             //Successful login takes to 'my.yahoo.com'
             if (_browser.Url.AbsoluteUri.Contains(MyYahoo))
             {
              if (_browser.Document != null && !String.IsNullOrEmpty(_browser.Document.Cookie))
              {
               _cookies = _browser.Document.Cookie;
              }
             }
            }" + "</pre><p>The second approach is shorted, but it took me longer to figure out. Here I have to explicitly use the <strong>CookieContainer</strong> to save the cookies \"harvested\" by the <strong>HttpWebRequest</strong> which does the login, and use them in the <strong>HttpWebRequest</strong> which asks for the file after authentication. Of course, I still need to know what are the names of login and password elements, because I'm sending the values in the POST data.</p><p>Step one - authentication</p><pre class=\"brush: csharp\">" +
                    @"string strPostData = String.Format('login={0}&passwd={1}', _login, _password);

            // Setup the http request.
            HttpWebRequest wrWebRequest = WebRequest.Create(LoginUrl) as HttpWebRequest;
            wrWebRequest.Method = 'POST';
            wrWebRequest.ContentLength = strPostData.Length;
            wrWebRequest.ContentType = 'application/x-www-form-urlencoded';
            _yahooContainer = new CookieContainer();
            wrWebRequest.CookieContainer = _yahooContainer;

            // Post to the login form.
            using (StreamWriter swRequestWriter = new StreamWriter(wrWebRequest.GetRequestStream()))
            {
             swRequestWriter.Write(strPostData);
             swRequestWriter.Close();           
            }

            // Get the response.
            HttpWebResponse hwrWebResponse = (HttpWebResponse)wrWebRequest.GetResponse();" + "</pre><p>Step two - accessing data using the cookies.</p><pre class=\"brush: csharp\">" +
                    @"HttpWebRequest req = (HttpWebRequest)WebRequest.Create(_downloadUrl);
            req.CookieContainer = _yahooContainer;
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            using(StreamReader streamReader = new StreamReader(resp.GetResponseStream()))
            {
             string t = streamReader.ReadToEnd();
            }" + "</pre><p><strong>References:</strong></p><a href=\"http://msdn.microsoft.com/en-us/library/aa752040(v=vs.85).aspx\">WebBrowser control</a><br/><a href=\"http://forums.asp.net/t/1678714.aspx/1\">submit a form data from external address !</a><br/><a href=\"http://stackoverflow.com/questions/930807/c-sharp-login-to-website-via-program\">C# Login to Website via program</a><br/><a href=\"http://stackoverflow.com/questions/9123159/how-to-login-to-yahoo-website-programatically\">how to login to yahoo website programatically</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_25102012_k = "Authentication C# development WebBrowser HttpWebRequest HttpWebResponse";
        public const string content_25102012_d = "Automating authentication into websites using WebBrowser control or HttpWebRequest and HttpWebResponse";

        //"Crystal Reports, C#, Object as Data Source"
        public const string content_14102012_b = "<p>Based on the Ping example from one of the recent posts, I'm continuing it with the Crystal Reports example, because I have never used Crystal Reports until now. So consider that the following class was added to the solution.</p>";
        public const string content_14102012_r = "<pre class=\"brush: csharp\">" +
            @"public class PingResult
            {
             public string sPacketsSent;
             public string sPacketsReceived;
             public string sPacketsLost;
             public string sPacketTime;
            }" + "</pre><p>An instance of the class is created and populated with results when the ping command runs and its results are parsed. So I have one of the simplest possible objects to use as a source for a report. The next step is to add a <strong>Crystal Report</strong> to the application. <strong>Visual Studio 2010</strong> has an item to add called \"Crystal Report\", but they are not installed.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/14102012_Crystal_report.png\" alt=\"Crystal report\" /></div>" +
            "<p align=\"center\">Crystal Report Online Template</p><p>When I select this item, I'm prompted with a download screen.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/14102012_Crystal_reports_install.png\" alt=\"Crystal reports install\" /></div>" +
            "<p align=\"center\">Download Crystal Reports</p><p>Installation is simple - just following the instructions. I chose the standard version, and the download size is 288MB. After a few short hours, I have a Crystal Report called pingReport.mht in my solution. I have an option to configure my report using the wizard, which I'm doing by choosing the following options:</p><p>On the first page, <strong>Using the Report Wizard</strong>, and <strong>Standard</strong> layout.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/14102012_Crystal_reports_gallery.png\" alt=\"Crystal reports gallery\" /></div>" +
            "<p align=\"center\">Create a New Crystal Report Document</p><p>On the next page, I choose to populate my report from .NET Object in project data. My PingResult class is in the list, and I move it to the <strong>Selected Tables</strong>.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/14102012_Report_creation_wizard.png\" alt=\"Report creation wizard\" /></div>" +
            "<p align=\"center\">Choose the data you want to report on</p><p>Then I choose the fields to display, of which I select all.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/14102012_Report_creation_wizard_2.png\" alt=\"Report creation wizard 2\" /></div>" +
            "<p align=\"center\">Choose the information to display on the report</p><p>I skip <strong>Grouping</strong>, <strong>Report Selection</strong> and <strong>Report Style</strong>, leaving default values. Now I have my report editor. I only want to do a little change - make the headers human readable, so I edit them in the following manner</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/14102012_Edit_text_object.png\" alt=\"Edit text object\" /></div>" +
            "<p align=\"center\">Edit text object</p><p>Now some tricks: when I build my solution, I get the following error:</p><p><u>Warning 1 The referenced assembly \"CrystalDecisions.CrystalReports.Engine, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304, processorArchitecture=MSIL\" could not be resolved because it has a dependency on \"System.Web, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a\" which is not in the currently targeted framework \".NETFramework,Version=v4.0,Profile=Client\". Please remove references to assemblies not in the targeted framework or consider retargeting your project. PingTest</u></p><p>This is quite obvious, I need to add a reference to <strong>System.Web</strong>, but to do that I need to first change the <strong>Target Framework</strong> default setting of <strong>.NET Framework 4 Client Profile</strong> to just <strong>.NET Framework 4</strong>. Now the project builds.</p>" +
            "<p>In my toolbox I now have the <strong>pingReport1</strong> component, which I add to the form.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/14102012_Toolbox.png\" alt=\"Toolbox\" /></div>" +
            "<p align=\"center\">pingReport1</p><p>I also need a report viewer, which I also add</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/14102012_CrystalReportViewer.png\" alt=\"CrystalReportViewer\" /></div>" +
            "<p align=\"center\">CrystalReportViewer</p><p>The final effort: connect the report with the object that contains data. Here's how:</p><pre class=\"brush: csharp\">" +
            @"pingReport1 myReport = new pingReport1();
            myReport.SetDataSource(new[] { pingResult });
            pingReportViewer1.ReportSource = myReport;" +
            "</pre><p>Looks simple, just note how the objects are wrapped into array. This is important.</p><p>And the final trick, when I run the application, I get the FileNotFound exception</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/14102012_FileNotFoundException.png\" alt=\"FileNotFoundException\" /></div>" +
            "<p align=\"center\">FileNotFoundException</p><p>which is resolved by adding the <strong>useLegacyV2RuntimeActivationPolicy=\"true\"</strong> parameter to the startup node of my app.config which now looks like the following</p><pre class=\"brush: xml\">" +
            @"&lt;?xml version='1.0'?&gt;
            &lt;configuration&gt;
            &lt;startup  useLegacyV2RuntimeActivationPolicy='true'&gt;
              &lt;supportedRuntime version='v4.0' sku='.NETFramework,Version=v4.0'/&gt;
            &lt;/startup&gt;
            &lt;/configuration&gt;" +
            "</pre><p>At this point I consider my small example complete.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/14102012_Form.png\" alt=\"Form\" /></div>" +
            "<p align=\"center\">Complete</p><p><strong>References</strong></p><a href=\"http://www.codeproject.com/Articles/12694/Creating-Crystal-Reports-using-C-with-Datasets\">Creating Crystal Reports using C# with Datasets</a><br/><a href=\"http://stackoverflow.com/questions/7619745/very-odd-situation-with-crystalreport-and-or-visual-studio-2010-i-dont-know-may\">Very Odd situation with CrystalReport and/or Visual studio 2010 I don't know maybe .Net Framework</a><br/><a href=\"http://stackoverflow.com/questions/3968727/can-crystal-reports-get-data-from-an-object-data-source\">Can crystal reports get data from an object data source?</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_14102012_k = "C# .NET Development Crystal Reports";
        public const string content_14102012_d = "A simple example of using Crystal Reports and populating data from .NET objects";

        // "A Simple Show/Hide Technique with jQuery"
        public const string content_07102012_b = "<p>Today I learned a simple technique to show/hide parts of content on the page. In my sample application, I'm using it to display a part of a long blog post, and then at the end of the part to display a button that will show the rest of the post. The button initially says <strong>\"Show more\"</strong>. When the button is clicked, it displays the previously hidden part and its text changes to <strong>\"Show less\"</strong>. So, in essence, it toggles the display state of the div it is connected to, and its own text.</p>";
        public const string content_07102012_r = "<p>Additionally, I'm displaying posts dynamically, so for each post there is a div with the first part of the post, the div with the rest, and the toggle button. Here's how it works:</p><p>For each blog post in the model a div is created for the rest of the post, and a button. The ID for both is assigned dynamically using a simple counter.</p><pre class=\"brush: xml\">" +
            @"@model Recipes.ViewModels.BlogViewModel
            @{ int i = 0; }
            @foreach (var post in Model.Posts)
            {
                string divID = ""hide"" + i.ToString();
                string btnID = ""btn"" + i.ToString();
                &lt;div id=""middlecolumn""&gt;
                    &lt;div class=""blogcontainer""&gt;
                        &lt;h3 class=""title""&gt;&lt;a href=""#""&gt;@post.Title&lt;/a&gt;&lt;/h3&gt;
                        &lt;div class=""info""&gt;&lt;span class=""submitted""&gt;Submitted on @post.DateCreated&lt;/span&gt;&lt;/div&gt;
                        @MvcHtmlString.Create(post.BriefContent)
                        &lt;div class=""buttonlink""&gt;&lt;a id=@btnID href=""javascript:toggleDiv('@divID', '@btnID');""&gt;Show more&lt;/a&gt;&lt;/div&gt;
                        &lt;div id=@divID  style=""display:none""&gt;@MvcHtmlString.Create(post.RestOfContent)&lt;/div&gt;
                    &lt;/div&gt;
                &lt;/div&gt;
                i = i + 1;
            }" +
           "</pre><p>The javaScript function <strong>toggleDiv()</strong> takes two parameters: div ID and button ID. First the function toggles the div display property by using the jQuery function <strong>toggle()</strong>. Next, based on the div display state, the button text is set to one of the two possible values. And that's it.</p><pre class=\"brush: jscript\">" +
            @"&lt;script type=""text/javascript""&gt;
            &lt;!--
                function toggleDiv(divId, btnId) {
                    $(""#"" + divId).toggle();
                    if ($(""#"" + divId).css('display') == 'none')
                    {$(""#"" + btnId).html('Show more'); }
                    else
                    { $(""#"" + btnId).html('Show less'); }
                }
            --&gt;
            &lt;/script&gt;" +
            "</pre><p>Here's the example of the page with the blog post</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/07102012_Hide.png\" alt=\"Hide\" /></div>" +
            "<p align=center>Rest of the post hidden</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/07102012_Show.png\" alt=\"Show\" /></div>" +
            "<p align=center>Rest of the post expanded</p><p><strong>References</strong></p><a href=\"http://www.randomsnippets.com/2008/02/12/how-to-hide-and-show-your-div/\">How to hide, show, or toggle your div</a><br/><a href=\"http://www.randomsnippets.com/2011/04/10/how-to-hide-show-or-toggle-your-div-with-jquery/\">How to hide, show, or toggle your div with jQuery</a>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_07102012_k = "MVC C# Development jQuery";
        public const string content_07102012_d = "A Simple Show/Hide Technique with jQuery";

        //"Playing With Google Search Results - 2"
        public const string content_27092012_b = "<p>Another way of getting back web search results from Google is to use <strong>Google API</strong>. I've spent a couple of hours researching the option to do that, and did not find too many exciting choices. There is an option to use Google API which is deprecated, and limits the amount of searches to about 100 per day, and does not return more than 64 results, and does not allow automatic searches, or to use Google Custom Search, which can be used only to create site-specific custom searches. Anyway, as an exercise I decided to implement a call to Google API (deprecated one). There are a few options available.</p>";
        public const string content_27092012_r = "<p>The easiest way to use Google API I found was to use Google API for .NET. After I downloaded and referenced the <strong>GoogleSearchAPI.NET20</strong> dll, it took me a surprisingly small amount of lines of code to create a quick prototype of querying the Google API</p><pre class=\"brush: csharp\">" +
            @"private void btnSearch_Click(object sender, EventArgs e)
            {
             string searchTerms = txtTerms.Text;
             List&lt;string&gt; GoogleApiResults = GoogleAPI.StringResultList(searchTerms, 100);
            }

            public static class GoogleAPI
            {
             public static GwebSearchClient client = new GwebSearchClient("");

             public static List&lt;String&gt; StringResultList(string terms, int number)
             {
              IList&lt;IWebResult&gt; list = client.Search(terms, number);
              List&lt;String&gt; results = new List&lt;string&gt;();
              foreach (var result in list)
              {
               results.Add(result.Url);
              }
              return results;
             }
            }" + "</pre>" + "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/27092012_results.png\" alt=\"results\" /></div>" + "<p align=\"center\">Search Results</p><p><strong>References</strong></p><a href=\"http://gapi4net.codeplex.com/\">Google API for .NET</a>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_27092012_k = "Google API GoogleSearchAPI C# Development MVC";
        public const string content_27092012_d = "Using Google API to retrieve search results programmatically";

        //"Running an Command Line Program in C# and Getting Output"
        public const string content_11092012_b = "<p>A simple example. Let's say I want to run ping from command line, but to make this more automated, or maybe user friendly, I would like to run a C# application that pings an IP address, captures the returned result and displays it in a user-friendly format.</p>";
        public const string content_11092012_r = "<p>Fist thing is to start the command prompt and execute a process. Here's one of the most convenient ways to use it: utilize <strong>ProcessStartInfo</strong> and <strong>Process</strong> classes, which are part of <strong>System.Diagnostics</strong> namespace. <strong>ProcessStartInfo</strong> takes the program to run, in this case <strong>cmd.exe</strong>, and parameters, in this case ping, together with its own parameters. Here's how it works:</p><pre class=\"brush: csharp\">" +
            @"private void btnPing_Click(object sender, EventArgs e)
            {
             string command = '/c ping ' + txtIP.Text;

             ProcessStartInfo procStartInfo = new ProcessStartInfo('CMD', command);

             Process proc = new Process();
             proc.StartInfo = procStartInfo;
             proc.Start();
            }" + "</pre><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/11092012_Ping_Command_Prompts.png\" alt=\"Ping Command Prompts\" /></div>" +
            "<p align=\"center\">Command prompts started from Windows Form</p><p>The process starts and the familiar command window appears, then the ping command runs. Now to capture the results of the ping, a few other lines are needed. Firstly, the output of the process needs to be redirected. The following values need to be set:</p><pre class=\"brush: csharp\">" +
                    @"procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;" + "</pre><p>Next, to capture the output line by line as it is sent by the process, I'll attach a function that does it asynchronously.</p><pre class=\"brush: csharp\">" +
                    @"proc.OutputDataReceived += new DataReceivedEventHandler(proc_OutputDataReceived);
            proc.Start();
            proc.BeginOutputReadLine();
            proc.WaitForExit();" + "</pre><p>The function can do anything, but in my case I'm simply redirecting the output to the Windows Form.</p><pre class=\"brush: csharp\">" +
                    @"void proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
            {
             if (e.Data != null)
             {
              txtOutput.Text = txtOutput.Text + e.Data.Trim() + Environment.NewLine;
             }
            }" + "</pre><p>Looks correct, so why am I receiving this exception:</p><p><i>Cross-thread operation not valid: Control 'txtOutput' accessed from a thread other than the thread it was created on.</i></p><p>Well, looks like it's telling me that the process is running from another thread and can not quite access my text box from that thread. Long story short, this is the shortest solution I have found for this issue (there are many options, some as complicated as using a BackgroundWorker).</p><pre class=\"brush: csharp\">" +
                    @"void proc_OutputDataReceived(object sender, DataReceivedEventArgs e)
            {
             if (e.Data != null)
             {
              string newLine = e.Data.Trim() +Environment.NewLine;
              MethodInvoker append = () => txtOutput.Text += newLine;
              txtOutput.BeginInvoke(append);
             }
            }" + "</pre><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/11092012_Ping_Form.png\" alt=\"Ping Form\" /></div><p align=\"center\">Command prompt output redirected to Windows Form</p><p><strong>References:</strong></p><a href=\"http://stackoverflow.com/questions/7717518/having-trouble-with-process-class-while-redirecting-command-prompt-output-to-win\">Having trouble with Process class while redirecting command prompt output to winform</a><a href=\"http://stackoverflow.com/questions/11631443/capturing-process-output-via-outputdatareceived-event\">Capturing process output via OutputDataReceived event</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_11092012_k = "C# programming command line output";
        public const string content_11092012_d = "Running an Command Line Program in C# and Getting Output";

        //"Learning MVC: No parameterless constructor defined for this object"
        public const string content_13092012_b = "<p>I'm developing a sample application using MVC - a \"blog engine\". OK, getting rid of all the buzzwords, it is just a few tables: Blogs, Bloggers, Posts. You can add bloggers, create blogs and add posts to a selected blog. Being a good boy, I'm trying not to pass objects like Blog or Post to the view, but rather use ViewModels wherever makes sense. Nothing complicated, for example</p>";
        public const string content_13092012_r = "<pre class=\"brush: csharp\">" +
            @"public class BlogViewModel
            {
             public Blog Blog;
             public List&lt;Post&gt; Posts;
             ...
 
             public BlogViewModel(Blog blog, List&lt;Post&gt; posts, ... )
             {
              Blog = blog;
              Posts = posts;
              ...
             }
            }" + "</pre><p>and then in the <strong>BlogController</strong> I would have these methods for creating a new blog:</p><pre class=\"brush: csharp\">" +
                    @"public ActionResult Create()
            {
             Blogger selectedBlogger = db.Bloggers.First();
             Blog blog = new Blog();
             return View(new BlogViewModel(blog, new List&lt;Post&gt;(), ...));
            }

            [HttpPost]
            public ActionResult Create(BlogViewModel viewModel)
            {
             Blog blog = viewModel.Blog;
             blog.Blogger = db.Bloggers.Where(b => b.BloggerID == viewModel.BloggerID).FirstOrDefault();

             if (ModelState.IsValid)
             {
              try
              {
               db.Blogs.Add(blog);
               db.SaveChanges();
              }
  
              // process errors
             }
             return View(new BlogViewModel(blog, new List&lt;Post&gt;(), ...));
            }" + "</pre><p>Something like that. So I'm testing the create method when I suddenly recieve the \"<i>No parameterless constructor defined for this object</i>\" error.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/11092012_No_parameterless_constructor.png\" alt=\"No parameterless constructor\" /></div>" +
            "<p align=\"center\">No parameterless constructor defined for this object</p><p>That left me scratching my head for some time, because I could not figure out what constructor I'm missing. Took a bit of searching to realise: the constructor is missing in the <strong>ViewModel</strong>. If I modify the constructor shown above as follows</p><pre class=\"brush: csharp\">" +
                    @"public class BlogViewModel
            {
             public Blog Blog;
             public List&lt;Post&gt; Posts;
             ...
 
             public BlogViewModel()
             { 
             }

             public BlogViewModel(Blog blog, List&lt;Post&gt; posts, List&lt;Blog&gt; blogs, int bloggerid, List&lt;Blogger&gt; bloggers)
             {
              Blog = blog;
              Posts = posts;
              ...
             }
            }" + "</pre><p>the error just goes away (notice that parameterless constructor that is just sitting there now, happily doing nothing?). Why is that? Well, I'll be totally honest: I have no idea.</p><p><strong>Reference</strong></p><a href=\"http://nicholasbarger.com/2012/03/11/fun-and-struggles-with-mvc-no-parameterless-constructor-defined/\">Fun and Struggles with MVC – No Parameterless Constructor Defined</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_13092012_k = "MVC C# Parameterless Constructor";
        public const string content_13092012_d = "No parameterless constructor defined for this object";

        //"Learning MVC: Display a Custom Error Page, Log Error and Send Email"
        public const string content_09082012_b = "<p>Step one - create my own controller class. Simple, just add a <strong>BaseController</strong> to the <strong>Controllers</strong> folder</p>";
        public const string content_09082012_r = "<pre class=\"brush: csharp\">" +
            @"public abstract class BaseController : Controller
            {
            }" + "</pre><p>and then modify all existing contollers to inherit from <strong>BaseController</strong> rather than from <strong>System.Web.Mvc.Controller</strong>.</p><p>Next, I override the <strong>OnException</strong> method in the <strong>BaseController</strong> which is called whenever the exception is thrown within an action.</p><pre class=\"brush: csharp\">" +
                    @"public abstract class BaseController : Controller
            {
                protected override void OnException(ExceptionContext filterContext)
                {
                    var fileName = Path.Combine(Request.MapPath('~/App_Data'), 'log.txt');
                    WriteLog(fileName, filterContext.Exception.ToString());
                    filterContext.ExceptionHandled = true; 
                    this.View('Error').ExecuteResult(this.ControllerContext);
                }
    
                static void WriteLog(string logFile, string text)
                {   
                    StringBuilder message = new StringBuilder();
                    message.AppendLine(DateTime.Now.ToString());
                    message.AppendLine(text);
                    message.AppendLine('=========================================');
                    System.IO.File.AppendAllText(logFile, message.ToString());
                }
            }" + "</pre><p>I can verify and find out that the Yellow Screen of Death is not indeed shown</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/09082012_Sorry_an_error_occurred.png\" alt=\"Sorry an error occurred\" /></div>" +
            "<p align=\"center\">Custom Error Page</p><p>And the log file is in my <strong>App_Data</strong> folder</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/09082012_InvalidOperationException.png\" alt=\"InvalidOperationException\" /></div><p align=\"center\">Log File</p><p>Now that I can see it works, I would still want to see the exceptions as soon as they occur, rather than checking the log file on each occasion. To achieve that, first I need to add</p><pre class=\"brush: xml\">" +
                    @"&lt;customErrors mode='RemoteOnly' /&gt;" +
                    "</pre><p>to the <strong>system.web</strong> section of the <strong>Web.config</strong> file and then in the <strong>OnException</strong> method check if this section is set.</p><pre class=\"brush: csharp\">" +
                    @"protected override void OnException(ExceptionContext filterContext)
            {
                var fileName = Path.Combine(Request.MapPath('~/App_Data'), 'log.txt');
                WriteLog(fileName, filterContext.Exception.ToString());
                if (filterContext.HttpContext.IsCustomErrorEnabled)
                {
                    filterContext.ExceptionHandled = true; 
                    this.View('Error').ExecuteResult(this.ControllerContext);
                }
            }" + "</pre><p>Finally, I would like to receive an email when something on my website goes wrong. I'm adding a function for that, rearranging a few lines and come up with the final version of my <strong>BaseController</strong> (for now).</p><pre class=\"brush: csharp\">" +
                    @"using System;
            using System.Web.Mvc;
            using System.Text;
            using System.IO;
            using System.Net.Mail;

            namespace Recipes.Controllers
            {
                public abstract class BaseController : Controller
                {
                    protected override void OnException(ExceptionContext filterContext)
                    {
                        string ex = filterContext.Exception.ToString();
                        var fileName = Path.Combine(Request.MapPath('~/App_Data'), 'log.txt');
                        WriteLog(fileName, ex);
                        SendEmail(ex);
                        if (filterContext.HttpContext.IsCustomErrorEnabled)
                        {
                            filterContext.ExceptionHandled = true; 
                            this.View('Error').ExecuteResult(this.ControllerContext);
                        }
                    }

                    static StringBuilder ErrorText(string text)
                    {
                        StringBuilder message = new StringBuilder();
                        message.AppendLine(DateTime.Now.ToString());
                        message.AppendLine(text);
                        message.AppendLine('=========================================');
                        return message;
                    }

                    static void WriteLog(string logFile, string text)
                    {
                        System.IO.File.AppendAllText(logFile, ErrorText(text).ToString());
                    }

                    static void SendEmail(string text)
                    {
                        MailMessage mail = new MailMessage();
                        SmtpClient client = new SmtpClient('smtp.example.com');
                        client.Credentials = new System.Net.NetworkCredential('u$3r', 'pa$$word'); client.Port = 587;

                        mail.From = new MailAddress('mvc@example.com');
                        mail.To.Add('developer@example.com');
                        mail.Subject = 'Error on your website';
                        mail.Body = ErrorText(text).ToString();

                        client.Send(mail); 
                    }
                }
            }" + "</pre><p><strong>References</strong></p><a href=\"http://stackoverflow.com/questions/1166089/problem-with-generic-base-controller-error-handling-in-asp-net-mvc\">Problem with generic base controller error handling in ASP.NET MVC</a><br/><a href=\"http://blog.dantup.com/2009/04/aspnet-mvc-handleerror-attribute-custom.html\">ASP.NET MVC HandleError Attribute, Custom Error Pages and Logging Exceptions</a><br/><a href=\"http://stackoverflow.com/questions/9629647/how-to-send-email-from-asp-net-mvc-3\">How to send email from Asp.net Mvc-3?</a><br/><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_09082012_k = "MVC C# custom error page logging email";
        public const string content_09082012_d = "Showing a custom error page, logging and error and sending email with MVC";

        //"Learning MVC: Game of Life in MVC"
        public const string content_06082012_b = "<p>I wrote some code and made a quick WPF application that implemented Conway's Game of Life earlier ( <a href=\"http://justmycode.blogspot.com/2012/07/game-of-life-exercise-and-extension.html\">Game of Life Exercise and Extension Methods</a>).</p>";
        public const string content_06082012_r = "<p>Next, I wanted to see how the game could be run on the MVC platform. The solution in short: use javaScript <strong>setInterval</strong> function to load the partial view into the div. Use a method in the controller to generate the partial view.</p><p>Here is how my solution looked:</p><pre class=\"brush: csharp\">" +
            @"public class GameController : Controller
            {
                public ActionResult Index()
                {
                    GameOfLifeHelpers.InitializeGame();
                    return View(NextIteration());
                }

                [OutputCache(NoStore = true, Location = OutputCacheLocation.Client, Duration = 1)]
                public ActionResult Update()
                {
                    return PartialView('_Table', NextIteration());
                }

                public HtmlString NextIteration()
                {
                    GameOfLifeHelpers.DrawNextIteration();
                    return new HtmlString(GameOfLifeHelpers.table.ToString());
                }
            }" + "</pre><p>The partial view is called <strong>_Table</strong> and is nothing more than the <strong></strong>HtmlString. Here is the partial view:</p><pre class=\"brush: xml\">" +
                    @"@model HtmlString
           
            @{ Layout = null; }

            @Model" + "</pre><p>The model is just the <strong>HtmlString</strong> which gets rendered, and the <strong>HtmlString</strong> itself is just a simple table of a specified number of cells. And here is the <strong>Index.cshtml</strong></p><pre class=\"brush: jscript\">" +
                    @"&lt;script type='text/javascript'&gt;
                setInterval('$('#update').load('/Game/Update')', 1000);
            &lt;/script&gt;

            @model HtmlString

            @{
                ViewBag.Title = 'Index';
            }

            &lt;h2&gt;Index&lt;/h2&gt;
            &lt;div id='update'&gt;@{Html.RenderPartial('_Table', Model);}&lt;/div&gt;" +
                    "</pre><p>Note how the interval is set to 1000 ms and the <strong>OutputCache</strong> duration in the controller is set to the same value. Every second the call to load will return a partial view from the <strong>Update</strong> method. What does the Update method return? When the game starts, and empty html table is created with each cell having a blue background.</p><pre class=\"brush: csharp\">" +
                    @"public static void NewGameTable()
            {
                table = new StringBuilder(@'&lt;table border=1 bordercolor=black cellspacing=0 cellpadding=0&gt;');
                for (int i = 0; i &lt; y; i++)
                {
                    table.Append('&lt;tr&gt;');
                    for (int j = 0; j &lt; x; j++)
                    {
                        table.Append('&lt;td width=10px height=10px bgcolor=#0276FD&gt;&lt;/td&gt;');
                    }
                    table.Append('&lt;/tr&gt;');
                }
                table.Append('&lt;/table&gt;');
            }" + "</pre><p>Then, on each iteration, a new boolead array is filled to specify which cells will be \"alive\".</p><pre class=\"brush: csharp\">" +
                    @"public static void DrawNextIteration()
            {
                bool[,] arrCurrent = counter % 2 == 0 ? arrOne : arrTwo;
                bool[,] arrNext = counter % 2 == 0 ? arrTwo : arrOne;
                FillArray(arrNext, arrCurrent);
                counter++;
                for (int i = 0; i &lt; y; i++)
                {
                    for (int j = 0; j &lt; x; j++)
                    {
                        if (arrNext[i, j] != arrCurrent[i, j])
                        {
                            table = arrNext[i, j] ? GameOfLifeTableReplaceCell(i, j, '#FF0000', table) : GameOfLifeTableReplaceCell(i, j, '#0276FD', table);
                        }
                    }
                }
            }" + "</pre><p>The function that replaces the cell is very simple - it calculates the position where the font for the cell is specified based on the coordinates and makes the cell color red if it went from \"dead\" to \"alive\", and vice versa.</p><pre class=\"brush: csharp\">" +
                    @"public static StringBuilder GameOfLifeTableReplaceCell(int i, int j, string colour, StringBuilder sb)
            {
                const int rowLength = 48*x + 9;
                const int cellLength = 48;
                int start = 62 + i * rowLength + 4 + j * cellLength + 35;
                sb.Remove(start, 7);
                sb.Insert(start, colour);
                return sb;
            }" + "</pre><p>The rest of the code is omitted because it can be found in my earlier post and used with little or no change.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_06082012_k = "Game of Life MVC C# Development Programming";
        public const string content_06082012_d = "Implementing Game of Life using MVC";

        //"The Case of a Strangely Coloured ProgressBar"
        public const string content_05082012_b = "<p>At first this bug report puzzled me a bit. Essentially it said <i>\"Progress bar fills with blue rectangles, status text nearly impossible to read\"</i>. That was a case of \"works on my machine\" because all I could see was that:</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/05082012_Progress_bar_green.png\" alt=\"Progress bar green\" /></div>" +
            "<p align=\"center\">Can you see blue rectangles?</p>";
        public const string content_05082012_r = "<p>However, I soon discovered that just optimizing my Windows visual effects for best performance does a neat trick. If you don't know, it's under <strong>Control Panel &rarr; System &rarr; Advanced System Settings &rarr; Advanced &rarr; Performance &rarr; Settings &rarr; Visual Effects &rarr; Adjust for best performance</strong>, as in the screen shot below</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/05082012_Advanced_system_settings.png\" alt=\"Advanced system settings\" /></div>" +
            "<p align=\"center\">Visual Effects</p><p>Here is what I saw:</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/05082012_Progress_bar_blue.png\" alt=\"Progress bar blue\" /></div>" + "<p align=\"center\">Can you see blue rectangles NOW?</p><p>That was an easy fix. I just modified the ForeColor of the ProgressBar as shown below from the default value:</p><pre class=\"brush: xml\">" +
            @"//before
            &lt;ProgressBar Name='Progress' Grid.Column='0' Value='{Binding ProgressValue}' HorizontalAlignment='Stretch'/&gt;

            //after
            &lt;ProgressBar Foreground='LightGreen' Name='Progress' Grid.Column='0' Value='{Binding ProgressValue}' HorizontalAlignment='Stretch'/&gt;" +
            "</pre><p>Here is what I saw after making this change:</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/05082012_Progress_bar_green_rectangles.png\" alt=\"Progress bar green rectangles\" /></div>" +
            "<p align=\"center\">Adjusted for best performance</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/05082012_Progress_bar_green_smooth.png\" alt=\"Progress bar green smooth\" /></div>" +
            "<p align=\"center\">Allow Windows to determine settings</p><p><strong>Reference:</strong></p><a href=\"http://stackoverflow.com/questions/4734814/wpf-progressbar-foreground-color\">WPF: Progressbar foreground color</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_05082012_k = "WPF ProgressBar color C# development";
        public const string content_05082012_d = "I search for the root cause of the issue with the WPF ProgressBar";

        //"Installing Windows Updates via Shell Script"
        public const string content_04082012_b = "<p>I had to dive into the world of shell scripting and do some work there. One of the tasks I had was to automatically install a bunch of Windows Updates on a server. The server could not be connected to the Internet so the updates were provided as separate files. The updates could be a mix of regular executables or Microsoft Update Standalone Packages (.msu). The script gets the name of the folder it runs from and then iterates over the files in this folder. It checks the file extension and runs appropriate command depending on the file being EXE or MSU. It also checks the return value and keeps a counter on the number of updates that reported successful and unsuccessful result, and writes the result of each install into a log file. At the end it displays a message that informs a user about the number of successfully installed and failed updates.</p>";
        public const string content_04082012_r = "<pre class=\"brush: vb\">" +
            @"Sub Main

            Dim objfso, objShell
            Dim iSuccess, iFail
            Dim folder, files, sFolder, folderidx, Iretval, return
            Set objfso = CreateObject('Scripting.FileSystemObject')
            Set objShell = CreateObject('Wscript.Shell')
 
            sFolder = left(WScript.ScriptFullName,(Len(WScript.ScriptFullName))-(len(WScript.ScriptName)))
            Set folder = objfso.GetFolder(sFolder)
            Set logFile = objfso.CreateTextFile('C:\log.txt', TRUE)
            Set files = folder.Files
            iSuccess = 0
            iFail = 0
 
            For each folderIdx In files
             If Ucase(Right(folderIdx.name,3)) = 'MSU' then
              logFile.WriteLine('Installing ' & folderidx.name & '...')
              wscript.echo 'Installing ' & folderidx.name & '...'
              iretval=objShell.Run ('wusa.exe ' & sfolder & folderidx.name & ' /quiet /norestart', 1, True)
              If (iRetVal = 0) or (iRetVal = 3010) then
               logFile.WriteLine('Success.')
               wscript.echo 'Success.'
               iSuccess = iSuccess + 1
              Else
               logFile.WriteLine('Failed.')
               wscript.echo 'Failed.'
               iFail = iFail + 1
              End If
             ElseIf Ucase(Right(folderIdx.name,3)) = 'EXE' Then
              logFile.WriteLine('Installing ' & folderidx.name & '...')
              wscript.echo 'Installing ' & folderidx.name & '...'
              iretval = objShell.Run(folderidx.name & ' /q /norestart', 1, True)
              If (iRetVal = 0) or (iRetVal = 3010) then
               logFile.WriteLine('Success.')
               wscript.echo 'Success.'
               iSuccess = iSuccess + 1
              Else
               logFile.WriteLine('Failed.')
               wscript.echo 'Failed.'
               iFail = iFail + 1
              End If
             End If
            Next

            wscript.echo iSuccess & ' update(s) installed successfully and ' & iFail & ' update(s) failed. See C:\log.txt for details.'
 
            End Sub

            Main()" + "</pre>";
        public const string content_04082012_k = "Windows Updates install shell script";
        public const string content_04082012_d = "Installing Windows Updates via Shell Script";

        //"Game of Life Exercise and Extension Methods"
        public const string content_31072012_b = "<p>A quick attempt in writing the Game of Life simulator using WPF. As a side goal, I wanted to better understand the extension methods in C# so I tried to move as much code as possible into the <strong>Helper</strong> class without sacrificing readability.</p>";
        public const string content_31072012_r = "<p><strong>PopulateGrid</strong> adds 50 rows and columns to a WPF Grid, creating a 50x50 matrix of cell. It then adds a rectangle to each cell so coloring could be applied. <strong>RePaintCell</strong> changes the background color of the cell at position i,j. <strong>InitializeArray</strong> just fills a 50x50 array of booleans, each value representing either a live or dead cell. <strong>CheckCell</strong> checks a single cell to find out if it will be live or dead in the next iteration. <strong>FillArray</strong> uses <strong>CheckCell</strong> to analyse the current array and construct the array of the next iteration. <strong>DrawArray</strong> compares the current and next iterations. If there is difference in the color of the cell at i,j, this cell is painted appropriately, otherwise it is skipped. Finally, <strong>AddGlider</strong> adds a glider element to the empty array so it was easy to test that the game runs correctly. The full listing is below.</p><p><strong>MainWindow.xaml</strong></p><pre class=\"brush: xml\">" +
                    @"&lt;Window x:Class='GameOfLife.MainWindow'
                    xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                    xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
                    Title='MainWindow' Height='386\' width=\'525'&gt;
                &lt;Grid Name='MainGrid'&gt;
                    &lt;Grid Name='DynamicGrid' ShowGridLines='True'&gt;
                    &lt;/Grid&gt;
                    &lt;Button Content='Button' Height='23' HorizontalAlignment='Left' Margin='25,316,0,0' Name='button1' VerticalAlignment='Top\' width=\'75' Click='button1_Click' /&gt;
                &lt;/Grid&gt;
            &lt;/Window&gt;" + "</pre><p><strong>MainWindow.xaml.cs</strong></p><pre class=\"brush: csharp\">" +
                    @"using System.Windows;
            using System.Windows.Media;
            using System.Windows.Threading;
            using System;

            namespace GameOfLife
            {
                public partial class MainWindow : Window
                {
                    private bool[,] arrOne = new bool[Helper.x, Helper.y];
                    private bool[,] arrTwo = new bool[Helper.x, Helper.y];
                    private DispatcherTimer dt = new DispatcherTimer();
                    private int count;

                    public MainWindow()
                    {
                        InitializeComponent();
                        dt.Interval = TimeSpan.FromMilliseconds(100);
                        dt.Tick += dt_Tick;
                        dt.Start();
                        Loaded += MainWindow_Loaded;
                    }

                    void MainWindow_Loaded(object sender, RoutedEventArgs e)
                    {
                        DynamicGrid.PopulateGrid(new SolidColorBrush(Colors.Blue));
                        InitializeGame();
                    }

                    void InitializeGame()
                    {
                        arrOne.InitializeArray(false);
                        arrTwo.InitializeArray(false);
                        arrOne.AddGlider(20, 30);
                        arrOne.DrawArray(arrTwo, DynamicGrid);
                    }

                    void dt_Tick(object sender, EventArgs e)
                    {
                        if(true)
                        {
                            bool[,] arrCurrent = count%2 == 0 ? arrOne : arrTwo;
                            bool[,] arrNext = count%2 == 0 ? arrTwo : arrOne;

                            arrNext.FillArray(arrCurrent);
                            arrNext.DrawArray(arrCurrent, DynamicGrid);
                            count++;
                        }
                    }

                    private void button1_Click(object sender, RoutedEventArgs e)
                    {
                        if(dt.IsEnabled)
                            dt.Stop();
                        else
                            dt.Start();
                    }
                }
            }" + "</pre><p><strong>Helper.cs</strong></p><pre class=\"brush: csharp\">" +
                    @"using System.Linq;
            using System.Windows.Controls;
            using System.Windows.Shapes;
            using System.Windows.Media;

            namespace GameOfLife
            {
                public static class Helper
                {
                    public const int x = 50;
                    public const int y = 50;

                    public static void PopulateGrid(this Grid grid, SolidColorBrush brush)
                    {
                        for (int i = 0; i &lt; y; i++)
                        {
                            grid.RowDefinitions.Add(new RowDefinition());
                        }

                        for (int j = 0; j &lt; x; j++)
                        {
                            grid.ColumnDefinitions.Add(new ColumnDefinition());
                        }

                        for (int i = 0; i &lt; y; i++)
                        {
                            for (int j = 0; j &lt; x; j++)
                            {
                                Rectangle rect = new Rectangle();
                                rect.SetValue(Grid.RowProperty, i);
                                rect.SetValue(Grid.ColumnProperty, j);
                                rect.Fill = brush;
                                grid.Children.Add(rect);
                            }
                        }
                    }

                    public static void RePaintCell(this Grid grid, int i, int j, SolidColorBrush brush)
                    {
                        Rectangle cell = grid.Children.Cast&lt;Rectangle&gt;().First(r =&gt; Grid.GetRow(r) == j && Grid.GetColumn(r) == i);
                        cell.Fill = brush;
                    }

                    public static void InitializeArray&lt;T&gt;(this T[,] arr, T value)
                    {
                        int iDim = arr.GetLength(0);
                        int jDim = arr.GetLength(1);
                        for (int i = 0; i &lt; iDim; i++)
                        {
                            for (int j = 0; j &lt; jDim; j++)
                            {
                                arr[i, j] = value;
                            }
                        }
                    }

                    public static void AddGlider(this bool[,] arr, int x, int y)
                    {
                        arr[x - 1, y] = false;
                        arr[x - 1, y + 1] = false;
                        arr[x - 1, y + 2] = true;

                        arr[x, y] = true;
                        arr[x, y + 1] = false;
                        arr[x, y + 2] = true;

                        arr[x + 1, y] = false;
                        arr[x + 1, y + 1] = true;
                        arr[x + 1, y + 2] = true;
                    }

                    public static void FillArray(this bool[,] arr, bool[,]arrCurrent)
                    {
                        for (int i = 0; i &lt; y; i++)
                        {
                            for (int j = 0; j &lt; x; j++)
                            {
                                arr[i, j] = arrCurrent.CheckCell(i, j);
                            }
                        }
                    }

                    public static void DrawArray(this bool[,] arr, bool[,] arrCurrent, Grid grid)
                    {
                        SolidColorBrush redBrush = new SolidColorBrush(Colors.Red);
                        SolidColorBrush blueBrush = new SolidColorBrush(Colors.Blue);

                        for (int i = 0; i &lt; y; i++)
                        {
                            for (int j = 0; j &lt; x; j++)
                            {
                                if (arr[i, j] != arrCurrent[i, j])
                                {
                                    SolidColorBrush brush = arr[i, j] ? redBrush : blueBrush;
                                    grid.RePaintCell(i, j, brush);
                                }
                            }
                        }
                    }

                    public static bool CheckCell(this bool[,] arr, int i, int j)
                    {
                        int nextI = i == (x - 1) ? 0 : i + 1;
                        int prevI = i == 0 ? x - 1 : i - 1;
                        int nextJ = j == (y - 1) ? 0 : j + 1;
                        int prevJ = j == 0 ? y - 1 : j - 1;

                        bool[] neighbours = new[]{
                                                    arr[prevI, prevJ],   arr[i, prevJ],   arr[nextI, prevJ],
                                                    arr[prevI, j],       arr[nextI, j],   arr[prevI, nextJ],
                                                    arr[i, nextJ],       arr[nextI, nextJ]
                                                };

                        int val = neighbours.Count(c =&gt; c);

                        if (arr[i, j])
                            return (val &gt;= 2 && val &lt;= 3) ? true : false;
                        else
                            return (val == 3) ? true : false;
                    }
                }
            }" + "</pre><p><strong>Reference</strong></p><a href=\"http://en.wikipedia.org/wiki/Conway%27s_Game_of_Life\">Conway's Game of Life</a><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/31072012_Game_of_life.png\" alt=\"Game of life\" /></div>" + "<p align=\"center\">The results are impressive</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_31072012_k = "Development programming C# MVC Game of Life";
        public const string content_31072012_d = "Implementing Game of Life using C# and MVC";

        // "Learning MVC: Vertical Pop-Out Menu"
        public const string content_17072012_b = "<p>Adding a pop-out menu to the application turned out to be two major steps. The second step can be broken down into obvious sub-steps too:</p>";
        public const string content_17072012_r = "<ul><li>Create HTML structure for the menu and apply the CSS</li><li>Create a ViewModel for the menu partial view and fill it with data<ul><li>Create a ViewModel</li><li>Create a controller to fill it with data</li><li>Modify the view to render the ViewModel properly</li></ul></li></ul><p>The first step I'm not describing here because I used a well-written tutorial[1]. I will only record here the styles I added to <strong>Site.css</strong>:</p><pre class=\"brush: css\">" +
            @"#menu {
                width: 12em;
                background: #eee;
            }

            #menu ul {
                list-style: none;
                margin: 0;
                padding: 0;
            }

            #menu a, #menu h2 {
                font: bold 11px/16px arial, helvetica, sans-serif;
                display: block;
                border-width: 1px;
                border-style: solid;
                border-color: #ccc #888 #555 #bbb;
                margin: 0;
                padding: 2px 3px;
            }

            #menu h2 {
                color: #fff;
                background: #000;
                text-transform: uppercase;
            }

            #menu a {
                color: #000;
                background: #efefef;
                text-decoration: none;
            }

            #menu a:hover {
                color: #a00;
                background: #fff;
            }

            #menu li {position: relative;}

            #menu ul ul ul {
                position: absolute;
                top: 0;
                left: 100%;
                width: 100%;
            }

            div#menu ul ul ul,
            div#menu ul ul li:hover ul ul
            {display: none;}

            div#menu ul ul li:hover ul,
            div#menu ul ul ul li:hover ul
            {display: block;}" + "</pre><p>The second step I'll write down in more detail.</p><p><strong>1.</strong> Create a partial view for the left sidebar. I decided to render the partial view by calling <strong>Html.Action</strong>, so I modified the div that holds the left sidebar in <strong>_Layout.shtml</strong> to look like this:</p><pre class=\"brush: xml\">" +
                    @"&lt;div id='left-sidebar'&gt;
                @Html.Action('MenuResult', 'LeftMenu')
            &lt;/div&gt;" +
                    "</pre><p>Then I created a partial view called <strong>MenuResult.shtml</strong> and placed it in the <strong>Shared</strong> folder. This is how the HTML structure looks like:</p><pre class=\"brush: xml\">" +
                    @"@model Recipes.Models.LeftMenuViewModel

            @{ Layout = null; }

            &lt;div id='menu'&gt;
                &lt;ul&gt;
                    &lt;li&gt;&lt;h2&gt;Recipes Menu&lt;/h2&gt;
                        &lt;ul&gt;
                            &lt;li&gt;@Html.ActionLink('Recipes', '../Recipe/Index')
                                &lt;ul&gt;
                                        &lt;li&gt;Test menu item
                                        &lt;ul&gt;
                                                &lt;li&gt;Test child menu item&lt;/li&gt;
                                        &lt;/ul&gt;
                                    &lt;/li&gt;                           
                                &lt;/ul&gt;
                            &lt;/li&gt;
                        &lt;/ul&gt;
                    &lt;/li&gt;
                &lt;/ul&gt;
            &lt;/div&gt;" +
                    "</pre><p><strong>2.</strong> ViewModel. After I was satisfied with the way the \"stub\" menu works, I started working on the model for the partial view. My first attempt looked something like this, a pretty simple model:</p><pre class=\"brush: csharp\">" +
                    @"public class LeftMenuViewModel
            {
                public List&lt;Category&gt; Categories { get; set; }
            }" + "</pre><p>And a pretty simple nested foreach iterator that attempts to render the view:</p><pre class=\"brush: xml\">" +
                    @"&lt;ul&gt;
                &lt;li&gt;@Html.ActionLink('Recipes', '../Recipe/Index')
                &lt;ul&gt;
                @foreach(var cat in Model.Categories)
                {
                &lt;li&gt;@Html.ActionLink(cat.CategoryName, @Html.CategoryAction(cat.CategoryID).ToString())
                &lt;ul&gt;
                    @foreach(var subcat in cat.SubCategories)
                    {
                    &lt;li&gt;@Html.ActionLink(subcat.SubCategoryName, @Html.SubCategoryAction(subcat.SubCategoryID).ToString())&lt;/li&gt;
                    }
                &lt;/ul&gt;
                &lt;/li&gt;                           
                }
                &lt;/ul&gt;
                &lt;/li&gt;
            &lt;/ul&gt;" +
                    "</pre><p><strong>3.</strong> Controller. The initial version of the controller looked something like this:</p><pre class=\"brush: csharp\">" +
                    @"public class LeftMenuController : Controller
            {
                public PartialViewResult MenuResult()
                {
                LeftMenuViewModel viewModel = new LeftMenuViewModel();

                using (RecipesEntities db = new RecipesEntities())
                {
                viewModel.Categories = db.Categories.ToList();
                foreach (var cat in viewModel.Categories)
                {
                cat.SubCategories = db.SubCategories.Where(s => s.CategoryID == cat.CategoryID).ToList();
                }
                }
                return PartialView(viewModel);
                }
            }" + "</pre><p>That was the initial attempt, but when I ran this, I was presented with the following exception: <i>The ObjectContext instance has been disposed and can no longer be used for operations that require a connection.</i></p>" +
               "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/17072012_ObjectDisposedException.png\" alt=\"ObjectDisposedException\" /></div><p align=\"center\">The ObjectContext instance has been disposed</p><p>That looked a bit weird to me - I can see the elements of the collection, but the application refuses to iterate through them, complaining about the context. I found a couple of thoughtful posts on the reason for such a behaviour. First post [2] suggested that most likely, the execution of the query was deferred and now, in the view, when it actually tries to execute the query, I have disposed of the DbContext already and it fails. The suggestion was to convert the query results to list using <strong>.ToList()</strong> so the query gets executed before disposing. That did not work. Another post [3] suggested replacing the foreach iterator with a for one for a number of reasons, but that did not help either.</p><p>I gave it some thought and chose an easy way out - remove the dependency on the LINQ and complex entity objects and create my own very simple class to use in the view model. Here is the final code, which worked for me:</p><p><strong>View Model</strong></p><pre class=\"brush: csharp\">" +
                    @"public class LeftMenuViewModel
            {
                public List&lt;MenuElement&gt; elements { get; set; }
            }

            public class MenuElement
            {
                public int id { get; set; }
                public string name { get; set; }
                public List&lt;MenuElement&gt; children { get; set; }

            }" + "</pre><p><strong>Controller</strong></p><pre class=\"brush: csharp\">" +
                    @"public class LeftMenuController : Controller
            {
                public PartialViewResult MenuResult()
                {
                LeftMenuViewModel viewModel = new LeftMenuViewModel();
                viewModel.elements = new List&lt;MenuElement&gt;();

                using (RecipesEntities db = new RecipesEntities())
                {
                List&lt;Category&gt; cats = db.Categories.ToList();
                foreach (var category in cats)
                {
                            MenuElement element = new MenuElement() {id = category.CategoryID, name = category.CategoryName, children = new List&lt;MenuElement&gt;()};

                List&lt;SubCategory&gt; subcats =
                    db.SubCategories.Where(s =&gt; s.CategoryID == category.CategoryID).ToList();

                foreach (var subcat in subcats)
                {
                    element.children.Add(new MenuElement(){id = subcat.SubCategoryID, name = subcat.SubCategoryName} );
                }
                viewModel.elements.Add(element);
                }
                }
                return PartialView(viewModel);
                }
            }" + "</pre><p><strong>View</strong></p><pre class=\"brush: xml\">" +
                    @"@model Recipes.Models.LeftMenuViewModel

            @{ Layout = null; }

            &lt;div id='menu'&gt;
                &lt;ul&gt;
                    &lt;li&gt;&lt;h2&gt;Recipes Menu&lt;/h2&gt;
                        &lt;ul&gt;
                            &lt;li&gt;@Html.ActionLink('Recipes', '../Recipe/Index')
                                &lt;ul&gt;

                                    @for(int i=0; i&lt;Model.elements.Count(); i++)
                                    {
                                        &lt;li&gt;@Html.ActionLink(Model.elements[i].name, @Html.CategoryAction(Model.elements[i].id).ToString())
                                        &lt;ul&gt;
                                            @for(int j=0; j&lt;Model.elements[i].children.Count(); j++)
                                            {
                                                &lt;li&gt;@Html.ActionLink(Model.elements[i].children[j].name, @Html.SubCategoryAction(Model.elements[i].children[j].id).ToString())&lt;/li&gt;
                                            }
                                        &lt;/ul&gt;
                                    &lt;/li&gt;                           
                                    }
                                &lt;/ul&gt;
                            &lt;/li&gt;
                        &lt;/ul&gt;
                    &lt;/li&gt;
                &lt;/ul&gt;
            &lt;/div&gt;" +
                    "</pre><p>While this looks a bit more complex compared to the initial attempt, I think there is really much less space for error. Here is how the menu looks like:</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/17072012_Vertical_Menu.png\" alt=\"Vertical Menu\" /></div>" +
                    "<p align=\"center\">Left side pop-out menu</p><p><strong>References:</strong></p><a href=\"http://ago.tanfa.co.uk/css/examples/menu/tutorial-v.html\">CSS Pop-Out Menu Tutorial</a><br/><a href=\"http://stackoverflow.com/questions/5360372/the-objectcontext-instance-has-been-disposed-and-can-no-longer-be-used-for-opera\">The ObjectContext instance has been disposed and can no longer be used for operations that require a connection</a><br/><a href=\"http://stackoverflow.com/questions/8894442/mvc-razor-view-nested-foreachs-model\">MVC Razor view nested foreach's model</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_17072012_k = "C# MVC jQuery vertical pop out menu";
        public const string content_17072012_d = "Adding a vertical pop-out menu to the website";

        //"Learning MVC: Editing the Variable Length List"
        public const string content_05072012_b = "<p>Continuing the recipe database example, the next step is to not just keep a list of ingredients that are required for a recipe, but also to allow specifying the quantity of each ingredient. Up to this point, I did not need any \"mapping table\" due to the MVC magic - the <strong>Recipe</strong> object kept a list of ingredients, and the <strong>Ingredient</strong> object kept a list of recipes. That was all that MVC needed to resolve the many-to-many relationship between recipes and ingredients and I could get away with just two models.</p>";
        public const string content_05072012_r = "<p>Now that I want to know how much of the ingredient is required, I don't have a place to save this information. I don't think I can get away without the <strong>RecipeIngredient</strong> model any longer. I'm adding that and also giving the user the ability to add and remove ingredients. This requires a technique to edit and save a list of variable length, which is referenced at the end of this post and which I applied, with small modifications.</p><p>To display the list, the following technique is used: a div element which contains one entry from the list is placed in a partial view. Each time the user adds an entry to the list, another such div is appended to the list using an Ajax call to a jQuery append() function. Each time the user removes an entry, a div is removed, which is even easier. To begin with, I added the following to the recipe's Edit view</p><pre class=\"brush: js\">" +
             @"&lt;fieldset&gt;
             &lt;div id='editorRows'&gt;
             &lt;ol&gt;
              @foreach (var item in Model.RecipeIngredients)
              {
               @Html.Partial('_RIEditor', item)
              }
             &lt;/ol&gt;
             &lt;/div&gt;
             @Html.ActionLink('Add another ...', 'Add', null, new {id = 'addItem'})
            &lt;/fieldset&gt;" +
                    "</pre><p>The same partial view <strong>_RIEditor</strong> is repeated once per each ingredient. The \"Add another ...\" link will later add an ingredient when the user clicks it. And here's a sample _RIEditor partial view ( the <strong>Layout=null</strong> part was added because otherwise my partial view was rendered with the header and the footer).</p><pre class=\"brush: csharp\">" +
                    @"@using Recipes.HtmlHelpers
            @model Recipes.Models.RecipeIngredient
           
            @{ Layout = null; }

            &lt;div class='editorRow'&gt;
                @using (Html.BeginCollectionItem('RecipeIngredients'))
                {
                    &lt;li class='styled'&gt;
                    &lt;div class='display-label'&gt;Ingredient:&lt;/div&gt;@Html.TextBoxFor(model =&gt; model.Ingredient.IngredientName)
                    &lt;div class='display-label-nofloat'&gt;Amount:&lt;/div&gt;@Html.TextBoxFor(model =&gt; model.Quantity, new { size = 4 })
                    &lt;a href='#' class='deleteRow'&gt;delete&lt;/a&gt;
                    &lt;/li&gt;
                }
            &lt;/div&gt;" +
                    "</pre><p>The key part here is the <strong>Html.BeginCollectionItem</strong>, which renders a sequence of items that will later be bound to a single collection. In short, it keeps track of the items as they are added or deleted, and when the form is finally submitted, the neat collection of items is returned, ready to be saved to database.</p><p>Now to allow for adding or deleting elements, I need to add two functions. Here's the example:</p><pre class=\"brush: js\">" +
                    @"&lt;script type='text/javascript'&gt;
            &lt;!--
                $('#addItem').click(function () {
                    $.ajax({
                        url: this.href,
                        cache: false,
                        success: function (html) { $('#editorRows').append(html); }
                    });
                    return false;
                });

                $('a.deleteRow').live('click', function () {
                    $(this).parents('div.editorRow:first').remove();
                    return false;
                });
            --&gt;
            &lt;/script&gt;" +
                    "</pre><p>I also need to reference a couple of JavaScript files to make it work</p><pre class=\"brush: js\">" +
                    @"&lt;script src='@Url.Content('~/Scripts/MicrosoftAjax.js')' type='text/javascript'&gt;&lt;/script&gt;
            &lt;script src='@Url.Content('~/Scripts/MicrosoftMvcValidation.debug.js')' type='text/javascript'&gt;&lt;/script&gt;" +
                    "</pre><p>Almost done, now I only need to take care of that \"Add another ...\" link that I added to the Edit view. To make it work, I only need a simple action added to the controller. which will return the partial view.</p><pre class=\"brush: csharp\">" +
                    @"public ViewResult Add()
            {
             return View('_RIEditor', new RecipeIngredient());
            }" +
        "</pre><p>So, what have I achieved? Here's the first approximation of how my ingredient list may look like when I load the recipe from the database</p>" +
        "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/05072012_List_1.png\" alt=\"List 1\" /></div>" +
        "<p align=\"center\">Original List of Ingredients</p><p>And how it looks when I click \"Add another ...\": a line for a new ingredient is added, looking the same as other lines and I can enter some data</p>" +
        "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/05072012_List_2.png\" alt=\"List 2\" /></div>" +
        "<p align=\"center\">Modified List of Ingredients</p><p>And then I can verify that some data is returned back on Submit, so my changes are not being lost</p>" +
        "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/05072012_Count_equals_3.png\" alt=\"Count equals 3\" /></div><p align=\"center\">Data Posted by the View</p><p>The concept is working at this point - I get back my three \"RecipeIngredients\" and the data I entered. It's only the proof of concept at this point, I need to make a number of modifications to make it functional.</p><p><strong>References:</strong></p><a href=\"https://github.com/danludwig/BeginCollectionItem/blob/master/BeginCollectionItem/HtmlPrefixScopeExtensions.cs\">BeginCollectionItem source code</a><br/><a href=\"http://blog.codeville.net/2010/01/28/editing-a-variable-length-list-aspnet-mvc-2-style/\">Editing a variable length list, ASP.NET MVC 2-style</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_05072012_k = "C# MVC development jQuery variable length list";
        public const string content_05072012_d = "An example of handling the variable length list in MVC";

        //"Learning MVC: Unit Testing Validation in MVC"
        public const string content_14062012_b = "<p>This is a rather short post. A little \"trick\" is required to test validation. If the <strong>Create</strong> function is called directly from the unit test, and the entity is validated using <strong>DataAnnotation</strong> validation, the model binder will not be invoked and the validation will not take place. The test result, obviously, will not be the expected one. To deal with that, it is necessary to mimic the behavior of the model binder. The following test creates a <strong>ValidationContext</strong> and uses the <strong>DataAnnotations</strong>.Validator class to validate the model.</p>";
        public const string content_14062012_r = "<pre class=\"brush: csharp\">" +
            @"[TestMethod()]
            public void ValidateNameIsTooShort()
            {
             Ingredient ingredient = new Ingredient() { IngredientName = 'a' };

             var validationContext = new ValidationContext(ingredient, null, null);
             var validationResults = new List&lt;ValidationResult&gt;();
             Validator.TryValidateObject(ingredient, validationContext, validationResults);

             string error = GetValidationError(validationResults);

             Assert.AreEqual(error, Constants.Constants.IngredientNameTooShort);
            }" + "</pre><p>If any errors are caught by the validation, they will be added to the <strong>ModelState</strong> of the controller. To get it back and compare with my expected error message, I just need to retrieve the first error message from the list of <strong>ValidationResult</strong>.</p><pre class=\"brush: csharp\">" +
                    @"public string GetValidationError(List&lt;ValidationResult&gt; results)
            {
             foreach (var result in results)
             {
              if(!String.IsNullOrEmpty(result.ErrorMessage))
              {
               return result.ErrorMessage;
              }
             }
             return string.Empty;
            }" + "</pre><p><strong>References:</strong></p><a href=\"http://johan.driessen.se/posts/testing-dataannotation-based-validation-in-asp.net-mvc\">Testing DataAnnotation-based validation in ASP.NET MVC</a><br/><a href=\"http://stackoverflow.com/questions/1269713/unit-tests-on-mvc-validation\">Unit tests on MVC validation</a><br/><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_14062012_k = "C# ASP.NET MVC Unit testing validation";
        public const string content_14062012_d = "Learning MVC: Unit Testing Validation in MVC";

        //"Learning MVC: Unit Testing CRUD actions in MVC"
        public const string content_12062012_b = "<p>Continuing the example with a recipe database, and having the most basic structure working (recipes belong to categories and consist of ingredients), it is time to remember about <strong>TDD</strong> (test driven development). To be honest, it is already too late, because in proper TDD tests are supposed to be writter before the code is. I'll do my best next time. This time, however, there are some simple tests I can think of which relate to the <strong>Ingredient</strong> entity</p>";
        public const string content_12062012_r = "<ul><li>Test that an Ingredient can be inserted into the database</li><li>Test that an Ingredient can be edited</li><li>Test that an Ingredient can be deleted from the database</li><li>Test that an Ingredient can not be deleted if it is used by any Recipe</li><li>Test that an Ingredient with a name that is too short can not be created</li><li>Test that an Ingredient with a name that is too long can not be created</li><li>Test that an existing Ingredient can not be edited so that its name becomes too short</li><li>Test that an existing Ingredient can not be edited so that its name becomes too long</li></ul><p>In fact, the last four tests are not related to database manipulations because the errors will be caught before the attempt to save data is made. It would be more appropriate to place them in a separate class, which will test the model and I'll return to them next time. The first four tests appear to test database operations.</p><p>Firstly, I would like to have each test start from a known state - for example, an almost empty database which has some minimal amount of seed data. To achieve that, I use the initializer class that inherits from <strong>DropCreateDatabaseAlways</strong>. For reference, here is the full listing - the only thing I care about is to override the Seed function. I will use the data to test that I can not delete the ingredient which is in use.</p><pre class=\"brush: csharp\">" +
        @"public class TestDatabaseInitializer : DropCreateDatabaseAlways&lt;RecipesEntities&gt;
            {
             protected override void Seed(RecipesEntities context)
             {
              var category0 = new Category { CategoryName = 'Mains', Description = 'Main Dishes' };
              var category1 = new Category { CategoryName = 'Desserts', Description = 'Dessert Dishes' };
              var categories = new List&lt;Category&gt;() { category0, category1 };
              categories.ForEach(c =&gt; context.Categories.Add(c));

              var ingredient0 = new Ingredient { IngredientName = 'Meat' };
              var ingredient1 = new Ingredient { IngredientName = 'Fish' };
              var ingredient2 = new Ingredient { IngredientName = 'Potato' };
              var ingredients = new List&lt;Ingredient&gt;() { ingredient0, ingredient1, ingredient2 };
              ingredients.ForEach(i =&gt; context.Ingredients.Add(i));

              var recipes = new List&lt;Recipe&gt;();
              recipes.Add(new Recipe { RecipeName = 'Grilled fish with potatoes', Category = category0, Ingredients = new List&lt;Ingredient&gt;() { ingredient1, ingredient2 } });
              recipes.Add(new Recipe { RecipeName = 'Grilled steak with potatoes', Category = category0, Ingredients = new List&lt;Ingredient&gt;() { ingredient0, ingredient2 } });
              recipes.ForEach(r =&gt; context.Recipes.Add(r));
             }
            }" + "</pre><p>I added a small function to my test class to create a database.</p><pre class=\"brush: csharp\">" +
                    @"[TestInitialize()]
            public void SetupDatabase()
            {
             Database.DefaultConnectionFactory = new SqlCeConnectionFactory('System.Data.SqlServerCe.4.0');
             Database.SetInitializer&lt;RecipesEntities&gt;(new TestDatabaseInitializer());
            }" + "</pre><p>Now I think I'm ready to create a simple test.</p><pre class=\"brush: csharp\">" +
                    @"[TestMethod()]
            public void CreateTest()
            {
             SetupDatabase();
             IngredientController target = new IngredientController();
             Ingredient ingredient = new Ingredient() { IngredientName = 'test' };

             ActionResult actual = target.Create(ingredient);
             Assert.IsTrue(ingredient.IngredientID != 0);

             RecipesEntities db = new RecipesEntities();
             var newIngredient = db.Ingredients.Find(ingredient.IngredientID);
             Assert.AreEqual(ingredient.IngredientName, newIngredient.IngredientName);
            }" + "</pre><p>I create the <strong>Ingredient</strong>, record it's <strong>IngredientID</strong> and make sure that I can retrieve it back from the database by ID after it's added. Deletion and editing tests are equally simple. Now the \"negative\" test: I'm not testing what I can do now, but rather what I can not do. I should not be able to delete the ingredient if it is used by any recipes - that would destroy referential integrity. Here is a simple test that passes:</p><pre class=\"brush: csharp\">" +
                    @"[TestMethod()]
            public void CanNotDeleteUsedIngredient2()
            {
             SetupDatabase();
             IngredientController target = new IngredientController();
             RecipesEntities db = new RecipesEntities();
             var ingredient = db.Ingredients.Where(i =&gt; i.IngredientName == 'Meat').FirstOrDefault();
             int id = ingredient.IngredientID;

             Assert.IsNotNull(ingredient);
             ActionResult actual = target.DeleteConfirmed(id);

             db = new RecipesEntities();
             var deletedIngredient = db.Ingredients.Find(id);
             Assert.IsNotNull(deletedIngredient);
            }" + "</pre><p>Okay, I tried to delete the ingredient and then I verified and it is still in the database. That's expected, but that tells me nothing about the reason why the ingredient was not deleted. Maybe the whole database is offline or there is an uncommitted transaction. To improve the test, I would like to know something about the reason. I need to check for the errors in the <strong>ModelState</strong>. Fortunately, I can access the <strong>ModelState</strong> from the <strong>ActionResult</strong>. Here is what I could do to return the first error that is found in the <strong>ModelState</strong>:</p><pre class=\"brush: csharp\">" +
                    @"public string GetFirstErrorMessage(ActionResult result)
            {
             ViewResult vr = (ViewResult)result;

             foreach (ModelState error in vr.ViewData.ModelState.Values)
             {
              foreach (var innerError in error.Errors)
              {
               if (!string.IsNullOrEmpty(innerError.ErrorMessage))
               {
                return innerError.ErrorMessage;
               }
              }
             }
             return string.Empty;
            }" + "</pre><p>Now I can modify the test to check the <strong>ErrorMessage</strong>. The check is rather lame at the moment - the error message is created dynamically to tell the user what recipes exactly use the ingredient. So I do not want to check the full error message and I'm satisfied with the fact that the first 10 characters are what I expect. Here is the slightly modified test:</p><pre class=\"brush: csharp\">" +
                    @"[TestMethod()]
            public void CanNotDeleteUsedIngredient()
            {
             SetupDatabase();
             IngredientController target = new IngredientController();
             RecipesEntities db = new RecipesEntities();
             var ingredient = db.Ingredients.Where(i =&gt; i.IngredientName == 'Meat').FirstOrDefault();
             int id = ingredient.IngredientID;

             Assert.IsNotNull(ingredient);

             ActionResult actual = target.DeleteConfirmed(id);
             Assert.AreEqual(GetFirstErrorMessage(actual).Substring(0, 10), 'Cannot del');

             db = new RecipesEntities();
             var deletedIngredient = db.Ingredients.Find(id);
             Assert.IsNotNull(deletedIngredient);
            }" + "</pre><p><strong>References:</strong></p><a href=\"http://www.arrangeactassert.com/code-first-entity-framework-unit-test-examples/\">Code First Entity Framework Unit Test Examples</a><br/><a href=\"http://msdn.microsoft.com/en-us/vs2010trainingcourse_aspnetmvc3testing_topic4\">Exercise 2: Testing CRUD actions</a><br/><a href=\"http://stackoverflow.com/questions/1352948/how-to-get-all-errors-from-asp-net-mvc-modelstate\">How to get all Errors from asp.net mvc modelState?</a><br/><a href=\"http://stackoverflow.com/questions/2071095/how-to-get-the-model-from-an-actionresult\">How to get the Model from an ActionResult?</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_12062012_k = "C# MVC Development Unit testing CRUD operations actions";
        public const string content_12062012_d = "Learning MVC: Unit Testing CRUD actions in MVC";

        //"Learning MVC: Updating the Many-to-many Relationship"
        public const string content_15052012_b = "<p>Continuing the application from the last post, I was now going to use a <strong>MultiSelectList</strong> to update the many-to-many relationship. The use case is the following: suppose we have a recipe but want to update it - maybe the dish will benefit from adding a bit of pepper. So how do I go about adding something to the list of ingredients? The solution was not so straightforward and there were at least three \"gotchas\" on the way.</p>";
        public const string content_15052012_r = "<p><strong>Gotcha 1.</strong> To actually populate the <strong>MultiSelectList</strong>. The easy (but not quite straightforward) task was to create the <strong></strong>MultiSelectList and to populate it with all the ingredients. To do that I added the properties to the <strong>Recipe</strong> class called <strong>AllIngredients</strong> and <strong>SelectedIngredientIDs</strong>. The most basic <strong>Recipe</strong> class now looks like this:</p><pre class=\"brush: csharp\">" +
            @"public class Recipe
            {
             [ScaffoldColumn(false)]
             public int RecipeID { get; set; }
             public string RecipeName { get; set; }

             public virtual ICollection&lt;Ingredient&gt; Ingredients { get; set; }
             public IEnumerable&lt;int&gt; SelectedIngredientIDs { get; set; }
             public ICollection&lt;Ingredient&gt; AllIngredients { get; set; }

             public Recipe()
             {
              Ingredients = new HashSet&lt;Ingredient&gt;();
             }
            }" + "</pre><p>In the controller, I populate all ingredients from the database into the <strong>AllIngredients</strong> and then the IDs of the ingredients of the recipe into the <strong>SelectedIngredientIDs</strong>.</p><pre class=\"brush: csharp\">" +
                    @"Recipe recipe = recipeDB.Recipes.Find(id);
            recipe.AllIngredients = recipeDB.Ingredients.ToList();

            recipe.SelectedIngredientIDs = Enumerable.Empty&lt;int&gt;();
            foreach (Ingredient ing in recipe.Ingredients)
            {
             recipe.SelectedIngredientIDs = recipe.SelectedIngredientIDs.Concat(new[] {ing.IngredientID});
            }" + "</pre><p>Then I create a <strong>MultiSelectList</strong> as follows</p><pre class=\"brush: csharp\">" +
                    @"@Html.ListBoxFor(model =&gt; model.SelectedIngredientIDs, new MultiSelectList(Model.AllIngredients, 'IngredientID', 'IngredientName'), new {Multiple = 'multiple'})" + "</pre><p><strong>Gotcha 2.</strong> Preselect the current ingredients in the list. The application works by now and the list is populated, but nothing is selected. Why is that? I checked the <strong>SelectedIngredientIDs</strong> and they are populated properly. The trick was to find out that MVC uses the <strong>ToString</strong> method as a way to determine if an item is selected or not, so I had to override it in the <strong>Ingredient</strong> class. Just added a piece of code below and it started working like magic.</p><pre class=\"brush: csharp\">" +
                    @"public class Ingredient
            {
             public int IngredientID { get; set; }
 
             ...
 
             public override string ToString()
             {
              return this.IngredientID.ToString();
             }
            }" + "</pre><p><strong>Gotcha 3.</strong> Finally, I had my list being populated and I also could change the selection to my content. However, no exceptions were thrown but also no updates were being saved to the database. The not-so-little trick was to find out how exactly to let the Entity Framework know what needs to be updated. Here is the slightly simplified <strong>HttpPost</strong> method (try/catch omitted etc) which worked, with comments.</p><pre class=\"brush: csharp\">" +
                    @"[HttpPost]
            public ActionResult Edit(Recipe recipe)
            {
             if(ModelState.IsValid)
             {
              //get the id of the current recipe
              int id = recipe.RecipeID;
              //load recipe with ingredients from the database
              var recipeItem = recipeDB.Recipes.Include(r =&gt; r.Ingredients).Single(r =&gt; r.RecipeID == id);
              //apply the values that have changed
              recipeDB.Entry(recipeItem).CurrentValues.SetValues(recipe);
              //clear the ingredients to let the framework know they have to be processed
              recipeItem.Ingredients.Clear();
              //now reload the ingredients again, but from the list of selected ones as per model provided by the view
              foreach (int ingId in recipe.SelectedIngredientIDs)
              {
               recipeItem.Ingredients.Add(recipeDB.Ingredients.Find(ingId));
              }
              //finally, save changes as usual
              recipeDB.SaveChanges();
              return RedirectToAction('Index');
             }
             return View(recipe);
            }" + "</pre><p>I'm anticipating the gotchas in the Create view already!</p><p><strong>References I used</strong></p><a href=\"http://stackoverflow.com/questions/3737985/asp-net-mvc-multiselectlist-with-selected-values-not-selecting-properly\">ASP.NET MVC MultiSelectList with selected values not selecting properly</a><br><a href=\"http://www.i-script.nl/?p=48\">MVC 3 Quirk: MultiSelect with selected items</a><br><a href=\"http://stackoverflow.com/questions/5850649/many-to-many-relationship-basic-example-mvc3\">Many-To-Many Relationship Basic Example (MVC3)</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_15052012_k = "MVC C# Development programming update many-to-many relationship";
        public const string content_15052012_d = "Updating the Many-to-many Relationships with MVC";

        //"Learning MVC - Code First and Many-to-many Relationship"
        public const string content_13052012_b = "<p>Learning MVC I came across a need to create a many-to-many relationship between entities using the code first approach. For an example, let's consider the Recipes database which holds recipes and ingredients. A recipe has multiple ingredients, such as meat, potatoes, pepper and so on. At the same time an ingredient may belong to multiple recipes, i.e. you can cook meat with potatoes but fish and chips will also require potatoes. That's a classic many-to-many relationship which has a classic mapping table solution, where one many-to-many relationship would convert to two one-to-many by adding a new table. The diagram would look like the following:</p>";
        public const string content_13052012_r =
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/13052012_Many_to_many_relationship.png\" alt=\"Many to many relationship\" /></div>" +
            "<p align=\"center\">Typical database solution for many-to-many relationship</p><p>So my first guess was that I will have to create three classes while implementing a similar structure with MVC code first. Fortunately, so far it appears to be easier than that. Below is the small exercise that creates a most basic MVC project from scratch and illustrates the many-to-many relationship via code first.</p><p>Start Visual Studio 2010 and select <strong>File</strong> -> <strong>New Project</strong>, select <strong>ASP.NET MVC 3 Web Application</strong>. On the next screen I selected <strong>Empty</strong> application to make the example most simple.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/13052012_New_project.png\" alt=\"New project\" /></div>" +
            "<p align=\"center\">Create an empty MVC 3 application</p><p>After the project was created, I added a <strong>HomeController</strong> to create a basic home page. Probably not necessary, but lets the application run without displaying an error. Right-click <strong>Controllers</strong> folder, select <strong>Add</strong> -> <strong>Controller</strong> and add an empty <strong>HomeController</strong>.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/13052012_Add_controller.png\" alt=\"Add controller\" /></div>" +
            "<p align=\"center\">Add a HomeController</p><p>Inside the <strong>HomeController.cs</strong>, the <strong>Index()</strong> method, right-click and select <strong>Add View</strong>. Leave default values and click <strong>Add</strong>.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/13052012_Add_view.png\" alt=\"Add view\" /></div>" +
                    "<p align=\"center\">Create an Index page for the project</p><p>Run the project to verify that no errors are displayed. Now it is the time to add entities for the Recipe and Ingredient. Right-click <strong>Models</strong> folder, select <strong>Add</strong> -> <strong>Class</strong> and call the class <strong>Recipe.cs</strong>. Add another one called <strong>Ingredient.cs</strong>. The little trick with a many-to-many relationship is to create an <strong>ICollection</strong> within each of the two related entities. The collection will actually hold those \"many\" entities that are related to the particular instance. In our case - the <strong>Recipe</strong> holds a list of all <strong>Ingredients</strong> it uses.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/13052012_Add_class.png\" alt=\"Add class\" /></div><p align=\"center\">Add new class in Models folder</p><pre class=\"brush: csharp\">" +
                    @"public class Recipe
            {
             public int RecipeID { get; set; }
             public string Name { get; set; }

             public virtual ICollection&lt;Ingredient&gt; Ingredients { get; set; }

             public Recipe()
             {
              Ingredients = new HashSet&lt;Ingredient&gt;();
             }
            }

            public class Ingredient
            {
             public int IngredientID { get; set; }
             public string Name { get; set; }

             public virtual ICollection&lt;Recipe&gt; Recipes { get; set; }

             public Ingredient()
             {
              Recipes = new HashSet&lt;Recipe&gt;();
             }
            }" + "</pre><p>And we'll need to have some sample data to verify that the application is working as expected. So add another class and call it <strong>SampleData.cs</strong>. Entity Framework allows to \"seed\" the newly created database. An implementation of <strong>DropCreateDatabaseIfModelChanges</strong> class will re-seed the database when model changes. This is handy for testing.</p><pre class=\"brush: csharp\">" +
                    @"public class SampleData : DropCreateDatabaseIfModelChanges&lt;RecipesEntities&gt;
            {
             protected override void Seed(RecipesEntities context)
             {
              var ingredient0 = new Ingredient{Name = 'Meat'};
              var ingredient1 = new Ingredient{Name = 'Fish'};
              var ingredient2 = new Ingredient{Name = 'Potato'};

              var ingredients = new List&lt;Ingredient&gt;(){ingredient0, ingredient1, ingredient2};

              ingredients.ForEach(i =&gt; context.Ingredients.Add(i));

              var recipes = new List&lt;Recipe&gt;();
  
              recipes.Add(new Recipe{Name = 'Grilled fish with potatoes', Ingredients = new List&lt;Ingredient&gt;() {ingredient1, ingredient2}});
              recipes.Add(new Recipe{Name = 'Grilled steak with potatoes', Ingredients = new List&lt;Ingredient&gt;() {ingredient0, ingredient2}});

              recipes.ForEach(r =&gt; context.Recipes.Add(r));
             }
            }" + "</pre><p>Next step was to create the database. Right-click <strong>Project</strong>, select <strong>Add</strong> -> <strong>Add ASP.NET Folder</strong> -> <strong>App_Data</strong>. The folder will be created. It will already have the correct security access settings.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/13052012_Add_asp.net_folder.png\" alt=\"Add asp.net folder\" /></div>" +
            "<p align=\"center\">Create an App_Data folder</p><p>Next, I added the following at the end of the web.config file just before the closing \"configuration\" tag. Now the Entity Framework will know how to connect to the database:</p><pre class=\"brush: xml\">" +
                    @"&lt;connectionStrings&gt;
             &lt;add name='RecipesEntities'
             connectionString='Data Source=|DataDirectory|Recipes.sdf'
             providerName='System.Data.SqlServerCe.4.0'/&gt;
            &lt;/connectionStrings&gt;" + "</pre><p>Next step is to create a context class, which will represent the Entity Framework database context. It is very simple indeed, and I'll create it by adding a new class called <strong>RecipeEntities.cs</strong> in the model folder. This class will be able to handle database operations due to the fact that it is extending <strong>DbContext</strong>. Here is the code:</p><pre class=\"brush: csharp\">" +
                    @"public class RecipesEntities : DbContext
            {
             public DbSet&lt;Recipe&gt; Recipes { get; set; }
             public DbSet&lt;Ingredient&gt; Ingredients { get; set; }
            }" + "</pre><p>Now, with the model, database, context and some sample data in place, it is time to verify that data is actually displayed properly by the application. First, the recipes. I'm going to check that the list is displayed properly and that I can display all the details I'm interested in (for now, just the list of ingredients). For that, I'll need a <strong>RecipeController</strong> and two views, <strong>List</strong> and <strong>Details</strong>. First, the controller, that I will create with default methods for displaying, editing, creating and deleting data. I'm only interested in two methods, which I'll modify as follows</p><pre class=\"brush: csharp\">" +
                    @"RecipesEntities recipeDB = new RecipesEntities();

            // GET: /Recipe/
            public ActionResult Index()
            {
             var recipes = recipeDB.Recipes.ToList();
             return View(recipes);
            }

            // GET: /Recipe/Details/5
            public ActionResult Details(int id)
            {
             var recipe = recipeDB.Recipes.Find(id);
             return View(recipe);
            }" + "</pre><p>Now I'll right-click within the <strong></strong>Index method and select <strong>Add View</strong>, which I will configure as follows:</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/13052012_Add_view_2.png\" alt=\"Add view 2\" /></div>" +
            "<p align=\"center\">Create the List view</p><p>Now if I run the application as it is and navigate to <strong>Recipe</strong> (<u>http://localhost/Recipe</u>), I should see the list of recipes.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/13052012_Index.png\" alt=\"Index\" /></div>" +
            "<p align=\"center\">Display the list of ingredients</p><p>Next, I want to see the details. I'll add another view by right-clicking within the <strong>Details</strong> method of the controller and select <strong>Add View</strong>, which I will configure in the similar fashion:</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/13052012_Add_view_3.png\" alt=\"Add view 3\" /></div>" +
            "<p align=\"center\">Create the Details view</p><p>The default contents of the view will look similar to this:</p><pre class=\"brush: xml\">" +
                    @"@model RecipesSample.Models.Recipe
            @{
                ViewBag.Title = 'Details';
            }

            &lt;h2&gt;Details&lt;/h2&gt;
            &lt;fieldset&gt;
                &lt;legend&gt;Recipe&lt;/legend&gt;
                &lt;div class='display-label'&gt;Name&lt;/div&gt;
                &lt;div class='display-field'&gt;@Model.Name&lt;/div&gt;
            &lt;/fieldset&gt;
            &lt;p&gt;
                @Html.ActionLink('Edit', 'Edit', new { id=Model.RecipeID }) |
                @Html.ActionLink('Back to List', 'Index')
            &lt;/p&gt;" + "</pre><p>This, however, will only display the name of the recipe, but not the ingredients. To check that the ingredients are returned from the database properly, I have to modify the view to look similar to this:</p><pre class=\"brush: xml\">" +
                    @"@model Recipes.Models.Recipe
            @{
                ViewBag.Title = 'Details';
            }

            &lt;h2&gt;Details&lt;/h2&gt;

            &lt;fieldset&gt;
                &lt;legend&gt;Recipe&lt;/legend&gt;
                &lt;div class='display-label'&gt;Name&lt;/div&gt;
                &lt;div class='display-field'&gt;@Model.Name&lt;/div&gt;
            &lt;/fieldset&gt;

            &lt;fieldset&gt;
                &lt;legend&gt;Ingredients&lt;/legend&gt;
                @foreach (Recipes.Models.Ingredient ingredient in Model.Ingredients)
                { 
                    &lt;div&gt;@Html.DisplayFor(model =&gt; ingredient.Name)&lt;/div&gt;
                }
            &lt;/fieldset&gt;

            &lt;p&gt;
                @Html.ActionLink('Edit', 'Edit', new { id=Model.RecipeID }) |
                @Html.ActionLink('Back to List', 'Index')
            &lt;/p&gt;" + "</pre><p>Now if I run the application, navigate to <strong>Recipe</strong> and click <strong>Details</strong> on any of them (the link will point to <u>http://localhost:49606/Recipe/Details/1</u> or similar), I will see the following page:</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/13052012_Details.png\" alt=\"Details\" /></div>" +
            "<p align=\"center\">Display the related data</p><p>The recipe ingredients were successfully extracted from the model and displayed. As an exercise, it's easy to perform reverse action - check that if the ingredient is displayed, the recipes where it is used can also be shown. Hint: the view code may look similar to this:</p><pre class=\"brush: xml\">" +
                    @"@model Recipes.Models.Ingredient
            @{
                ViewBag.Title = 'Details';
            }

            &lt;h2&gt;Details&lt;/h2&gt;

            &lt;fieldset&gt;
                &lt;legend&gt;Ingredient&lt;/legend&gt;
                &lt;div class='display-label'&gt;Name&lt;/div&gt;
                &lt;div class='display-field'&gt;@Model.Name&lt;/div&gt;
            &lt;/fieldset&gt;

            &lt;fieldset&gt;
                &lt;legend&gt;Is used in the following recipes&lt;/legend&gt;
                @foreach (Recipes.Models.Recipe recipe in Model.Recipes)
                { 
                    &lt;div&gt;@Html.DisplayFor(model =&gt; recipe.Name)&lt;/div&gt;
                }
            &lt;/fieldset&gt;
            &lt;p&gt;
                @Html.ActionLink('Edit', 'Edit', new { id=Model.IngredientID }) |
                @Html.ActionLink('Back to List', 'Index')
            &lt;/p&gt;" + "</pre><p>And the output will be</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/13052012_Details_2.png\" alt=\"Details 2\" /></div><p align=\"center\">Display the related data</p><p>This is a very crude example without following \"best practices\" (such as creating the DbContext inside the using statement) and without any formatting, but it shows that an application that employs the many-to-many relationships between the entities can be created with MVC with just several lines of code.</p><p>References I used:</p><a href=\"http://www.asp.net/mvc/tutorials/mvc-music-store/mvc-music-store-part-4\">Part 4: Models and Data Access </a><br/><a href=\"http://www.codeproject.com/Articles/234606/Creating-a-Many-To-Many-Mapping-Using-Code-First\">Creating a Many To Many Mapping Using Code First</a><br/><a href=\"http://stackoverflow.com/questions/5741109/the-type-or-namespace-name-dbcontext-could-not-be-found\">The type or namespace name 'DbContext' could not be found</a><br/><a href=\"http://msdn.microsoft.com/en-us/library/gg679604(v=vs.103).aspx\">DropCreateDatabaseIfModelChanges<TContext> Class</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_13052012_k = "C# MVC Many-to-many relationship code first enterprise framework";
        public const string content_13052012_d = "Code First and Many-to-many Relationship with MVC and Enterprise Framework";

        //"Converting a Physical PC to VM with VMWare Converter"
        public const string content_02052012_b = "<p>When the software I'm working on is installed on the PC which is later shipped to the client, the PC has an exact configuration and the operating system is set up in a certain way, which is precisely documented. To make sure every workstation has exactly the same configuration, the images of the hard disk partitions are created once and then copied over to every PC. There are cases, however, when the same configuration needs to be applied to the Virtual Machine - for example, to simplify some of the testing tasks. In this case the desired action is to use the preconfigured PC and to convert it to the Virtual Machine. Since we use the VMWare products, the tool that I use also comes from VMWare and it's called <a href=\"http://www.vmware.com/products/converter/\">VMware vCenter Converter</a> (aka vConverter Standalone).</p>";
        public const string content_02052012_r = "<p>To start with, I downloaded and installed VMware vCenter Converter (aka vConverter Standalone). In my case, Local installation was sufficient. In local mode I can only create and manage conversion tasks from the PC on which I install the converter. The client-server installation allows to create and manage conversion tasks remotely in case you need that.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/02052012_Setup_Type.png\" alt=\"Setup Type\" /></div>" +
            "<p align=\"center\">Choose \"Local Installation\".</p><p>Two important configurations have to be set up on the source PC (the PC that is converted to VM) before the conversion starts. First, file sharing has to be disabled.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/02052012_Turn_off_file_and_printer_sharing.png\" alt=\"Turn off file and printer sharing\" /></div>" +
            "<p align=\"center\">Turn off file sharing</p><p>Also, the Windows firewall has to be disabled.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/02052012_Customize_settings.png\" alt=\"Customize settings\" /></div>" +
            "<p align=\"center\">Disable Windows Firewall</p><p>You should also check some other things, such as the Windows version being supported, network access, no other conversion jobs running on the source machine and no VMware Converter installations existing on the source PC. After all that is taken care of, I ran the client application. In the application, I selected <strong>Convert machine</strong> and filled in the details.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/02052012_Source_System.png\" alt=\"Source System\" /></div>" +
            "<p align=\"center\">Source PC details</p><p>When the connection succeeded, I saw the following message. Just to make sure that I will not forget to remove the agent and to free myself from extra hassle, I chose the automatic version.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/02052012_Automatically_Uninstall.png\" alt=\"Automatically Uninstall\" /></div>" +
            "<p align=\"center\">Select the uninstallation method for Converter Standalone</p><p>Next step was to provide the location where the virtual machine will be saved.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/02052012_Destination_System.png\" alt=\"Destination System\" /></div>" +
            "<p align=\"center\">Destination System</p><p>Next step was to select what I wanted to copy. I wanted to copy the whole machine, with one small exclusion: there was a disk drive that only contained some backup data and was irrelevant. So it was possible to save some time and disk space this way. I unchecked the drive I was not interested in. If you are an advanced user, you can apply further configuration changes.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/02052012_Options.png\" alt=\"Options\" /></div>" +
            "<p align=\"center\">Conversion task options</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/02052012_Source_volumes.png\" alt=\"Source volumes\" /></div>" +
            "<p align=\"center\">Uncheck the hard disk</p><p>Next step was to review the details and press <strong>Finish</strong>. Everything went well and a job was be added to the list, displaying some details and estimated time to completion. Now all I needed was some patience.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/02052012_Convert_Machine.png\" alt=\"Convert Machine\" /></div>" +
            "<p align=\"center\">Conversion task</p><p>When the conversion task was complete, I opened the newly created VM.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/02052012_Open_Virtual_Machine.png\" alt=\"Open Virtual Machine\" /></div>" +
            "<p align=\"center\">Open Virtual Machine</p><p>I got a warning message, which, I assumed, was related to the fact that VMWare tools were not yet installed. After I installed the tools later, I never saw the warning again.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/02052012_Cannot_connect.png\" alt=\"Cannot connect\" /></div>" +
            "<p align=\"center\">Warning message</p><p>Finally the VMWare tools improve graphic performance on the guest PC. So the last step was to install them.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/02052012_Install_VMWare_Tools.png\" alt=\"Install VMWare Tools\" /></div>" +
            "<p align=\"center\">Install VMWare tools</p><p>In my case, a message appeared and I did exactly as it advised - on the VM, logged in and ran <strong>E:\\setup.exe</strong> from the command prompt. The installation started and I followed the prompts until VMWare tools were installed.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/02052012_Click_on_the_virtual_screen.png\" alt=\"Click on the virtual screen\" /></div><p align=\"center\">VMWare message</p>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_02052012_k = "VM VMWare converter physical virtual";
        public const string content_02052012_d = "Converting a Physical PC to VM with VMWare Converter";

        //"QML ScrollBar"
        public const string content_04042012_b = "<p>I had to implement a <strong>ScrollBar</strong> component but the examples I could find did not do what I needed. I needed the <strong>ScrollBar</strong> with arrows on the sides, which would move the contents of the field a certain number of points to the required direction. After that, I added the sliding feature, that moved the contents of the field when the slider is dragged.</p>";
        public const string content_04042012_r = "<p>The <strong>ScrollBar</strong> consists of four rectangles. Two of them are arrows at the ends. One is the whole length of the scroll bar, and the last one is the slider that can be dragged along the body of the scroll bar. I used the <strong>Flickable</strong> component because I need to know the size of the field contents. Otherwise, I disabled the functionality of the flickable. When any of the arrows is clicked, first action is to identify if the slider can move in the required direction. This is calculated based on the height/width of the field (returned by <strong>flickable.height</strong> or <strong>width</strong>), height/width of the contents of the field (returned by <strong>flickable.contentHeight</strong> or <strong>contentWidth</strong>) and the current position of the contents within the flickable control (returned by <strong>flickable.contentX</strong> or <strong>contentY</strong>). If there is space to move, the <strong>flickable.contentX</strong> or <strong>contentY</strong> is changed, which moves the contents inside the field, and the slider position is adjusted.</p><p>The slide operates on the similar principle. When it is released, its position is read and then basing on the position and the length of the slider body the percentage of length the slider has traveled is calculated. From that percentage, the <strong>flickable.contentX</strong> or <strong>contentY</strong> is calculated so that the field contents move to reflect that percentage. It is not yet completely presice, but it does what is expected to do.</p><p>To use the <strong>ScrollBar</strong>, it needs to know several things: the id of the <strong>Flickable</strong> component associated with the field, the desired width of the scroll bar, the amount of pixels the contents need to move each click and the orientation - vertical or horisontal. In the example below, the <strong>Flickable</strong> component called <strong>view</strong> is associated with both scroll bars, the width of the scroll bar is 10 and the image moves 20 pixels each arrow click.</p><pre class=\"brush: js\">" +
        @"import QtQuick 1.1<br /><br />Rectangle {<br />    width: 360<br />    height: 360<br /><br />    Flickable{<br /><br />        id:view<br />        anchors.fill: parent<br />        contentWidth: picture.width<br />        contentHeight: picture.height<br />        interactive: false<br /><br />        Image {<br />            id: picture<br />            source: 'images/Desert.jpg'<br />            asynchronous: true<br />        }<br />    }<br /><br />    ScrollBar{<br />        id: verticalScroll<br />        flickable: view<br />        step: 20<br />        size: 10<br />        orientation: Qt.Vertical<br />    }<br /><br />    ScrollBar{<br />        id: horisontalScroll<br />        flickable: view<br />        step: 20<br />        size: 10<br />        orientation: Qt.Horizontal<br />    }<br />}" +
        "</pre><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/04042012_QML_ScrollBar.png\" alt=\"QML ScrollBar\" /></div><p align=\"center\">QML ScrollBar</p><p>The listing of the ScrollBar component is as follows:</p><pre class=\"brush: js\">" +
        @"import QtQuick 1.1<br /><br />Rectangle {<br /><br />    id: scrollBar<br />    property variant orientation<br />    property variant flickable<br />    property int step<br />    property int size<br />    height: orientation === Qt.Vertical ? flickable.height : size<br />    width: orientation === Qt.Vertical ? size : flickable.width<br /><br />    function canMove(first)<br />    {<br />        if(first)<br />            return orientation === Qt.Vertical ? flickable.contentY > 0 : flickable.contentX > 0;<br />        else<br />            return orientation === Qt.Vertical ? flickable.height + flickable.contentY < flickable.contentHeight :<br />                                                            flickable.width + flickable.contentX < flickable.contentWidth<br />    }<br /><br />    function arrowClicked(first)<br />    {<br />        if(canMove(first))<br />        {<br />            if(first)<br />            {<br />                if(orientation === Qt.Vertical)<br />                    flickable.contentY -= step;<br />                else<br />                    flickable.contentX -= step;<br />            }<br />            else<br />            {<br />                if(orientation === Qt.Vertical)<br />                    flickable.contentY += step;<br />                else<br />                    flickable.contentX += step;<br />            }<br />            positionSlider();<br />        }<br />    }<br /><br />    function moveContents()<br />    {<br />        if(orientation === Qt.Vertical)<br />            flickable.contentY = (slider.y - size)*(flickable.contentHeight - flickable.height)/(body.height - slider.height);<br />        else<br />            flickable.contentX = (flickable.contentWidth - flickable.width)*slider.x/flickable.width;<br />    }<br /><br />    function positionSlider()<br />    {<br />        var percentage = orientation === Qt.Vertical ? (flickable.contentY)/(flickable.contentHeight - flickable.height)<br />                                                     : (flickable.contentX + size)/(flickable.contentWidth - flickable.width);<br />        if(orientation === Qt.Vertical)<br />            slider.y = percentage*(body.height - slider.height) + size;<br />        else<br />            slider.x = percentage*(body.width - slider.width) + size;<br />    }<br /><br />    onHeightChanged: {<br />        if(canMove(true) || canMove(false))<br />            positionSlider();<br />    }<br /><br />    onWidthChanged: {<br />        if(canMove(true) || canMove(false))<br />            positionSlider();<br />    }<br /><br />    Component.onCompleted: {<br />        sliderArea.drag.axis = orientation === Qt.Vertical ? Drag.YAxis : Drag.XAxis;<br /><br />        if(orientation === Qt.Vertical)<br />        {<br />            scrollBar.anchors.right = flickable.right<br />            firstArrow.anchors.top = scrollBar.top;<br />            body.anchors.top = firstArrow.bottom;<br />            secondArrow.anchors.bottom = scrollBar.bottom;<br />            slider.y = size;<br />        }<br />        else<br />        {<br />            scrollBar.anchors.bottom = flickable.bottom;<br />            firstArrow.anchors.left = scrollBar.left;<br />            body.anchors.left = firstArrow.right;<br />            secondArrow.anchors.right = scrollBar.right;<br />            slider.x = size;<br />        }<br />    }<br /><br />     Rectangle{<br />        id: firstArrow<br />        width: size<br />        height: size<br /><br />        Image{<br />            id: imgFirstArrow<br />            anchors.fill: parent<br />            source: orientation === Qt.Vertical ? 'images/vertUpArrow.png' : 'images/horisLeftArrow.png'<br />        }<br /><br />        MouseArea{<br />            id: firstArrowArea<br />            anchors.fill: parent<br />            onClicked: {<br />                arrowClicked(true);<br />            }<br />        }<br />     }<br /><br />     Rectangle{<br />        id: body<br />        width: orientation === Qt.Vertical ? size : scrollBar.width - 2*size<br />        height: orientation === Qt.Vertical ? scrollBar.height - 2*size : size<br />        color: '#575B5E'<br />     }<br /><br />     Rectangle{<br />         id: slider<br />         width: orientation === Qt.Vertical ? size : 3*size;<br />         height: orientation === Qt.Vertical ? 3*size : size;<br /><br />         Image{<br />             id: imgSlider<br />             anchors.fill: parent<br />             source: orientation === Qt.Vertical ? 'images/SliderVert-Enabled.png' : 'images/SliderHoris-Enabled.png'<br />         }<br /><br />         MouseArea{<br />             id:sliderArea<br />             anchors.fill: parent<br />             drag.target: slider<br />             drag.minimumX: 10<br />             drag.minimumY: 10<br />             drag.maximumX: orientation === Qt.Vertical ? 0 : body.width - slider.width + size<br />             drag.maximumY: orientation === Qt.Vertical ? body.height - slider.height + size : 0<br /><br />             onReleased: {<br />                 moveContents();<br />             }<br />         }<br />     }<br /><br />     Rectangle{<br />        id: secondArrow<br />        width: size<br />        height: size<br /><br />        Image{<br />            id: imgSecondArrow<br />            anchors.fill: parent<br />            source: orientation === Qt.Vertical ? 'images/vertDownArrow.png' : 'images/horisRightArrow.png'<br />        }<br /><br />        MouseArea{<br />            id: secondArrowArea<br />            anchors.fill: parent<br />            hoverEnabled: true<br />            onClicked: {<br />                arrowClicked(false);<br />            }<br />        }<br />     }<br /> }" +
        "</pre><p><strong>References:</strong></p><a href=\"http://doc.qt.nokia.com/4.7-snapshot/demos-declarative-webbrowser-content-scrollbar-qml.html\">ScrollBar.qml Example File</a><br/><a href=\"http://qtsource.wordpress.com/2011/02/07/scrollable-and-scroll-indicators-with-qml/\">Scrollable and Scroll indicators with QML</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_04042012_k = "QML ScrollBar";
        public const string content_04042012_d = "Implementing a ScrollBar with QML";

        //"Return Multiple Values to QML from C++"
        public const string content_01042012_b = "<p>In some cases it is required to return several values from a C++ function to the QML code. In my case, I call the function that checks the syntax of the Qt script that is passed as a string. The <strong>QScriptSyntaxCheckResult</strong> class does that check for me. If an error is found inside the script, I want to get back the error message and the line and column where the error was detected. In this case a <strong>QVariantMap</strong> class is the elegant and effective solution. In my C++ code, I pack the values I want to return into an instance of a <strong>QVariantMap</strong> class:</p>";
        public const string content_01042012_r = "<pre class=\"brush: cpp\">" +
        @"QVariantMap QMLFile::checkScriptSyntax(QString input) const<br />{<br />    QScriptSyntaxCheckResult result = QScriptEngine::checkSyntax(input);<br /><br />    QVariantMap value;<br />    value.insert('errorMessage', result.errorMessage());<br />    value.insert('errorLineNumber', result.errorLineNumber());<br />    value.insert('errorColumnNumber', result.errorColumnNumber());<br />    return value;<br />}" +
        "</pre><p>In my QML code, I can now access the values in the following manner, after opening a file that contains a Qt script and passing its contents to the C++ function:</p><pre class=\"brush: js\">" +
        @"onClicked: {<br /> var fileContent = QMLFile.getFileContents();<br /> var result = QMLFile.checkScriptSyntax(fileContent);<br /><br /> if(result.errorMessage !== '')<br /> {<br />  console.log('Error on line ' + result.errorLineNumber + ', column ' + result.errorColumnNumber + ' : ' + result.errorMessage);<br /> }<br /> else<br /> {<br />  console.log('Script syntax is correct!');<br /> }<br />}" +
        "</pre><p>The result is further used in the <strong>ToolTipRectangle</strong> element, which is a <strong>Text</strong> element that appears when the mouse is hovered over the line which contains an error.</p><pre class=\"brush: js\">import QtQuick 1.1<br /><br />Rectangle {<br /><br />    property string toolTip : ''<br />    property bool showToolTip: false<br /><br />    Rectangle{<br />        id: toolTipRectangle<br />        anchors.horizontalCenter: parent.horizontalCenter<br />        anchors.top: parent.bottom<br />        width: parent.toolTip.length * toolTipText.font.pixelSize / 2<br />        height: toolTipText.lineCount * (toolTipText.font.pixelSize + 5)<br />        z:100<br /><br />        visible: parent.toolTip !== '' && parent.showToolTip<br />        color: '#ffffaa'<br />        border.color: '#0a0a0a'<br /><br />        Text{<br />            id:toolTipText<br />            width: parent.parent.toolTip.length * toolTipText.font.pixelSize / 2<br />            height: toolTipText.lineCount * (toolTipText.font.pixelSize + 5)<br />            text: parent.parent.toolTip<br />            color:'black'<br />            wrapMode: Text.WordWrap<br />        }<br />    }<br /><br />    MouseArea {<br />        id: toolTipArea<br />        anchors.fill: parent<br />        onEntered: {<br />            parent.showToolTip = true<br />        }<br />        onExited: {<br />            parent.showToolTip = false<br />        }<br />        hoverEnabled: true<br />    }<br />}" +
        "</pre><p>The end result looks like this:</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/01042012_Return_Multiple_Values.png\" alt=\"Return Multiple Values\" /></div>" +
        "<p align=\"center\">Syntax check result</p><p><strong>References:</strong></p><a href=\"http://comments.gmane.org/gmane.comp.lib.qt.qml/3031\">Q_INVOKABLE member function: Valid argument types</a><br/><a href=\"http://www.fioniasoftware.dk/blog/?p=142\">Implementing tool tips in QML</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_01042012_k = "QML C++ multiple return values";
        public const string content_01042012_d = "How to Return Multiple Values to QML from C++";

        //"Connecting QML to C++, More Practical Example"
        public const string content_29032012_b = "<p>As a first practical application of connecting C++ to QML I had to integrate a <strong>File Open/Close</strong> dialog to be accessed from QML, because QML does not have this kind of control natively. So here are the requirements to the simple task:</p>";
        public const string content_29032012_r = "<ul><li>Open button starts the <strong>Open File</strong> dialog, which allows the user to select a text file</li><li>When the user selects the file, the contents of this file are loaded into an editable field</li><li>The user can edit the file</li><li>Save button opens the <strong>Save File</strong> dialog, which allows the user to save the contents of the editable field into a new or existing text file</li></ul><p>The C++ header file will define only two functions, <strong>getFileContents</strong> will be called when the <strong>Open</strong> button is clicked, and <strong>saveFileContents</strong> will be called when the <strong>Save</strong> button is clicked. Here is the full header file:</p><pre class=\"brush: cpp\">" +
        @"//qmlfile.h<br />#ifndef QMLFILE_H<br />#define QMLFILE_H<br /><br />#include <QObject><br /><br />class QMLFile : public QObject<br />{<br />    Q_OBJECT<br /><br />public:<br />    explicit QMLFile(QObject *parent = 0);<br /><br />    Q_INVOKABLE QString getFileContents() const;<br /><br />    Q_INVOKABLE void saveFileContents(QString fileContents) const;<br />};<br /><br />#endif // QFILE_H" +
        "</pre><p>The C++ code file will implement these functions. <strong>getFileContents</strong> opens the <strong>Open File</strong> dialog and waits for the user to select a file. When the file is selected, the function tries to open it and, if successful, returns the string that has file contents. <strong>saveFileContents</strong> is the opposite: it takes a string as a parameter, opens a <strong>Save File</strong> dialog, waits for the user to select a file or enter a name of the file, and tries to save the string as file contents. Quick and dirty - a lot of things could go wrong and cause exceptions, but that's not the point at this stage.</p><pre class=\"brush: cpp\">" +
        @"//qmlfile.cpp<br />#include <QFileDialog><br />#include <QTextStream><br />#include <QDebug><br />#include 'qmlfile.h'<br /><br />QMLFile::QMLFile(QObject *parent): QObject(parent)<br />{<br /><br />}<br /><br />QString QMLFile::getFileContents() const<br />{<br />    QString fileName = QFileDialog::getOpenFileName(NULL, tr('Open File'), '/home', tr('Text Files (*.txt)'));<br />    qDebug() << 'fileName:' << fileName;<br />    QFile file(fileName);<br />    if(!file.open(QIODevice::ReadOnly | QIODevice::Text))<br />        return '';<br /><br />    QString content = file.readAll();<br />    file.close();<br />    return content;<br />}<br /><br />void QMLFile::saveFileContents(QString fileContents) const<br />{<br />    QString fileName = QFileDialog::getSaveFileName(NULL, tr('Save File'), '/home', tr('Text Files (*.txt)'));<br /><br />    QFile file(fileName);<br />    if(file.open(QIODevice::WriteOnly | QIODevice::Text))<br />    {<br />        qDebug() << 'created file:' << fileName;<br />        QTextStream stream(&file);<br />        stream << fileContents << endl;<br />        file.close();<br />        return;<br />    }<br />    else<br />    {<br />        qDebug() << 'could not create file:' << fileName;<br />        return;<br />    }<br />}" +
        "</pre><p>Now the only two things that are left is to update the main files: main.cpp and main.qml. In main.cpp I add the two lines similar to the example in the previous post.</p><pre class=\"brush: cpp\">//in main.cpp after viewer<br />QMLFile qmlFile;<br />viewer.rootContext()->setContextProperty('QMLFile', &qmlFile);" +
        "</pre><p>In the main.qml I will implement the <strong>onClicked</strong> events for <strong>Open</strong> and <strong>Save</strong> buttons. Each will only take a single line.</p><pre class=\"brush: cpp\">" +
        "//open file and load contents into TextEdit<br />txt.text = QMLFile.getFileContents();" + "</pre><pre class=\"brush: cpp\">" +
        @"//get contents of the TextEdit and save to a file<br />QMLFile.saveFileContents(txt.text);" +
        "</pre><p>Now I can build and run the application and open some file, and if everything goes smoothly you'll see the results similar to the screenshot below - contents of a text file loaded into the QML TextEdit component and displayed.</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/29032012_Practical_CPP.png\" alt=\"Practical CPP\" /></div><p align=\"center\">Text File Loaded into QML TextEdit</p><p>For reference, the full main.qml file.</p><pre class=\"brush: js\">" +
        @"//main.qml<br />import QtQuick 1.0<br /><br />Rectangle {<br />    width: 360; height: 360<br /><br />    Rectangle{<br />        id:buttons<br />        height: 50; width: parent.width; anchors.top: parent.top<br /><br />        Rectangle{<br />            id: btnOpen<br />            width: 50; height: parent.height; anchors.left: parent.left<br /><br />            Image{<br />                anchors.fill: parent; source: 'images/open.png'<br />            }<br /><br />            MouseArea{<br />                anchors.fill: parent<br />                onClicked: {<br />                    txt.text = QMLFile.getFileContents();<br />                }<br />            }<br />        }<br /><br />        Rectangle{<br />            id: btnSave<br />            width:50; height: parent.height; anchors.left: btnOpen.right<br /><br />            Image{<br />                anchors.fill: parent; source: 'images/save.png'<br />            }<br /><br />            MouseArea{<br />                anchors.fill: parent<br />                onClicked: {<br />                    QMLFile.saveFileContents(txt.text);<br />                }<br />            }<br />        }<br />    }<br /><br />    Rectangle{<br />        id:textHandle<br />        width: parent.width; height: parent.height - buttons.height; anchors.bottom: parent.bottom<br /><br />        TextEdit{<br />            id: txt; anchors.fill: parent<br />        }<br />    }<br />}" +
        "</pre><p><strong>References</strong></p><a href=\"http://apidocs.meego.com/1.0/qt4.7/qfiledialog.html\">QFileDialog Class Reference</a><br/><a href=\"http://stackoverflow.com/questions/3122152/opening-a-file-from-a-qt-string\">Opening a file from a Qt String</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_29032012_k = "QML C++ Software development";
        public const string content_29032012_d = "Connecting QML to C++, A Practical Example";

        //"Connecting QML to C++, First Attempt"
        public const string content_28032012_b = "<p>It took me some time to figure it out all the way, so I'm recording steps to create a minimal application that will have QML UI talking to the C++ code and receiving some data. I was using Qt Creator 2.2.1, and some syntax appear to have changed in further versions, which gave me some additional headaches while trying to figure out the examples. There are some \"gotchas\" on the way, but eventually it will look quite simple.</p>";
        public const string content_28032012_r = "<p>In Qt Creator, select <strong>File -> New File or Project</strong> and choose <strong>Qt Quick Project -> Qt Quick Application</strong>. Give it a name, i.e. SimpleCPP. Accept the defaults. Let's first add the C++ code. Right-click the project and select <strong>Add New -> C++ -> C++ Class</strong>. Give it a name too, i.e. \"test\". This will add two files to the project: test.h under <strong>Headers</strong> and test.cpp under <strong>Sources</strong>. Here is the full header file:</p><pre class=\"brush: cpp\">" +
        @"#ifndef TEST_H
  #define TEST_H
  #include &lt;QObject&gt;

  class test : public QObject
  {
    Q_OBJECT

    public:
      explicit test(QObject *parent = 0);
      
    Q_INVOKABLE QString getString() const;
  };
  #endif 
  // TEST_H" +
          "</pre><p>The class <strong>test</strong> inherits from <strong>QObject</strong> and exposes <strong>getString</strong> function, which is marked as <strong>Q_INVOKABLE</strong>. This registers the function with the meta-object system and, whatever that means, will allow the function to be called from QML.</p><p>Here is the test.cpp listing, and not much to say about it - it's only purpose is to return a string so we could actually verify that the C++ code gets executed.</p><pre class=\"brush: cpp\">" +
        @"#include ""test.h""
  
  test::test(QObject *parent): QObject(parent)
  {
  }

  QString test::getString() const
  {
    QString str = ""string from cpp code"";
    return str;
  }" +
          "</pre><p>Next step is to register the C++ class with the main.cpp which was automatically created by the project. Add a couple of includes on the top</p><pre class=\"brush: cpp\">" +
        @"#include &lt;QDeclarativeContext&gt;
  #include &lt;QtGui/QGraphicsObject&gt;
  #include ""test.h""" +
          "</pre><p>And these two lines after the definition of the <strong>QApplicationViewer</strong>:</p><pre class=\"brush: cpp\">" +
        @"test dummy;
  viewer.rootContext()->setContextProperty(""Dummy"", &dummy);" +
          "</pre><p>The instance of the test class will now be known to the QML file as \"Dummy\". See later.</p><p>Now to modify the main.qml so it will be able to receive something from C++. In the main.qml which was automatically generated, I give the Text element the <strong>objectName</strong> property of \"textObject\". This is how it will be referenced by C++.</p><pre class=\"brush: js\">" +
        @"Text {
        objectName: ""textObject""
        text: ""Hello World""
        anchors.centerIn: parent
       }" +
         "</pre><p>So, here's one way to modify the text on the screen: add the following two lines to main.cpp</p><pre class=\"brush: cpp\">" +
        @"QObject* testText = viewer.rootObject()->findChild&lt;QObject*&gt;(""textObject"");
if(testText) testText->setProperty(""text"", dummy.getString());" +
        "</pre><p>This code finds a child object in the QML file which has an <strong>objectName</strong> of \"textObject\" and, if found, sets its text property to the string returned by the test C++ class. Build and run the application and verify that the \"string from cpp code\" is shown in the middle of the screen.</p><p>Another way to access the C++ code is to call the function right from the QML file. I can modify the main.qml this way:</p><pre class=\"brush: js\">" +
        @"Rectangle {
   width: 360
   height: 360
   Text {
     id: textQML
     objectName: ""textObject""
     text: ""Hello World""
     anchors.centerIn: parent
   }
   
   MouseArea {
     anchors.fill: parent
     onClicked: {
       textQML.text = Dummy.getString();
       //Qt.quit();
    }
  }
}" +
          "</pre><p>Now, when the Rectangle is clicked, the QML file calls <strong>Dummy</strong>, which is how the test class is known to the QML file. See above. Remove or comment out the last two added lines from the main.cpp and run the application again. At first nothing happens, but when you click anywhere in the window, the text changes to \"string from cpp code\".</p><p>The full main.cpp now looks like that:</p><pre class=\"brush: cpp\">" +
          @"#include &lt;QtGui/QApplication&gt;<br />#include ""qmlapplicationviewer.h""<br />#include &lt;QDeclarativeContext&gt;<br />#include &lt;QtGui/QGraphicsObject&gt;<br />#include ""test.h""<br /><br />int main(int argc, char *argv[])<br />{<br />    QApplication app(argc, argv);<br /><br />    QmlApplicationViewer viewer;<br /><br />    test dummy;<br />    viewer.rootContext()->setContextProperty(""Dummy\"", &dummy);<br /><br />    viewer.setOrientation(QmlApplicationViewer::ScreenOrientationAuto);<br />    viewer.setMainQmlFile(QLatin1String(""qml/SimpleCPP/main.qml""));<br />    viewer.showExpanded();<br /><br /> /* comment these two lines if you don't want to display the string on start up */<br />    QObject* testText = viewer.rootObject()->findChild&lt;QObject*&gt;(""textObject"");<br />    if(testText) testText->setProperty(""text"", dummy.getString());<br /><br />    return app.exec();<br />}" +
          "</pre><strong><p>References</p></strong><a href=\"http://doc.qt.nokia.com/4.7-snapshot/qobject.html#Q_INVOKABLE\">QObject Class Reference</a><br/><a href=\"http://stackoverflow.com/questions/9500280/qt-access-c-function-from-qml\">qt, access c++ function from qml</a><br/><a href=\"http://stackoverflow.com/questions/5709820/communication-between-c-and-qml\">Communication between C++ and QML</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_28032012_k = "Software development QML C++";
        public const string content_28032012_d = "Connecting QML to C++";

        //"Improved QML Simple Splitter"
        public const string content_26032012_b = "<p>The splitter from the last post works. What happens, however, if I want to add content to it, such as text boxes and other things? Well, currently I'll have to mess with the code of the splitter component itself. Obviously, I will not be able to reuse it after that, I will have to copy and paste chunks of it next time I need another splitter. This is not something I can call a good solution. So the next iteration is to somehow make it possible to reuse the splitter without making changes to its QML file, and make it possible to add content \"from outside\" of the component. Here comes splitter 2.0. The code itself changed very little, but the two rectangles, which are left and right panels (or top and bottom in the case of a horizontal splitter) now are not part of the component itself. To add content to panels, two properties are defined in the VerticalSplitter component.</p>";
        public const string content_26032012_r = "<pre class=\"brush: js\">" +
            @"property QtObject firstRect;<br />property QtObject secondRect;<br />" +
            "</pre><p>These components will be assigned in the \"main.qml\" as follows:</p><pre class=\"brush: js\">" +
            @"//main.qml<br />import QtQuick 1.0<br /><br />Rectangle {<br />    width: 600<br />    height: 600<br /><br />    Rectangle{<br />        id: leftRect<br />        color: ""blue""<br />    }<br /><br />    VerticalSplitter{<br />        id: someSplitter<br />        firstRect : leftRect<br />        secondRect: rightRect<br />    }<br /><br />    Rectangle{<br />        id:rightRect<br />        color: ""red""<br />    }<br />}</pre>" +
            "<p>The splitter component is now simply inserted between the two panels. The panels and splitter are anchored to each other in the VerticalSplitter.qml which is almost the same as the one from the last post - probably slightly more simple. Great. I think my chunky, lousy QML is improving little by little.</p><pre class=\"brush: js\">" +
            @"//Reusable VerticalSplitter.qml<br />import QtQuick 1.0<br /><br />Rectangle{<br />    id: splitterRect<br /><br />    anchors.left: firstRect.right<br />    anchors.right: firstRect.right<br />    anchors.rightMargin: -10<br /><br />    width: 10<br />    height: parent.height<br />    clip: true<br /><br />    property QtObject firstRect;<br />    property QtObject secondRect;<br /><br />    property int maximizedRect : -1;<br /><br />    function moveSplitterTo(x)<br />    {<br />        if(x > 0 && x < parent.width - splitterRect.width)<br />        {<br />            firstRect.width = x;<br />            secondRect.width = parent.width - firstRect.width - splitterRect.width;<br />        }<br />    }<br /><br />    function maximizeRect(x)<br />    {<br />        firstRect.width = x===0 ? parent.width - splitterRect.width : 0<br />        secondRect.width = x===0 ? 0 : parent.width - splitterRect.width<br />    }<br /><br />    Component.onCompleted: {<br />        firstRect.height = height;<br />        firstRect.width = (firstRect.parent.width - width)/2;<br />        firstRect.anchors.left = firstRect.parent.left;<br /><br />        secondRect.anchors.left = splitterRect.right;<br />        secondRect.anchors.right = secondRect.parent.right;<br />        secondRect.height = height;<br />    }<br /><br />    onXChanged: {<br />        moveSplitterTo(splitterRect.x);<br />    }<br /><br />    BorderImage {<br />        id: splitterBorder<br />        anchors.fill: parent<br />        source: ""images/splitterBorder_vertical.png""<br />    }<br /><br />    Image{<br />        id: arrows<br />        anchors.horizontalCenter: parent.horizontalCenter<br />        anchors.verticalCenter: parent.verticalCenter<br />        source: ""images/splitterArrows_vertical.png""<br />    }<br /><br />    MouseArea {<br />        anchors.fill: parent<br />        drag.axis: Drag.XAxis<br />        drag.target: splitterRect<br /><br />        onClicked: {<br />            maximizedRect = maximizedRect == 1 ? 0 : 1;<br />            maximizeRect(maximizedRect);<br />        }<br />    }<br />}</pre>" +
            "<br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_26032012_k = "QML Splitter software development";
        public const string content_26032012_d = "Continuing the work on QML Splitter from earlier posts";

        //"The Most Basic Splitter Component Possible"
        public const string content_25032012_b = "<p>The most basic splitter involves the top part of the area, the bottom part and the splitter in between. The splitter is a narrow rectangle with a <strong>MouseArea</strong>. <strong>drag.target:splitterRect</strong> makes the <strong>MouseArea</strong> to listen to drag events from splitter. <strong>drag.axis</strong> specifies along which axis the splitter can be dragged. When the splitter is dragged with the mouse along the Y axis, its Y position changes, triggering the <strong>onYChanged event</strong>, and the <strong>moveSplitterTo</strong> just recalculates the widths of top and bottom rectangles according to the current Y position of the splitter.</p><p>Additionally, if the splitter is not dragged, but clicked, the splitter collapses one of the frames. On start up, the first frame to be collapsed is chosen, and then, on each click, the other frame is collapsed, and the one that was collapsed is expanded.</p>";
        public const string content_25032012_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/25032012_QML_Splitter_1.png\" alt=\"QML Splitter 1\" /></div>" + "<p align=\"center\">Basic Splitter</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/25032012_QML_Splitter_2.png\" alt=\"QML Splitter 2\" /></div>" +
            "<p align=\"center\">One of the Frames is Collapsed</p><pre class=\"brush: js\">" +
            @"//Splitter component<br />import QtQuick 1.0<br /><br />Item{<br />    id: root<br />    anchors.fill: parent<br />    width: parent.width<br />    height: parent.height<br />    clip: true<br /><br />    property int splitterHeight: 10<br />    property int maximizedRect : -1;<br /><br />    function moveSplitterTo(y)<br />    {<br />        if(y > 0 && y < parent.height - splitterHeight)<br />        {<br />            topRect.height = y;<br />            bottomRect.height = parent.height - topRect.height - splitterHeight;<br />        }<br />    }<br /><br />    function maximizeRect(x)<br />    {<br />        topRect.height = x===0 ? parent.height - splitterHeight : 0<br />        bottomRect.height = x===0 ? 0 : parent.height - splitterHeight<br />    }<br /><br />    Rectangle{<br />        id: topRect<br />        width: parent.width<br />        height:  (parent.height-splitterHeight)/2<br />        anchors.top: parent.top<br />        color:  ""blue""<br />    }<br /><br />    Rectangle {<br />        id: splitterRect<br />        width: parent.width<br />        height: splitterHeight<br />        color: ""black""<br /><br />        anchors.top: topRect.bottom<br />        anchors.bottom: bottomRect.top<br /><br />        property int tempY : splitterRect.y<br /><br />        onYChanged: {<br />            moveSplitterTo(splitterRect.y);<br />        }<br /><br />        BorderImage {<br />            id: splitterBorder<br />            anchors.fill: parent<br />            source: ""images/splitterBorder.png""<br />        }<br /><br />        Image{<br />            id: arrows<br />            anchors.horizontalCenter: parent.horizontalCenter<br />            anchors.verticalCenter: parent.verticalCenter<br />            source: ""images/splitterArrows.png""<br />        }<br /><br />        MouseArea {<br />            anchors.fill: parent<br />            drag.axis: Drag.YAxis<br />            drag.target: splitterRect<br /><br />            onClicked: {<br />                maximizedRect = maximizedRect == 1 ? 0 : 1;<br />                maximizeRect(maximizedRect);<br />            }<br />        }<br />    }<br /><br />    Rectangle{<br />        id:bottomRect<br />        width: parent.width<br />        height: (parent.height-splitterHeight)/2<br />        anchors.bottom: parent.bottom<br />        color: ""red""<br />    }" + "<br />}</pre><p>References</p><a href=\"http://doc.qt.nokia.com/4.7-snapshot/qml-mousearea.html\">QML MouseArea Element</a><br/><a href=\"http://qt.gitorious.org/qt-components/desktop/blobs/3c0208a3e146ac1f33997bbf3e100a88e7dbf852/components/custom/SplitterRow.qml\">SplitterRow.qml</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_25032012_k = "Software development QML Splitter";
        public const string content_25032012_d = "The Most Basic QML Splitter Component Possible";

        //"QML Check Box"
        public const string content_20032012_b = "<p>The check box can be in seven different states: enabled - checked and unchecked, disabled - checked and unchecked, pressed - in checked and unchecked states, and finally \"mouseover\" state when the cursor is over the checkbox, but not pressed.</p><p>The <strong>mouseover</strong> state is triggered by the cursor entering and exiting the check box area. The <strong>disabled</strong> and <strong>checked</strong> stated can also be set from \"outside\" by setting the <strong>isDisabled</strong> and <strong>isChecked</strong> variables. The <strong>onPressed</strong> code remembers the state the check box was in and changes the icon to pressed version, and <strong>onReleased</strong> reverts the icon change. The <strong>onClicked</strong> toggles the state from on to off and back.</p>";
        public const string content_20032012_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/20032012_QML_Check_Box_1.png\" alt=\"QML Check Box 1\" /></div>" +
        "<p align=\"center\">Checked state</p>" +
        "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/20032012_QML_Check_Box_2.png\" alt=\"QML Check Box 2\" /></div>" +
        "<p align=\"center\">Checked and pressed state</p>" +
        "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/20032012_QML_Check_Box_3.png\" alt=\"QML Check Box 3\" /></div>" +
        "<p align=\"center\">Disabled state</p><pre class=\"brush: js\">" + @"import QtQuick 1.0<br /><br />Rectangle {<br /><br />id: checkBoxRect<br />width: 100<br />height:  30<br /><br />property string chkUnchecked: ""images/checkbox_enabled.png""<br />property string chkChecked : ""images/checkbox_checked.png""<br /><br />property string chkUncheckedPressed : ""images/checkbox_pressed.png""<br />property string chkCheckedPressed : ""images/checkbox_checked_pressed.png""<br /><br />property string chkDisabled: ""images/checkbox_disabled.png""<br />property string chkDisabledChecked: ""images/checkbox-checked_disabled.png""<br />property string chkMouseOver: ""images/checkbox_enabled_mouseover.png""<br /><br />property bool pressed: false<br />property string src: chkUnchecked<br />property bool isDisabled : false<br />property bool isChecked: false<br /><br />property string tempState : """"<br />property string tempStateHover : """"<br />property string status : """"<br /><br />Image {<br />    id: checkBoxImg<br />    width: 30<br />    height: parent.height<br />    source: src<br />    fillMode: Image.PreserveAspectFit;<br /><br />    MouseArea {<br />        anchors.fill: parent<br />        hoverEnabled: true<br />        onClicked: {<br />            if(!isDisabled)<br />            {<br />                if(checkBoxRect.state == ""mouseover"")<br />                    checkBoxRect.state = tempStateHover == ""on"" ? ""off"" : ""on"";<br />                else<br />                    checkBoxRect.state = checkBoxRect.state == ""on"" ? ""off"" : ""on"";<br />                tempStateHover = checkBoxRect.state;<br />            }<br />        }<br />        onPressed: {<br />            if(!isDisabled)<br />            {<br />                tempState = checkBoxRect.state;<br />                checkBoxRect.state = ""pressed"";<br />            }<br />        }<br />        onReleased: {<br />            if(!isDisabled)<br />                checkBoxRect.state = tempState;<br />        }<br />        onEntered: {<br />            if(!isDisabled)<br />            {<br />                tempStateHover = checkBoxRect.state;<br />                checkBoxRect.state = ""mouseover"";<br />            }<br />        }<br />        onExited: {<br />            if(!isDisabled)<br />                checkBoxRect.state = tempStateHover;<br />        }<br />    }<br />}<br /><br />Component.onCompleted: {<br />    if(isDisabled)<br />        src = isChecked ? chkDisabled : chkCheckedDisabled<br />    else<br />        checkBoxRect.state = ""off"";<br />}<br /><br />Text{<br />    id: checkboxText<br />    height: parent.height<br />    width:  parent.width - checkBoxImg.width<br />    text: ""click here""<br />    anchors.left: checkBoxImg.right<br />    verticalAlignment: Text.AlignVCenter<br />}<br /><br />states: [<br />    State {<br />        name: ""on""<br />        PropertyChanges { target: checkBoxImg; source: chkChecked }<br />        PropertyChanges { target: checkBoxRect; pressed: true }<br />    },<br />    State {<br />        name: ""off""<br />        PropertyChanges { target: checkBoxImg; source: chkUnchecked }<br />        PropertyChanges { target: checkBoxRect; pressed: false }<br />    },<br />    State {<br />        name: ""pressed""<br />        PropertyChanges {target: checkBoxImg; source: (tempState == ""on"" || tempStateHover == ""on"") ? chkCheckedPressed : chkUncheckedPressed}<br />    },<br />    State {<br />        name: ""mouseover""<br />        PropertyChanges {target: checkBoxImg; source: chkMouseOver}<br />    }<br />]<br />}" +
        "</pre><p>Reference:</p><a href=\"http://stackoverflow.com/questions/6152041/qt-qml-anchors-probleme\">QT QML anchors probleme</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_20032012_k = "Software development QML Check box";
        public const string content_20032012_d = "Implementing QML Check Box";

        //"QML Drop Down Menu"
        public const string content_19032012_b = "<p>A drop down menu example. There are two states of the drop down list - <strong>dropDown</strong> and, well, not dropDown. When the top rectangle (<strong>chosenItem</strong>) is clicked, the state is switched from dropDown to none and back, and the height of the <strong>dropdownList</strong> is adjusted accordingly, showing or hiding the list with the selection options. When the list itself is clicked, there is an additional action - the text of the <strong>chosenItemText</strong> area is updated to reflect the selection.</p>";
        public const string content_19032012_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/19032012_Expanded.png\" alt=\"Expanded\" /></div>" +
            "<p align=\"center\">List is expanded</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/19032012_Collapsed.png\" alt=\"Collapsed\" /></div>" +
            "<p align=\"center\">A selection was made, text is updated and list is hidden again</p><pre class=\"brush: js\">" +
            @"import QtQuick 1.0<br /><br />Rectangle {<br />    width:400;<br />    height: 400;<br /><br />    property int dropDownHeight:40<br /><br />    Rectangle {<br />            id:dropdown<br />            property variant items: [""Test Item 1"", ""Test Item 2"", ""Test Item 3""]<br />            width: 100;<br />            height: dropDownHeight;<br />            z: 100;<br /><br />            Rectangle {<br />                id:chosenItem<br />                width:parent.width;<br />                height:dropdown.height;<br />                color: ""lightsteelblue""<br />                Text {<br />                    anchors.top: parent.top;<br />                    anchors.left: parent.left;<br />                    anchors.margins: dropDownHeight/5;<br />                    id:chosenItemText<br />                    text:dropdown.items[0];<br />                }<br /><br />                MouseArea {<br />                    anchors.fill: parent;<br />                    onClicked: {<br />                        dropdown.state = dropdown.state===""dropDown""?"""":""dropDown""<br />                    }<br />                }<br />            }<br /><br />            Rectangle {<br />                id:dropdownList<br />                width:dropdown.width;<br />                height:0;<br />                clip:true;<br />                anchors.top: chosenItem.bottom;<br />                anchors.margins: 2;<br />                color: ""lightgray""<br /><br />                ListView {<br />                    id:listView<br />                    height:dropDownHeight*dropdown.items.length<br />                    model: dropdown.items<br />                    currentIndex: 0<br />                    delegate: Item{<br /><br />                        width:dropdown.width;<br />                        height: dropdown.height;<br /><br />                        Text {<br />                            text: modelData<br />                            anchors.top: parent.top;<br />                            anchors.left: parent.left;<br />                            anchors.margins: 5;<br />                        }<br /><br />                        MouseArea {<br />                            anchors.fill: parent;<br />                            onClicked: {<br />                                dropdown.state = """";<br />                                chosenItemText.text = modelData;<br />                                listView.currentIndex = index;<br />                            }<br />                        }<br />                    }<br />                }<br />            }<br /><br />            states: State {<br />                name: ""dropDown"";<br />                PropertyChanges { target: dropdownList; height:dropDownHeight*dropdown.items.length }<br />            }<br />        }<br />    }" +
        "</pre><p>References:</p><a href=\"http://qt-project.org/forums/viewthread/5276\">QML drop Down Menu or Menu bar</a><br/><a href=\"http://stackoverflow.com/questions/9634897/qt-qml-dropdown-list-like-in-html\">Qt QML dropdown list like in HTML</a><br/><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_19032012_k = "Software develoment QML Drop Down Menu";
        public const string content_19032012_d = "Implementing QML Drop Down Menu";

        //"QML Improved Tree View"
        public const string content_18032012_b = "<p>I added some styling to the tree view exercise, so it looks better overall. A little change to the <strong>ListView</strong> layout - there is now the separation line between the items, which is just a rectangle with the height of 2. Additionally, I do a couple other things when the selected item changes, by hooking to the <strong>onFocusChanged</strong> event and checking in the delegate if the item is selected: I toggle the transparency first, so that the highlight color is visible, but also when the selection is lost, the item is returned to its background color. Also, I invert the color of the item text (to white when the item is selected).</p>";
        public const string content_18032012_r = "<pre class=\"brush: js\">" +
        @"// ListView item layout<br />Rectangle{<br /> id: listItemRect<br /> anchors.fill: parent<br /> color: '#E2E3E4'<br /><br /> Rectangle{ //dividing line<br />  height:  2<br />  width: parent.width<br /><br />  anchors.top: parent.top<br />  anchors.left: parent.left<br />  anchors.right: parent.right<br /><br />  color: '#AFADB3'<br /> }<br /><br /> Rectangle{<br /><br />  height: parent.height - 2<br />  width:  parent.width<br />  anchors.left: parent.left<br />  anchors.right: parent.right<br />  anchors.bottom: parent.bottom<br />  color: 'transparent'<br />  ...<br /> }<br />}" +
        "</pre><pre class=\"brush: js\">" +
        @"//set text color to white if the item is selected.    <br />onFocusChanged: {<br /> if(ListView.view.currentIndex == index)<br /> {<br />  listItemRect.color = 'transparent';<br />  textDelegate.color = 'white';<br /> }<br /> else<br /> {<br />  listItemRect.color = '#E2E3E4';<br />  textDelegate.color = 'black';<br /> }<br />}" +
        "</pre><p>Another addition is the navigation bar on the top, so the user knows where in the document structure he is currently positioned. The arrows effect is achieved by simply adding some \"help\" images that are toggled and their visibility is changed as required by calling the <strong>setVisibility</strong> function from outside. Navigation bar itself responds to clicks on the items, bringing the user back to the position in the document structure according to the element being clicked.</p><p>Here's how the improved tree view looks like:</p>" +
        "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/18032012_ListView_Level_One.png\" alt=\"ListView Level One\" /></div>" +
        "<p align=\"center\">Just loaded</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/18032012_ListView_Level_Two.png\" alt=\"ListView Level Two\" /></div>" +
        "<p align=\"center\">Last level in the tree</p><p>Navigation bar modifications in main.qml and navigation bar code:</p><pre class=\"brush: js\">" +
        @"//added a navigation bar in NavigationBar.qml<br />Rectangle {<br />    id:main<br /> <br />//change visibility of the navigation bar elements<br />function setVisibility()<br />{<br /> navigationRect.hideRect(2, false)<br /><br /> if(level == 0)<br /> {<br />  navigationRect.hideRect(1, false)<br />  backButton.visible = false;<br /> }<br /> else<br /> {<br />  navigationRect.hideRect(1, true)<br /><br />  if(level == 1)<br />  {<br />   navigationRect.setText(0, level0Name);<br />  }<br />  if(level == 2)<br />  {<br />   navigationRect.hideRect(2, true)<br />  }<br /> }<br />}   <br />  ...<br /> <br /> NavigationBar{<br />        id:navigationRect<br />    }<br />}" + "</pre><pre class=\"brush: js\">" + "//Navigation bar code<br /><br />import QtQuick 1.0<br /><br />Rectangle{<br />    id: navigationRect<br />    height:  50<br />    width: parent.width<br />    anchors.top: parent.top<br />    anchors.left: parent.left<br />    anchors.right: parent.right<br />    anchors.topMargin: marginValue<br />    anchors.leftMargin: marginValue<br />    anchors.rightMargin: marginValue<br /><br />    function setText(rect, text)<br />    {<br />        var texts = [level0Text, level1Text, level2Text];<br />        texts[rect].text = text;<br />    }<br /><br />    function hideRect(rect, hide)<br />    {<br />        var rects = [level0Rect, level1Rect, level2Rect];<br />        var helps = [level0Help, level1Help, level2Help];<br />        rects[rect].visible = hide;<br />        helps[rect].visible = hide;<br /><br />        if(rect > 0)<br />        {<br />            hide? helps[rect-1].children[0].source = 'icons/selected_black.png' : helps[rect-1].children[0].source = 'icons/selected_black_half.png'<br />        }<br />        if(rect == 2)<br />        {<br />            hide? helps[rect].children[0].source = 'icons/selected_black_half.png' : helps[rect].children[0].source = ''<br />        }<br />    }<br /><br />    Component.onCompleted: {<br />        level1Rect.visible = false;<br />        level2Rect.visible = false;<br />        level2Rect.visible = false;<br />        level2Help.visible = false;<br />    }<br /><br />    Rectangle{<br />        id: level0Rect<br />        width:  parent.width*4/15<br />        height: parent.height<br />        anchors.left: parent.left<br />        anchors.top: parent.top<br />        color: 'black'<br /><br />        Text{<br />            id: level0Text<br />            anchors.centerIn: parent<br />            color: 'white'<br />            text: level0Label<br />            font.pixelSize: 10<br />        }<br /><br />        MouseArea{<br />            id: level0Mouse<br />            anchors.fill: parent<br /><br />            onClicked: {<br />                level = 0;<br />                setQueries();<br />            }<br />        }<br />    }<br /><br />    Rectangle{<br />        id: level0Help<br />        width:  parent.width/15<br />        height: parent.height<br />        anchors.left: level0Rect.right<br />        anchors.top: parent.top<br /><br />        Image{<br />            anchors.fill: parent<br />        }<br />    }<br /><br />    Rectangle{<br />        id: level1Rect<br />        width:  parent.width*4/15<br />        height: parent.height<br />        anchors.left: level0Help.right<br />        anchors.top: parent.top<br />        color: 'black'<br /><br />        Text{<br />            id: level1Text<br />            color: 'white'<br />            anchors.centerIn: parent<br />            text: level1Label<br />            font.pixelSize: 10<br />        }<br /><br />        MouseArea{<br />            id: level1Mouse<br />            anchors.fill: parent<br /><br />            onClicked: {<br />                level = 1;<br />                setQueries();<br />            }<br />        }<br />    }<br /><br />    Rectangle{<br />        id: level1Help<br />        width:  parent.width/15<br />        height: parent.height<br />        anchors.left: level1Rect.right<br />        anchors.top: parent.top<br /><br />        Image{<br />            anchors.fill: parent<br />        }<br />    }<br /><br />    Rectangle{<br />        id: level2Rect<br />        width:  parent.width*4/15<br />        height: parent.height<br />        anchors.left: level1Help.right<br />        anchors.top: parent.top<br />        color: 'black'<br /><br />        Text{<br />            id: level2Text<br />            color: 'white'<br />            anchors.centerIn: parent<br />            text: level2Label<br />            font.pixelSize: 10<br />        }<br /><br />        MouseArea{<br />            id: level2Mouse<br />            anchors.fill: parent<br />        }<br />    }<br /><br />    Rectangle{<br />        id: level2Help<br />        width:  parent.width/15<br />        height: parent.height<br />        anchors.left: level2Rect.right<br />        anchors.top: parent.top<br /><br />        Image{<br />            anchors.fill: parent<br />        }<br />    }<br />}" + "</pre><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_18032012_k = "Software development QML Treeview";
        public const string content_18032012_d = "Implementing Improved QML Tree View";

        //"QML TreeView Exercise"
        public const string content_14032012_b = "<p>Here's a bit of exersice I had to go through to implement a treeview-like structure in QML. The control has to process the XML file, read data and display it in the form that will allow the user to navigate the tree structure. As an example, the XML file is shown below.</p>";
        public const string content_14032012_r = "<pre class=\"brush: xml\">" +
            @"&lt;?xml version=""1.0"" encoding=""utf-8""?&gt;<br />&lt;configuration&gt;<br />&lt;level1item name=""level1item 1""&gt;<br /> &lt;level2item name=""module 1.1""&gt;<br />  &lt;level3item name=""1.1.1"" attr1=""0"" attr2=""1""&gt;<br />  &lt;/level3item&gt;<br />  &lt;level3item name=""1.1.2"" attr1=""0"" attr2=""1""&gt;<br />  &lt;/level3item&gt;<br /> &lt;/level2item&gt;<br /> &lt;level2item name=""module 1.2""&gt;<br /> &lt;/level2item&gt;<br /> &lt;level2item name=""module 1.3""&gt;<br /> &lt;/level2item&gt;<br /> &lt;level2item name=""module 1.4""&gt;<br /> &lt;/level2item&gt;<br /> &lt;level2item name=""module 1.5""&gt;<br /> &lt;/level2item&gt;<br />&lt;/level1item&gt;<br />&lt;level1item name=""level1item 2""&gt;<br /> &lt;level2item name=""module 2.1""&gt;<br /> &lt;/level2item&gt;<br /> &lt;level2item name=""module 2.3""&gt;<br /> &lt;/level2item&gt;<br /> &lt;level2item name=""module 2.4""&gt;<br /> &lt;/level2item&gt;<br /> &lt;level2item name=""module 2.5""&gt;<br /> &lt;/level2item&gt;<br />&lt;/level1item&gt;<br />&lt;level1item name=""level1item 3""&gt;<br />&lt;/level1item&gt;<br />&lt;/configuration&gt;" +
            "</pre><p>The QML only has very basic means to parse XML files. In fact, the only suitable way I have found so far is to use <strong>XmlListModel</strong>. This means that I have to assume that the schema of the XML document is known beforehand. A more generic way would be to use C++ to parse the XML, but that was out of scope for me. I based my control on some existing solutions that are referenced at the end. Essentially, when the control is first loaded, a first level of the XML tree is read and the data is loaded into the <strong>ListView XmlListModel</strong>, which is defined in the main QML file.</p><p>The next idea is to check if each element of the <strong>ListView</strong> has any children at all. For that purpose, a separate <strong>XmlListModel</strong> is defined in the <strong>ListView</strong> delegate. Whenever the <strong>ListView</strong> item is created, the model is constructed and the query uses the delegate's data to retrieve the children of the item. I want a certain image button (with an arrow indicating that the user can navigate to the children of the item) to become visible only in case there are more than 0 children. First, the query is assigned to the model, and the <strong>onQueryChanged</strong> event fires. The button is made invisible on this event. Next, the query returns the results and the <strong>onCountChanged</strong> fires. If the result returned more than 0 items, the button is made visible.</p><pre class=\"brush: js\">" +
            @"<br />XmlListModel{<br /> id: delegateXmlModel<br /> source: ""tree1.xml""<br /> query: buildQuery(false, name);<br /><br /> XmlRole{name: ""name""; query: buildRoleQuery();}<br /><br /> onQueryChanged: {<br />  button.visible = false;<br /> }<br /><br /> onCountChanged: {<br />  if(count > 0)<br />   button.visible = true;<br /> }<br />}" + "</pre><p>The \"back\" button is displayed at the bottom of the ListView if the user has moved past the first level of the tree and has an option to move back. The functionality revolves around using the global variable <strong>level</strong>, which indicates which level of the XML tree is current at the moment. Most of the JavaScript deals with constructing the correct queries for the <strong>XmlListModels</strong>.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/14032012_ListView_Level_One.png\" alt=\"ListView Level One\" /></div>" +
            "<p align=\"center\">First level of the tree</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/14032012_ListView_Level_Two.png\" alt=\"ListView Level Two\" /></div>" +
            "<p align=\"center\">Last level of the tree</p><p>Full listing:</p><pre class=\"brush: js\">" + @"// XMLTree<br /><br />import QtQuick 1.0<br /><br />Rectangle {<br />    id:main<br />    width: 360<br />    height: 480<br /><br />    property int level: 0 //0: level1item; 1: level2item; 2: level3item<br />    property string topElement : ""configuration""<br />    property string level0: ""level1item""<br />    property string level1: ""level2item""<br />    property string level2: ""level3item""<br />    property string level0Name: """"<br />    property string level1Name: """"<br />    property string level2Name: """"<br /><br />    function buildRoleQuery()<br />    {<br />        return ""@name/string()"";<br />    }<br /><br />    function buildQuery(isMainTree, name)<br />    {<br />        var level0path = ""/"" + topElement + ""/"" + level0;<br />        var level1path = level0path + ""[@name=\""""<br />            + level0Name + ""\""]/"" + level1;<br />        var level2path = level1path + ""[@name=\""""<br />            + level1Name + ""\""]/"" + level2;<br /><br />        var level0query = level0path + ""[@name=\""""<br />            + name + ""\""]/"" + level1;<br />        var level1query = level0path + ""[@name=\""""<br />            + level0Name + ""\""]/"" + level1 + ""[@name=\""""<br />            + name + ""\""]/"" + level2;<br /><br />        if(level == 0)<br />        {<br />            if(isMainTree)<br />                return level0path;<br />            else<br />                return level0query;<br />        }<br />        if(level == 1)<br />        {<br />            if(isMainTree)<br />                return level1path;<br />            else<br />                return level1query;<br />        }<br />        if(level == 2)<br />        {<br />            if(isMainTree)<br />                return level2path;<br />            else<br />                return level0query; //unused<br />        }<br />        return ""/"";<br />    }<br /><br />    function setQueries()<br />    {<br />        listModel.query = buildQuery(true, """");<br />        listModel.roles[0].query = buildRoleQuery();<br />        listModel.reload();<br />    }<br /><br />    Component.onCompleted: {<br />        backButton.visible = false;<br />    }<br /><br />    ListView {<br />        id: listView<br />        height: parent.height-50<br />        width: parent.width<br />        anchors.top: parent.top<br />        model:  listModel<br /><br />        delegate: ListViewDelegate{}<br />        focus: true<br />    }<br /><br />    Rectangle{<br />        id: backRect<br />        height: 50<br />        width: parent.width<br />        anchors.bottom: parent.bottom<br /><br />        Rectangle{<br />            id:backButton<br />            anchors.right: parent.right<br />            width: 40<br />            height:  40<br /><br />            Image{<br />                id:backIcon<br />                anchors.fill: parent<br />                fillMode: Image.PreserveAspectFit<br />                source: ""icons/arrowBack.png""<br />            }<br /><br />            MouseArea{<br />                id: backMouseArea<br />                anchors.fill: parent<br /><br />                onClicked: {<br />                    level--;<br />                    if(level == 0)<br />                        backButton.visible = false;<br />                    setQueries();<br />                }<br />            }<br />        }<br />    }<br /><br />    XmlListModel{<br />        id: listModel<br />        source: ""tree1.xml""<br />        query: ""/"" + topElement + ""/"" + level0<br /><br />        XmlRole {name: ""name""; query: ""@name/string()""}<br />        XmlRole {name: ""type""; query: ""@type/string()""}<br />        XmlRole {name: ""attr1""; query: ""@attr1/string()""}<br />        XmlRole {name: ""attr2""; query: ""@attr2/string()""}<br />    }<br />}" + "</pre><pre class=\"brush: js\">" +
            @"//ListView delegate<br /><br />import QtQuick 1.0<br /><br />Rectangle{<br />    id: delegate<br />    width: parent.width<br />    height:  textDelegate.height<br /><br />    property string fullText : ""<br /><br />    function addComma()<br />    {<br />        if(fullText)<br />            fullText = fullText + "", "";<br />    }<br /><br />    function getText()<br />    {<br />        fullText = """";<br />        if(level == 0)<br />        {<br />            if(type)<br />                fullText = """"<br/>"""" + ""type="" + type;<br />            if(attr1)<br />            {<br />                addComma();<br />                fullText = fullText + ""<br/>"" + ""attr1="" + attr1;<br />            }<br />            return name + fullText;<br />        }<br />        else if(level == 1)<br />            return name;<br />        else if(level == 2)<br />        {<br />            if(attr1)<br />                fullText = ""<br/>"" + ""attr1: "" + attr1;<br />            if(attr2)<br />            {<br />                addComma();<br />                fullText = fullText + ""attr2: "" + attr2;<br />            }<br />            return name + fullText;<br />        }<br />        else<br />            return name;<br />        return """";<br />    }<br /><br />    Component.onCompleted: {<br />        textDelegate.text = getText();<br />    }<br /><br />    XmlListModel{<br />        id: delegateXmlModel<br />        source: ""tree1.xml""<br />        query: buildQuery(false, name);<br /><br />        XmlRole{name: ""name""; query: buildRoleQuery();}<br /><br />        onQueryChanged: {<br />            button.visible = false;<br />        }<br /><br />        onCountChanged: {<br />            if(count > 0)<br />                button.visible = true;<br />        }<br />    }<br /><br />    Rectangle{<br />        id: contentDelegate<br />        anchors.left: parent.left<br />        anchors.right: nextButton.left<br />        height: textDelegate.height<br />        border.color: ""red""<br />        border.width: 1<br />        z:1<br /><br />        Image{<br />            id: elementIcon<br />            height: textDelegate.height<br />            fillMode: Image.PreserveAspectFit<br />        }<br /><br />        Text{<br />            id: textDelegate<br />            anchors.left:  elementIcon.right<br />            anchors.leftMargin: 10<br />            anchors.right: parent.right<br />            height: 70<br />            text: """"<br />            font.pixelSize: 15<br />            horizontalAlignment: Text.AlignHLeft<br />            wrapMode: Text.WordWrap<br />        }<br />    }<br /><br />    Rectangle{<br />        id: nextButton<br />        anchors.right: parent.right<br />        anchors.rightMargin: 4<br />        width: 40<br />        height: contentDelegate.height<br /><br />        Rectangle{<br />            id: button<br />            anchors.centerIn: parent<br />            width: 40<br />            height: 40<br /><br />            radius: 5<br />            border{ color: ""gray""; width: 3}<br />            visible: (level < 2) //hide 'next' button when lowest level reached<br /><br />            Image{<br />                id: nextIcon<br />                anchors.fill: parent<br />                fillMode: Image.PreserveAspectFit<br />                source: ""icons/arrow.png""<br />            }<br /><br />            MouseArea{<br />                id: nextMouseArea<br />                anchors.fill: parent<br /><br />                onClicked: {<br />                    if(level == 0)<br />                        level0Name = name;<br />                    else if(level == 1)<br />                        level1Name = name;<br />                    level++;<br />                    if(level > 0)<br />                        backButton.visible = true;<br />                    setQueries();<br />                    listView.model = listModel;<br />                }<br />            }<br />        }<br />    }<br />}" + "</pre><p>References</p><a href=\"http://ruedigergad.com/2011/08/14/qml-treeview\">QML Treeview</a><br/><a href=\"http://ruedigergad.com/2011/08/28/new-version-of-the-qml-treeview\">New Version of the QML TreeView</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_14032012_k = "Software development QML Treeview";
        public const string content_14032012_d = "Implementing QML Tree View";

        //"A Disabled Tab and Adding Tabs on the Fly"
        public const string content_08032012_b = "<p>A custom tab control received a slight update: An <strong>isDisabled</strong> property was added to the <strong>CustomTab</strong>. The disabled tab does absolutely nothing - does not respond to mouse clicks or mouse enter and exit events. Its name is displayed in gray. So a couple changes were done to the <strong>CustomTab</strong> which are summarized below.</p>";
        public const string content_08032012_r = "<pre class=\"brush: js\">" + @"import QtQuick 1.0<br /><br />Rectangle {<br /> ...<br />    property bool isDisabled;<br />    property int tabIndex;<br /><br />    function applyState()<br />    {<br />  ...<br />        if(isDisabled)<br />        {<br />            textOnTab.color = ""darkgrey"";<br />        }<br />    }<br /><br />    MouseArea{<br />        hoverEnabled: true<br />        anchors.fill: parent<br />        onClicked: {<br />            if(!isDisabled)<br />            {<br />    ...<br />            }<br />        }<br /> ...<br />}" +
            "</pre><p>The JavaScript for the main control was modified to become somewhat more generic. There is certainly space for some more improvement! Tabs are currently added with the minimum number of properties. When all the tabs are added, the recalculateTabProperties() is called to calculate properties of each tab such as width, margins or anchors using the total number of tabs and the index of the current tab as starting points. The <strong>applyXMLModel()</strong> is totally optional, it just sits there so that the code that reads the XML is not lost.</p><p>Another bit of functionality is adding a tab \"on the fly\", to the right of the existing tabs. Using the changes above, the <strong>addOneMoreTab()</strong> function adds a <strong>CustomTab</strong> object to the children of the <strong>tabsRow</strong> element. Currently this function modifies the <strong>iPosition</strong> of the previous tab to make sure it's no longer marked as rightmost. As an improvement, this bit can be moved into the <strong>recalculateTabProperties()</strong>, where it most likely belongs. The way the tab is added is shown on the screenshots.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/08032012_Nice_Tabs.png\" alt=\"Nice Tabs\" /></div><p align=\"center\">Tab Control Loaded</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/08032012_Nice_Tabs_2.png\" alt=\"Nice Tabs\" /></div>" +
            "<p align=\"center\">New Tab Added Dynamically</p><p>Full code of the main application.</p><pre class=\"brush: js\">" +
            @"import QtQuick 1.0<br /><br />Rectangle {<br />    id: screen<br />    width: 490; height: 400<br />    property int numTabs: 5<br />    property int margin: 2<br />    property string transparentColor : ""transparent""<br />    property string redColor: ""red""<br />    property string grayColor: ""#B7B9BC"" // ""lightgray""<br /><br />    function addOneMoreTab()<br />    {<br />        //get the current tab count<br />        var numTabs = tabsRow.children.length;<br />        //get the last tab and change its position state<br />        var lastTab = tabsRow.children[numTabs-1];<br />        lastTab.iPosition = 1;<br />        //add the new tab<br />        var tab = Qt.createComponent(""CustomTab.qml"");<br />        var obj = tab.createObject(tabsRow);<br /><br />        obj.tabIndex = tabsRow.children.length;<br />        obj.tabtext = ""Just Added"";<br /><br />        recalculateTabProperties();<br /><br />        tabsRow.clearState();<br />    }<br /><br />    function recalculateTabProperties()<br />    {<br />       var numTabs = tabsRow.children.length;<br />       var i=0;<br />       for(i=0;i<=numTabs-1;i++)<br />       {<br />           console.log(""recalculating tab "" + i);<br /><br />           var obj = tabsRow.children[i];<br />           if(i==0)<br />           {<br />              obj.iPosition = 0;<br />           }<br />           else if(i==numTabs-1)<br />           {<br />               obj.iPosition = 2;<br />           }<br />           else<br />           {<br />               obj.iPosition = 1;<br />           }<br /><br />           obj.ctlHeight = tabsRow.height - margin;<br />           obj.isSelected = false;<br />           obj.anchors.bottom = tabsRow.bottom;<br />           obj.anchors.bottomMargin = margin;<br /><br />           if(obj.iPosition == 0)<br />           {<br />               obj.ctlWidth = tabsRow.width/numTabs;<br />               obj.anchors.left = tabsRow.left;<br />           }<br />           else<br />           {<br />               obj.ctlWidth = tabsRow.width/tabsRow.children.length - margin;<br />               obj.anchors.left = tabsRow.children[i-1].right;<br />               obj.anchors.leftMargin = margin;<br />           }<br />       }<br />    }<br /><br />    function createTab()<br />    {<br />        var tab = Qt.createComponent(""CustomTab.qml"");<br /><br />        if(tab.status == Component.Ready)<br />        {<br />            var obj = tab.createObject(tabsRow);<br />            if(obj == null)addOneMoreTab();<br />            {<br />                return false;<br />            }<br /><br />            var numTabs = tabsRow.children.length<br />            console.log(""tabsRow children:"" + numTabs)<br /><br />            obj.tabIndex = numTabs;<br />        }<br />        return true;<br />    }<br /><br />    function applyXMLModel()<br />    {<br />        var i=0;<br />        for(i=0;i<=xmlTabModel.count-1;i++)<br />        {<br />            var obj = tabsRow.children[i];<br />            if(obj != null)<br />            {<br />                console.log(""obj is not null"")<br />                if(xmlTabModel.get(i).size == ""Disabled"")<br />                {<br />                    obj.tabtext = ""Disabled"";<br />                    obj.isDisabled = true;<br />                }<br />                else<br />                {<br />                   obj.tabtext = xmlTabModel.get(i).name;<br />                }<br />            }<br />        }<br />    }<br /><br />    function createTabs(num)<br />    {<br />       var i=0;<br /><br />       for(i=0;i<=num-1;i++)<br />       {<br />           var success = createTab();<br />           if(!success)<br />           {<br />               console.log(""Failed to create tab #"" + i);<br />           }<br />       }<br /><br />       recalculateTabProperties();<br />       applyXMLModel();<br /><br />       tabsRow.clearState();<br />    }<br /><br />    XmlListModel{<br />        id: xmlTabModel<br />        source: ""tabs.xml""<br />        query: ""/tabList/tab""<br />        XmlRole{name: ""name""; query: ""name/string()"" }<br />        XmlRole{name: ""size""; query: ""size/string()""}<br /><br />        onCountChanged: {<br />            createTabs(count);<br />        }<br />    }<br /><br />Rectangle {<br />    id: backRect<br />    radius: 10<br />    width: parent.width<br />    height: parent.height - 50       // need to expand to free space<br />    color: grayColor<br />    anchors.top: parent.top<br />    anchors { leftMargin: 10; bottomMargin: 10; topMargin: 10; rightMargin:10 }<br /><br />Rectangle{<br />    id: tabsRect<br />    radius: 10<br />    width: parent.width<br />    height: 80<br />    anchors.top: parent.top<br /><br />    Row{<br />        id:tabsRow<br />        width: parent.width<br />        height: parent.height<br /><br />        function clearState()<br />        {<br />            var j=0;<br />            for(j=0;j<= tabsRow.children.length - 1;j++)<br />            {<br />                children[j].isSelected = false;<br />                children[j].state = ""unselected"";<br />                children[j].applyState();<br />            }<br />        }<br />      }<br />    }<br />  }<br /><br />  Rectangle{<br />     id: buttonRect<br />     height: 40<br />     width:  75<br />     border.width: 1<br />     border.color: ""black""<br />     anchors.bottom: parent.bottom<br /><br />     Text{<br />         text: ""Click Here""<br />         x:5<br />         y:10<br />     }<br /><br />     MouseArea{<br />         anchors.fill: parent<br />         onClicked: {<br />            addOneMoreTab();<br />         }<br />     }<br />  }<br />}" + "</pre><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_08032012_k = "QML TabControl disable enable add tab on the fly software development";
        public const string content_08032012_d = "Disabling the Tab in the Tab Control and adding tabs on the fly with QML";

        //"Loading Tabs Dynamically"
        public const string content_07032012_b = "<p>The next task is to remove all hard-coded <strong>CustomTab</strong> elements from the custom tab control and load them all through JavaScript. Furthermore, verify that any settings can be loaded from the XML file. For the start, let's use the simple XML file, suspiciously similar to the one I've recently used to playing with a ListView element.</p>";
        public const string content_07032012_r = "<pre class=\"brush: xml\">" +
            @"&lt;?xml version=""1.0"" encoding=""utf-8""?&gt;<br />&lt;tabList&gt;<br /> &lt;tab&gt;<br />   &lt;name&gt;Tab 1&lt;/name&gt;<br />   &lt;size&gt;Medium&lt;/size&gt;<br /> &lt;/tab&gt;<br /> &lt;tab&gt;<br />   &lt;name&gt;Tab 2&lt;/name&gt;<br />   &lt;size&gt;Medium&lt;/size&gt;<br /> &lt;/tab&gt;<br /> &lt;tab&gt;<br />   &lt;name&gt;Tab 3&lt;/name&gt;<br />   &lt;size&gt;Medium&lt;/size&gt;<br /> &lt;/tab&gt;<br /> &lt;tab&gt;<br />   &lt;name&gt;Tab 4&lt;/name&gt;<br />   &lt;size&gt;Medium&lt;/size&gt;<br /> &lt;/tab&gt;<br /> &lt;tab&gt;<br />   &lt;name&gt;Tab 5&lt;/name&gt;<br />   &lt;size&gt;Medium&lt;/size&gt;<br /> &lt;/tab&gt;<br />&lt;/tabList&gt;" +
            "</pre><p>Just like in a ListView example, the <strong>XmlListModel</strong> element will be used to read data from the XML file. The usage of the <strong>XmlListModel</strong> is not limited to lists - data can be accessed directly. When the <strong>XmlListModel</strong> element is just created, it has no data. Then, at some point, data is loaded and the <strong>onCountChanged</strong> event fires. This fact is used to trigger the creation of tabs.</p><pre class=\"brush: js\">" +
            @"XmlListModel{<br /> id: xmlTabModel<br /> source: ""tabs.xml""<br /> query: ""/tabList/tab""<br /> XmlRole{name: ""name""; query: ""name/string()"" }<br /> XmlRole{name: ""size""; query: ""size/string()""}<br /><br /> onCountChanged: {<br />  createTabs(count);<br /> }<br />}" +
            "</pre><p>This line</p><pre class=\"brush: js\">" + @"var tab = Qt.createComponent(""CustomTab.qml"");" +
            "</pre><p>creates a <strong>Component</strong> object created using the QML file that is located at the address specified - can be a local file, or a URL, for example. The next point of interest,</p><pre class=\"brush: js\">" + @"var obj = tab.createObject(tabsRow);" + "</pre><p>creates an object instance of that component. The parameter is the parent object. So, I want the tab to be a child of the <strong>tabsRow</strong>. Next, some simple JavaScript follows - I'm dynamically assigning properties to the object which were previously hard-coded and make sure the initial position of the tabs is correct. The line</p><pre class=\"brush: js\">obj.tabtext = xmlTabModel.get(i).name;</pre><p>confirms that my XML file was read correctly: I'm displaying the name property from the XML file on the tab. That's about all that's interesting about this example - the behavior or the visual layout did not change, just the way the tabs are created inside the tab control.</p><p>The <strong>CustomTab.qml</strong> did not change much. I only added a tabtext property that is used by a Text element to display some text that is retrieved from the XML file and confirms that the tab control works as expected. So all the code added to the <strong>CustomTab.qml</strong> is summarized below:</p><pre class=\"brush: js\">" +
            @"import QtQuick 1.0<br /><br />Rectangle {<br />    id: customTab<br /> ...<br /> <br />    property string tabtext;<br /><br />    Text{<br />        id: textOnTab;<br />        text: tabtext;<br />        y:30;<br />        z:1;<br />    }<br /> ...<br />}</pre>" + "<p>The screenshot is about the same.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/07032012_Dynamic_Tabs.png\" alt=\"Dynamic Tabs\" /></div>" +
            "<p align=\"center\">The Tab Control - Looks the Same, but Dynamic</p><p>The full main QML file for reference, there's much more JavaScript and less QML layout:</p><pre class=\"brush: js\">" +
            @"import QtQuick 1.0<br /><br />Rectangle {<br />    id: screen<br />    width: 490; height: 400<br />    property int numTabs: 5<br />    property int margin: 2<br />    property string transparentColor : ""transparent""<br />    property string redColor: ""red""<br />    property string grayColor: ""#B7B9BC"" // ""lightgray""<br /><br />    function createTab(i, num)<br />    {<br />        var tab = Qt.createComponent(""CustomTab.qml"");<br /><br />        if(tab.status == Component.Ready)<br />        {<br />            var obj = tab.createObject(tabsRow);<br /><br />            if(obj == null)<br />                return false;<br />            if(i == 0)<br />               obj.iPosition =  0;<br />            else if(i == num-1)<br />               obj.iPosition = 2;<br />            else<br />               obj.iPosition = 1;<br /><br />            obj.ctlHeight = tabsRow.height - margin;<br />            obj.isSelected = false;<br />            obj.anchors.bottom = tabsRow.bottom;<br />            obj.anchors.bottomMargin = margin;<br />            obj.tabtext = xmlTabModel.get(i).name;<br />            if(i == 0)<br />            {<br />                obj.ctlWidth = tabsRow.width/num;<br />                obj.anchors.left = tabsRow.left;<br />            }<br />            else<br />            {<br />                obj.ctlWidth = tabsRow.width/num - margin;<br />                obj.anchors.left = tabsRow.children[i-1].right;<br />                obj.anchors.leftMargin = margin;<br />            }<br />         }<br />         else<br />         {<br />            return false;<br />         }<br />         return true;<br />    }<br /><br />    function createTabs(num)<br />    {<br />       var i=0;<br /><br />       for(i=0;i<=num-1;i++)<br />       {<br />           var success = createTab(i, num);<br />           if(!success)<br />           {<br />               console.log(""Failed to create tab #"" + i);<br />           }<br />       }<br /><br />       tabsRow.clearState();<br />    }<br /><br />    XmlListModel{<br />        id: xmlTabModel<br />        source: ""tabs.xml""<br />        query: ""/tabList/tab""<br />        XmlRole{name: ""name""; query: ""name/string()"" }<br />        XmlRole{name: ""size""; query: ""size/string()""}<br /><br />        onCountChanged: {<br />            createTabs(count);<br />        }<br />    }<br /><br />Rectangle {<br />    id: backRect<br />    radius: 10<br />    width: parent.width<br />    height: parent.height        // need to expand to free space<br />    color: grayColor<br />    anchors.top: parent.top<br />    anchors { leftMargin: 10; bottomMargin: 10; topMargin: 10; rightMargin:10 }<br /><br />Rectangle{<br />    id: tabsRect<br />    radius: 10<br />    width: parent.width<br />    height: 80<br />    anchors.top: parent.top<br /><br />    Row{<br />        id:tabsRow<br />        width: parent.width<br />        height: parent.height<br />        anchors.fill: parent<br /><br />        function clearState()<br />        {<br />            var j=1;<br />            for(j=0;j<= numTabs-1;j++)<br />            {<br />                children[j].isSelected = false;<br />                children[j].state = ""unselected"";<br />                children[j].applyState();<br />            }<br />        }<br />      }<br />    }<br />  }<br />}" +
            "</pre><p>References</p><a href=\"http://qt-project.org/doc/qt-4.8/qdeclarativedynamicobjects.html\">Dynamic Object Management in QML</a><br/><a href=\"http://www.pyside.org/docs/pyside/tutorials/qmladvancedtutorial/samegame2.html\">QML Advanced Tutorial 2 - Populating the Game Canvas</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_07032012_k = "QML TabControl dynamic tab loading software development";
        public const string content_07032012_d = "Loading Tabs Dynamically with QML";

        //"Customizing a Tab Control with QML"
        public const string content_06032012_b = "<p>As my next task, I'm developing a tab control that has to follow certain design standards. The end result is shown at the end of this post. The easiest way to do this would be just to use images as backgrounds for UI elements. However, the control also has to scale and in most cases the uneven stretching of images would lead to ugliness. Therefore, the whole control had to be developed by using QML design elements, such as <strong>Rectangles</strong>, <strong>Rows</strong> or <strong>Grids</strong>. There were two major issues to be considered: firstly, there is no way to round only certain corners of the rectangle, its either all or none. Secondly, when the tab is selected or moused over, a red line has to be displayed across the top of the tab.</p><p>Generally, the tab control is a rectangle which has a row at the top. A tab is a component which is specified in a separate QML file, <strong>CustomTab.qml</strong>. For now, tabs are hard-coded as child elements of the said row. Other than layout properties, that anchor the tab to a proper position, it also holds some properties that are important for visual display. <strong>isSelected</strong> defines if the tab is currently selected. If it is, it becomes slightly taller and displays a red line across the top. <strong>isHighlighted</strong> defines if the mouse is currently over the tab. If it is, a more narrow red line is displayed across the top, and the tab height does not change. The <strong>iPosition</strong> has three states and defines if the tab is rightmost, leftmost or is in between the tabs. Rightmost tab has the top right corner rounded, leftmost - the left, and the middle tabs are not rounded. When the tab is selected, both corners are rounded regardless of position. All of this logic was implemented in somewhat messy JavaScript functions.</p>";
        public const string content_06032012_r = "<p>From the design point of view, each tab is a 2x2 <strong>Grid</strong> element. A rectangular grid covers a rounded rectangle. By making top right or top left elements or both of the grid transparent, I make rounded corners visible, giving the impression of a tab with one or two rounded corners. Top elements of the grid are also used to change the height of the red line by modifying their height.</p><p>The most important function is the <strong>applyState()</strong>. After tab properties such as <strong>isSelected</strong> or <strong>isHighlighted</strong> were assigned, the function makes sure that the tab is visualised correctly.</p><pre class=\"brush: js\">" +
            @"function applyState()<br />{<br /> if(isSelected)<br /> {<br />  applyHeights(ctlHeight + redLineHeight/2, redLineHeight, ctlHeight + redLineHeight/2)<br />  customTab.color=redColor<br /> }<br /> else if(isHighlighted)<br /> {<br />  applyHeights(ctlHeight, redLineHeight/2, ctlHeight)<br />  customTab.color = redColor<br /> }<br /> else<br /> {<br />  applyHeights(ctlHeight, redLineHeight, ctlHeight)<br />  customTab.color=grayColor<br /> }<br /><br /> if(iPosition == ""0"")<br /> {<br />  elements.children[0].color = transparentColor;<br />  elements.children[1].color = getCorrectColor();<br /> }<br /> else if(iPosition == ""1"")<br /> {<br />  elements.children[0].color = getCorrectColor();<br />  elements.children[1].color = getCorrectColor();<br /> }<br /> else if(iPosition == ""2"")<br /> {<br />  elements.children[0].color = getCorrectColor();<br />  elements.children[1].color = transparentColor;<br /> }<br />}</pre>" +
            "<p>If the tab is selected, the height of the grid rows is increased as required, if it is highlighted the height of the top row is adjusted and if none of that, it is returned to the initial state. Next, the grid elements are colored appropriately or set to transparent according to the tab position in the tab control.</p><pre class=\"brush: js\">" +
            @"function clearState()<br />{<br /> var j=1;<br /> for(j=0;j<= numTabs-1;j++)<br /> {<br />  children[j].isSelected = false;<br />  children[j].state = ""unselected"";<br />  children[j].applyState();<br /> }<br />}" + "</pre><p>The <strong>clearState()</strong> function just loops through the children of <strong>tabRow</strong>, which are individual tabs. It clears the <strong>isSelected</strong> tag and applies the proper <strong>state</strong> (see below). Then it applies the tab state, essentially resetting it to initial state.</p><pre class=\"brush: js\">" +
            @"MouseArea{<br /> hoverEnabled: true<br /> anchors.fill: parent<br /> onClicked: {<br />  parent.parent.clearState();<br />  parent.isSelected = true;<br />  parent.state = ""selected""<br />  parent.applyState();<br /> }<br /> onEntered: {<br />  parent.isHighlighted = true;<br />  if(!parent.isSelected)<br />  {<br />   parent.applyState();<br />  }<br /> }<br /> onExited: {<br />  parent.isHighlighted = false;<br />  if(!parent.isSelected)<br />  {<br />   parent.applyState();<br />  }<br /> }<br />}" +
            "</pre><p>Each tab has a <strong>MouseArea</strong>, which listens to three events: <strong>onClicked</strong>, <strong>onEntered</strong> and <strong>onExited</strong>. Note the usage of <strong>hoverEnabled: true</strong>, without which the <strong>MouseArea</strong> would just ignore the last two events mentioned. When the tab is clicked, all tabs are reset first, and then the clicked tab is set to be selected. When the tab is entered or exited, the <strong>isHighlighted</strong> attribute is toggled and the changes are applied to the tab - but only is it is not already selected. If it is, the event is ignored since the tab should not change if it is already selected, and the mouse is over it.</p><pre class=\"brush: js\">" +
            @"states: [<br /> State {<br />  name: ""selected""<br />  PropertyChanges { target: customTab; anchors.bottomMargin: 0}<br /> },<br /><br /> State {<br />  name: ""unselected""<br />  PropertyChanges {target: customTab; anchors.bottomMargin: margin}<br /> }<br />]" +
            "</pre><p>And lastly, the <strong>anchor.bottomMargin</strong> can not be changed from JavaScript, therefore two states had to be created. Removing the margin when the tab is selected gives the \"melding\" effect, when the white line between the selected tab and the rest of the control disappears, and appears again when a different tab is selected. The state of the tab gets toggled by the <strong>onClicked</strong> event of the <strong>MouseArea</strong>, as shown above.</p><p>Finally, the full listing of the code - it is not yet overly huge.</p><pre class=\"brush: js\">" +
            @"// The main qml file<br /><br />import QtQuick 1.0<br /><br />Rectangle {<br />    id: screen<br />    width: 490; height: 400<br />    property int numTabs: 5<br />    property int margin: 2<br />    property string transparentColor : ""transparent""<br />    property string redColor: ""red""<br />    property string grayColor: ""#B7B9BC"" // ""lightgray""<br /><br />Rectangle {<br />    id: backRect<br />    radius: 10<br />    width: parent.width<br />    height: parent.height       <br />    color: grayColor<br />    anchors.top: parent.top<br />    anchors { leftMargin: 10; bottomMargin: 10; topMargin: 10; rightMargin:10 }<br /><br />Rectangle{<br />    id: tabsRect<br />    radius: 10<br />    width: parent.width<br />    height: 80<br />    anchors.top: parent.top<br /><br />    Row{<br />        id:tabsRow<br />        width: parent.width<br />        height: parent.height<br />        anchors.fill: parent<br /><br />        function clearState()<br />        {<br />            var j=1;<br />            for(j=0;j<= numTabs-1;j++)<br />            {<br />                children[j].isSelected = false;<br />                children[j].state = ""unselected"";<br />                children[j].applyState();<br />            }<br />        }<br /><br />        CustomTab{<br />            id: customTab0<br />            ctlWidth: parent.width/numTabs<br />            ctlHeight: parent.height - margin<br />            tabIndex: 0<br />            isSelected: false;<br />            iPosition: 0<br />            anchors { left: parent.left; bottom: parent.bottom; bottomMargin: margin }<br />        }<br /><br />        CustomTab{<br />            id: customTab1<br />            ctlWidth: parent.width/numTabs-margin<br />            ctlHeight: parent.height - margin<br />            tabIndex: 1<br />            isSelected: false;<br />            iPosition: 1<br />            anchors { left: customTab0.right; bottom: parent.bottom; leftMargin: margin; bottomMargin: margin; }<br />        }<br /><br />        CustomTab{<br />            id: customTab2<br />            ctlWidth: parent.width/numTabs-margin<br />            ctlHeight: parent.height - margin<br />            tabIndex: 2<br />            isSelected: false;<br />            iPosition: 1<br />            anchors { left: customTab1.right; bottom: parent.bottom; leftMargin: margin; bottomMargin: margin; }<br />        }<br /><br />        CustomTab{<br />            id: customTab3<br />            ctlWidth: parent.width/numTabs-margin<br />            ctlHeight: parent.height - margin<br />            tabIndex: 3<br />            isSelected: false;<br />            iPosition: 1<br />            anchors { left: customTab2.right; bottom: parent.bottom; leftMargin: margin; bottomMargin: margin; }<br />        }<br /><br />        CustomTab{<br />            id: customTab4<br />            ctlWidth: parent.width/numTabs-margin<br />            ctlHeight: parent.height - margin<br />            tabIndex: 4<br />            isSelected: false;<br />            iPosition: 2<br />            anchors { left: customTab3.right; bottom: parent.bottom; leftMargin: margin; bottomMargin: margin; }<br />        }<br />      }<br />    }<br />  }<br />}" + "</pre><pre class=\"brush: js\">" +
            @"// The CustomTab.qml file<br /><br />import QtQuick 1.0<br /><br />Rectangle {<br /><br />    id: customTab<br />    clip: true<br /><br />    property int ctlWidth;<br />    property int ctlHeight;<br />    property int redLineHeight: 20;<br />    property bool isSelected;<br />    property bool isHighlighted;<br />    property int tabIndex;<br />    property int iPosition; // 0 = left; 1 = middle; 2 = right<br /><br />    width: ctlWidth<br />    height: ctlHeight<br />    color:  ""#B7B9BC""<br />    radius: 10<br /><br />    function applyHeights(customTabHeight, topHeight, bottomHeight)<br />    {<br />        customTab.height = customTabHeight<br /><br />        elements.children[0].height = topHeight<br />        elements.children[1].height = topHeight<br />        elements.children[2].height = bottomHeight<br />        elements.children[3].height = bottomHeight<br />    }<br /><br />    function clearChildState()<br />    {<br />        applyHeights(ctlHeight, redLineHeight, ctlHeight - redLineHeight)<br />    }<br /><br />    function getCorrectColor()<br />    {<br />        if(isSelected)<br />        {<br />            return transparentColor;<br />        }<br />        else if(isHighlighted)<br />        {<br />            return redColor;<br />        }<br />        else<br />        {<br />            return grayColor;<br />        }<br />    }<br /><br />    function applyState()<br />    {<br />        if(isSelected)<br />        {<br />            applyHeights(ctlHeight + redLineHeight/2, redLineHeight, ctlHeight + redLineHeight/2)<br />            customTab.color=redColor<br />        }<br />        else if(isHighlighted)<br />        {<br />            applyHeights(ctlHeight, redLineHeight/2, ctlHeight)<br />            customTab.color = redColor<br />        }<br />        else<br />        {<br />            applyHeights(ctlHeight, redLineHeight, ctlHeight)<br />            customTab.color=grayColor<br />        }<br /><br />        if(iPosition == ""0"")<br />        {<br />            elements.children[0].color = transparentColor;<br />            elements.children[1].color = getCorrectColor();<br />        }<br />        else if(iPosition == ""1"")<br />        {<br />            elements.children[0].color = getCorrectColor();<br />            elements.children[1].color = getCorrectColor();<br />        }<br />        else if(iPosition == ""2"")<br />        {<br />            elements.children[0].color = getCorrectColor();<br />            elements.children[1].color = transparentColor;<br />        }<br />    }<br /><br />    Component.onCompleted: {<br />            applyState();<br />        }<br /><br />    Grid{<br />        id:elements<br />        width: parent.width<br />        height: parent.height<br />        columns: 2<br />        rows: 2<br /><br />        Rectangle{id: topLeft; color: grayColor; width: parent.width/2; height: redLineHeight;}<br />        Rectangle{id: topRight; color: grayColor; width: parent.width/2; height: redLineHeight;}<br />        Rectangle{id: bottomLeft; color: grayColor; width: parent.width/2; height: parent.height - redLineHeight;}<br />        Rectangle{id: bottomRight; color: grayColor; width: parent.width/2; height: parent.height - redLineHeight;}<br />    }<br /><br />    MouseArea{<br />        hoverEnabled: true<br />        anchors.fill: parent<br />        onClicked: {<br />            parent.parent.clearState();<br />            parent.isSelected = true;<br />            parent.state = ""selected""<br />            parent.applyState();<br />        }<br />        onEntered: {<br />            parent.isHighlighted = true;<br />            if(!parent.isSelected)<br />            {<br />                parent.applyState();<br />            }<br />        }<br />        onExited: {<br />            parent.isHighlighted = false;<br />            if(!parent.isSelected)<br />            {<br />                parent.applyState();<br />            }<br />        }<br />    }<br /><br />    states: [<br />        State {<br />            name: ""selected""<br />            PropertyChanges { target: customTab; anchors.bottomMargin: 0}<br />        },<br /><br />        State {<br />            name: ""unselected""<br />            PropertyChanges {target:customTab; anchors.bottomMargin: margin}<br />        }<br />    ]<br />}" +
            "</pre><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/06032012_Nice_Tab.png\" alt=\"Nice Tab\" /></div>" +
            "<p align=\"center\">Tabs Just Loaded, None Selected</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/06032012_Nice_Tab_2.png\" alt=\"Nice Tab\" /></div><p align=\"center\">Tab Selected, Another Tab Highlighted</p><p>References</p><a href=\"http://doc.qt.nokia.com/4.7-snapshot/qdeclarativestates.html\">QML States</a><br/><a href=\"http://doc.qt.nokia.com/4.7-snapshot/qml-propertychanges.html\">QML PropertyChanges Element</a><br/><a href=\"https://qt-project.org/doc/qt-4.8/qml-anchor-layout.html\">Anchor-based Layout in QML</a><br/><a href=\"http://www.quirksmode.org/js/function.html\">JavaScript Functions</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_06032012_k = "Software development QML TabControl customisation";
        public const string content_06032012_d = "Customizing a Tab Control with QML";

        //"ListView in QML"
        public const string content_05032012_b = "<p>The <strong>ListView</strong> is similar to ListViews in other programming languages - it displays a list of items! There is some difference, of course, in the way it is implemented. The data to be displayed is called a <strong>model</strong>, and if it is some fixed data then it can be defined right in the code by adding a <strong>ListModel</strong> element like this:</p>";
        public const string content_05032012_r = "<pre class=\"brush: js\">" + @"<br />ListModel {<br />     ListElement {<br />         name: ""Joe Bloggs""<br />         number: ""123 1234""<br />     }<br />}" + "</pre><p>That will be a list with just one element. Next, there is the <strong>delegate</strong>. The delegate defines the way data is displayed. Again, it can be very simple, for example</p><pre class=\"brush: js\">" +
            @"ListView{<br /> id: listView<br /> <br /> ...<br /><br /> delegate: Text{text: name}<br />}" + "</pre><p>Such delegate will just display some text which is taken from the model.</p><p>To make this post slightly less boring, I will first make the <strong>ListView</strong> read its model from the xml file. For that purpose, the <strong>XmlListModel</strong> element is used. I will use this sample XML structure:</p><pre class=\"brush: xml\">" +
            @"&lt;?xml version=""1.0"" encoding=""utf-8""?&gt;<br />&lt;listModel&gt;<br /> &lt;item&gt;<br />   &lt;name&gt;Item One&lt;/name&gt;<br />   &lt;size&gt;Medium&lt;/size&gt;<br />   &lt;desc&gt;Item One Detailed Description&lt;/desc&gt;<br /> &lt;/item&gt;<br /> &lt;item&gt;<br />   &lt;name&gt;Item Two&lt;/name&gt;<br />   &lt;size&gt;Large&lt;/size&gt;<br />   &lt;desc&gt;Item Three Detailed Description&lt;/desc&gt;<br /> &lt;/item&gt;<br /> &lt;item&gt;<br />   &lt;name&gt;Item Three&lt;/name&gt;<br />   &lt;size&gt;Small&lt;/size&gt;<br />   &lt;desc&gt;Item Three Detailed Description&lt;/desc&gt;<br /> &lt;/item&gt;<br /> &lt;item&gt;<br />   &lt;name&gt;Item Four&lt;/name&gt;<br />   &lt;size&gt;Small&lt;/size&gt;<br />   &lt;desc&gt;Item Four Detailed Description&lt;/desc&gt;<br /> &lt;/item&gt;<br /> &lt;item&gt;<br />   &lt;name&gt;Item Five&lt;/name&gt;<br />   &lt;size&gt;Small&lt;/size&gt;<br />   &lt;desc&gt;Item Five Detailed Description&lt;/desc&gt;<br /> &lt;/item&gt;<br />&lt;/listModel&gt;" +
            "</pre><p>The <strong>XMLListModel</strong> that reads this file will be defined as follows:</p><pre class=\"brush: js\">" +
            @"XmlListModel{<br /> id: xmlTestListModel<br /> source: ""listModel.xml""<br /> query: ""/listModel/item""<br /> XmlRole{name: ""name""; query: ""name/string()"" }<br /> XmlRole{name: ""size""; query: ""size/string()"" }<br /> XmlRole{name: ""desc""; query: ""desc/string()"" }<br />}" +
            "</pre><p>I give the element the location where to look for the file, and the XPath to look for individual items. Next, I define all the tags I want to read into the model - in this case, name, size and description, but there can be more. The model will parse the XML file and read all this information, and if I want to use it or not - it's completely up to me. Next, I'll do something with the delegate to show that its activity is not limited to displaying lines of text, it is capable of more complex behaviour. Before that, a quick note about the <strong>section</strong> property of the ListView: it allows the list to be separated into different parts, and the sections can have a delegate specified. Additionally, QML allows breaking the code into multiple files. I will add the file called SectionHeading.qml and specify the delegate for the section in the following manner:</p><pre class=\"brush: js\">" +
            @"import QtQuick 1.0<br /><br />Component {<br />    id: sectionHeading<br />    Rectangle {<br />        width: listViewRect.width<br />        height: childrenRect.height<br />        color: ""lightsteelblue""<br /><br />        Text {<br />            text: section<br />            font.bold: true<br />        }<br />    }<br />}</pre>" + "<p>This delegate displays the section as the rectangle with a background colour, and displays the text that is defined in the variable section, in bold. To reference this delegate I can use the QML file name: <strong>section.delegate: SectionHeading{}</strong>. At this point the \"desc\" property from the XML file is not used anywhere - but the XmlListModel already knows it and I can use it later. Here is the full code after making all these modifications:</p><pre class=\"brush: js\">" +
            @"import QtQuick 1.0<br /><br />Rectangle{<br /><br />    id: main<br />    color:  ""lightGrey""<br />    property int rectWidth:  480<br />    property int rectHeight:  480<br /><br />    width:  rectWidth<br />    height: rectHeight<br /><br />    Rectangle{<br />        id: listViewPanel<br />        width: rectWidth/3<br />        height: rectHeight<br />        x: 0<br />        y: 0<br />        border.color: ""black""<br />        color: ""green""<br />        border.width: 1<br /><br />        Text{<br />            text: ""Left panel""<br />        }<br /><br />        Rectangle{<br />            id: listViewRect<br />            state: ""list""<br />            anchors.fill: parent<br />            width: parent.width<br />            height: parent.height<br />            anchors.margins: 15<br /><br />            XmlListModel{<br />                id: xmlTestListModel<br />                source: ""listModel.xml""<br />                query: ""/listModel/item""<br />                XmlRole{name: ""name""; query: ""name/string()"" }<br />                XmlRole{name: ""size""; query: ""size/string()"" }<br />                XmlRole{name: ""desc""; query: ""desc/string()"" }<br />            }<br /><br />            ListView{<br />                id: listView<br /><br />                width: parent.width<br />                height:parent.height<br />                anchors.top: parent.top<br />                anchors.bottom: parent.bottom<br /><br />                model:  xmlTestListModel<br />                delegate: Text{text: name}<br /><br />                section.property: ""size""<br />                section.criteria: ViewSection.FullString<br />                section.delegate: SectionHeading{}<br />            }<br />        }<br />    }<br /><br />    Rectangle{<br />        id: detailsPanel<br />        width: parent.width*2/3<br />        height: parent.height*2/3<br />        x: parent.width/3<br />        y: 0<br />        border.color: ""black""<br />        color: ""yellow""<br />        border.width: 1<br /><br />        Text{<br />            text:  ""Details panel""<br />        }<br />    }<br /><br />    Rectangle{<br />        id: loggingPanel<br />        width:  parent.width*2/3<br />        height: parent.height/3<br />        border.color: ""black""<br />        border.width: 1<br />        x: parent.width/3<br />        y: parent.height*2/3<br />        color: ""blue""<br /><br />       Text{<br />           text: ""Logging panel""<br />       }<br />    }<br />}" + "</pre><p>The ListView is drawn inside a Rectangle. The XmlListModel then parses the XML file specified. The delegate for the ListView only displays the \"name\" property from the XML file for each item. The \"size\" property is assigned to the section. It is passed to the section.delegate as a parameter, which then displays it in bold, inside a Rectangle which has lightsteelblue background colour. The end result of the application is displayed below.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/05032012_QML_ListView.png\" alt=\"QML ListView\" /></div>" +
            "<p align=\"center\">The Example Application</p><p>References:</p><a href=\"http://doc.qt.nokia.com/4.7-snapshot/qml-listview.html\">QML ListView Element</a><br/><a href=\"http://qt-project.org/doc/qt-4.8/qml-xmllistmodel.html\">QML XmlListModel Element</a><br/><a href=\"http://qt-project.org/doc/qt-4.8/declarative-modelviews-listview-sections.html\">Models and Views: Sections ListView Example</a><br/><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_05032012_k = "QML ListView software development";
        public const string content_05032012_d = "Implementing ListView in QML";

        //"Starting With QML"
        public const string content_04032012_b = "<p>Qt is a framework for developing software with a GUI (and other software, too). It uses C++. QML (Qt Meta Language) is a declarative language for developing UI, and is based on JavaScript. I'll be using QML to build a prototype of a UI for a certain device. For that purpose, I'll be using Qt Creator that is installed on the Ubuntu 11.10. There are some new experiences here. I'll move on to connecting QML applications with C++ plugins later, but for now just some plain QML.</p>";
        public const string content_04032012_r = "<p>To create a simple QML project, I select <strong>File -> New File or Project</strong> from Qt Creator menu. From Projects I select <strong>Qt Quick Project</strong> and <strong>Qt Quick UI</strong> on the right.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/04032012_Qt.png\" alt=\"Qt\" /></div>" +
            "<p align=\"center\">New Qt Creator Project</p><p>Next I select the location for the project files and when I'm done, the project contains only one qml file and a project file with the extension \"qmlproject\" which I do not need to know much at that early stage. The qml file contains a simple \"Hello World\" application.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/04032012_QMLExample.png\" alt=\"QMLExample\" /></div>" +
            "<p align=\"center\">Qt Creator Environment</p><p>Compared to my experience, QML appears to be somewhat similar to WPF, considering that I define the layout with the declarative part and then can apply scripts additionally to provide extra functionality. In this first post, I'll only look at some of the most basic layout elements. I'm starting with the UI that has three panels: the left panel takes up one third of the space, and the right side is divided into the top and bottom parts, the bottom being one third of the vertical space. The screen size is hard-coded at this point.</p><p>The most basic element is a <strong>Rectangle</strong>. It is just that - a UI element of a rectangular shape. It can be filled with a color and can hold other UI elements. For example, it can hold a <strong>Text</strong> element which is, essentially, a label.</p><pre class=\"brush:js\">" +
            @"Rectangle{<br /> id: listViewPanel<br /> width: 100<br /> height: 100<br /> x: 0<br /> y: 0<br /> border.color: ""black""<br /> color: ""green""<br /> border.width: 1<br /><br /> Text{<br />  text: ""Left panel""<br /> }<br />}" + "</pre><p>The <strong>id</strong> is optional, but will be used later to reference the element. Other parameters are self-explanatory.</p><p>Here is the first QML application - just a few rectangles that take up the screen.</p><pre class=\"brush:js\">" +
            @"import QtQuick 1.0<br /><br />Rectangle{<br /><br /> id: main<br /> color:  ""lightGrey""<br /> property int rectWidth:  480<br /> property int rectHeight:  480<br /><br /> width:  rectWidth<br /> height: rectHeight<br /><br /> Rectangle{<br />  id: listViewPanel<br />  width: rectWidth/3<br />  height: rectHeight<br />  x: 0<br />  y: 0<br />  border.color: ""black""<br />  color: ""green""<br />  border.width: 1<br /><br />  Text{<br />   text: ""Left panel""<br />  }<br /> }<br /><br /> Rectangle{<br />  id: detailsPanel<br />  width: parent.width*2/3<br />  height: parent.height*2/3<br />  x: parent.width/3<br />  y: 0<br />  border.color: ""black""<br />  color: ""yellow""<br />  border.width: 1<br /><br />  Text{<br />   text:  ""Details panel""<br />  }<br /> }<br /><br /> Rectangle{<br />  id: loggingPanel<br />  width:  parent.width*2/3<br />  height: parent.height/3<br />  border.color: ""black""<br />  border.width: 1<br />  x: parent.width/3<br />  y: parent.height*2/3<br />  color: ""blue""<br /><br />    Text{<br />     text: ""Logging panel""<br />    }<br /> }<br />}" +
            "</pre><p>Rectangle <strong>main</strong> is the parent element, and defines the size of the screen. It defines two properties, which are screen height and width and are used by child elements to calculate their own sizes. A child element can access the parent's properties by using <strong>parent.property</strong> syntax (i.e. <strong>parent.height</strong>). Each child rectangle has a <strong>Text</strong> element inside, just to provide some visual feedback.</p><p>Here's the result of executing the application:</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/04032012_QMLExample_2.png\" alt=\"QMLExample\" /></div>" +
            "<p align=\"center\">First QML Application</p><p>References:</p><a href=\"http://qt.nokia.com/\">Qt - Cross-platform application and UI framework</a><br/><a href=\"http://doc.qt.nokia.com/4.7-snapshot/qml-rectangle.html\">QML Rectangle Element</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_04032012_k = "QML Qt declarative meta language software development";
        public const string content_04032012_d = "I get to learn a new language - QML (Qt meta language)";

        //Writing my First Web Script for Alfresco"
        public const string content_26022012_b = "<p>I started with the goal of creating a custom XML file which would be somewhat a \"data extract\" across a set of files which, in my case, will be located in the same folder. However, understanding how web scripts work and writing a functional web script that provides some meaningful output took me a bit longer than I expected (about 6 hours total, but now I know a lot about luceneSearch function and Alfresco AVM Store API), so at least I got to the point where I can list all the files in the folder and render this data in the HTML file.</p>";
        public const string content_26022012_r = "<p>The web script required 3 files to run: a definition file, a javaScript file which gets the folder details from the AVM store and a ftl (FreeMarker template) file which renders HTML. It is also possible to render output in JSON format (and other formats, I guess) but I did not get there yet. The files are called <strong>items.get.desc.xml</strong>, <strong>items.get.js</strong> and <strong>items.get.html.ftl</strong>.</p><p>I placed these files into the Alfresco \"classes\" path, in the subfolder \"items\" under \"webscripts\". The full path on my PC is</p><p>C:\\Alfresco\\tomcat\\webapps\\alfresco\\WEB-INF\\classes\\alfresco\\extension\\templates\\webscripts\\items</p><p>And the full path to the actual files I'm interested in is</p><p>Y:\\hostingitems\\HEAD\\DATA\\www\\avm_webapps\\ROOT\\items</p><p>where Y is the drive I mapped AVM repository to. \"hostingitems\" is the name of the web project where files were created. Currently I'm getting back all files in the folder, so if I want to sort out only XML files I'll need to additional logic.</p><p><strong>items.get.desc.xml</strong> is a web script description file. Basically, it tells where the script is located and what it does.</p><pre class=\"brush:xml\">" +
            @"&lt;webscript&gt;<br />&lt;shortname&gt;Get all items&lt;/shortname&gt;<br />&lt;description&gt;Returns a list of items&lt;/description&gt;<br />&lt;url&gt;/items&lt;/url&gt;<br />&lt;url&gt;/items.json&lt;/url&gt;<br />&lt;url&gt;/items.html&lt;/url&gt;<br />&lt;format default=""json""&gt;extension&lt;/format&gt;<br />&lt;authentication&gt;guest&lt;/authentication&gt;<br />&lt;transaction&gt;none&lt;/transaction&gt;<br />&lt;/webscript&gt;" + "</pre><p><strong>items.get.js</strong> returns the AVM store and the particular folder. The \"s\" variable is the web project name, and the \"p\" variable is the path to the folder. They can be passed to the web script as arguments rather than being hardcoded, but again that's a future \"TODO\" task.</p><pre class=\"brush:js\">" +
            @"script: {<br />var s = ""hostingitems"";<br />var p = ""/www/avm_webapps/ROOT/items/"";<br /><br />// get avm node<br />var store = avm.lookupStore(s);<br />if (store == null || store == undefined) <br />{<br /> status.code = 404;<br /> status.message = ""Store "" + s + "" not found."";<br /> status.redirect = true;<br /> break script;<br />}<br />// get items data folder<br />var itemsNode = avm.lookupNode(s + "":"" + p);<br />if (itemsNode == undefined) <br />{<br /> status.code = 404;<br /> status.message = ""Could not find items folder. Path:"" + p + "" Store:"" + s;<br /> status.redirect = true;<br /> break script;<br />}<br /><br />// set store and folder in the model<br />model.store = store;<br />model.folder = itemsNode;<br />}" + "</pre><p><strong>items.get.html.ftl</strong> is a template that renders html. Dead simple, it only loops through the folder contents and populates some settings of the files into an html table.</p><pre class=\"brush:xml\">" +
            @"&lt;#assign datetimeformat=""EEE, dd MMM yyyy HH:mm:ss zzz""&gt;<br />&lt;html&gt;<br />&lt;head&gt;<br /> &lt;title&gt;Items in folder: ${folder.displayPath}/${folder.name}&lt;/title&gt;<br />&lt;/head&gt;<br />&lt;body&gt;<br />&lt;p&gt;&lt;a href=""${url.serviceContext}/sample/avm/stores""&gt;AVM Store&lt;/a&gt;: ${store.id}&lt;/p&gt;<br />&lt;p&gt;AVM Folder: ${folder.displayPath}/${folder.name}&lt;/p&gt;<br />&lt;table&gt;<br />&lt;#list folder.children as child&gt;<br /> &lt;tr&gt;<br />  &lt;td&gt;${child.properties.creator}&lt;/td&gt;<br />  &lt;td&gt;${child.size}&lt;/td&gt;<br />  &lt;td&gt;${child.properties.modified?datetime}&lt;/td&gt;<br />  &lt;td&gt;<br />   &lt;a href=""${url.serviceContext}/api/node/content/${child.nodeRef.storeRef.protocol}<br />   /${child.nodeRef.storeRef.identifier}/${child.nodeRef.id}/${child.name?url}""&gt;${child.name}<br />   &lt;/a&gt;<br />  &lt;/td&gt;<br /> &lt;/tr&gt;<br />&lt;/#list&gt;<br />&lt;/table&gt;<br />&lt;/body&gt;<br />&lt;/html&gt;</pre><p>Now when my files are ready I can go and check the Alfresco web scripts by logging in and naivgating to<br/>http://localhost:8080/alfresco/service/index<br/><br />I currently have 463 web scripts.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/26022012_Web_Scripts_Home.png\" alt=\"Web Scripts Home\" /></div>" +
            "<p align=\"center\">Web Scripts Home</p><p>Browse all button shows me the list - fortunately the user defined web scripts are on the top so I do not have to scroll the whole list.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/26022012_Get_All_Items.png\" alt=\"Get All Items\" /></div>" +
            "<p align=\"center\">Web Script Details</p><p>This data obviously comes from my description file. This would be displayed even if the script itself results with an error. For example, if I make a syntax error in my javaScript and navigate to the alfresco/service/items.html, I will get an error message.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/26022012_Internal_Error.png\" alt=\"Internal Error\" /></div>" +
            "<p align=\"center\">Web Script Error</p><p>Fortunately, the error messages have been mostly helpful - in this case, it's just a missing closing bracket after an if statement. So, I fix it, save js file, go back to web scripts page, press \"Refresh web scripts\" to pick up my changes and try again. And there's my list of files generated by a web script.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/26022012_AVM_Store.png\" alt=\"AVM Store\" /></div>" +
            "<p align=\"center\">Web Script Results</p><p>Reference:</p><p><a href=\"http://www.packtpub.com/alfresco-developer-guide/book\">Alfresco Developer Guide</a><p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_26022012_k = "Alfresco Web Script";
        public const string content_26022012_d = "Writing my First Web Script for Alfresco";

        //"Amusing"
        public const string content_23022012_b = "<p><a href=\"http://www.google.com/patents/US20060063265\">Advance programmed sample processing system and methods of biological slide processing</a></p>";
        public const string content_23022012_r = "<p>\"A local area network for this type of system may also include features such as but not limited to: an Ethernet element, a token ring element, an arcnet element, a fiber distributed data interface element, an industry specification protocol, a bluetooth-based element <u>(named but not contemporary to King Harald Bluetooth of Denmark in the mid-tenth century!)</u>, a telecommunications industry specification\" ...</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/23022012_Amusing.png\" alt=\"Amusing\" /></div><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_23022012_k = "none";
        public const string content_23022012_d = "Random observation";

        //"Restricting Access to Content in Alfresco"
        public const string content_14022012_b = "<p>A small exercise in restricting access to certain actions with content.</p><p>Create at least two users. To create users, login as admin (the only account that has the rights to manage users). Navigate to Company Home -> User Homes. In the top bar, select \"Administration Console\".</p>";
        public const string content_14022012_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/14022012_My_Alfresco_Dashboard.png\" alt=\"My Alfresco Dashboard\" /></div>" +
        "<p align=\"center\">Link to Administration Console</p><p>In the Administrative Console, select Manage System Users. In the top ride part of the screen, select \"Create User\". The required fields are First Name, Last Name and Email. Other fields are optional. Select \"Next\".</p>" +
        "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/14022012_New_User_Wizard.png\" alt=\"New User Wizard\" /></div>" +
        "<p align=\"center\">New User Wizard</p><p>Assign the username and password and select Finish. Create another user in the same manner. Now we can assign these users to different roles and check if it makes any difference. Navigate to Company Home -> Web Projects. Select a Web Project. From Actions, select “Invite Web Project Users”. On the “Invite Web Project Users” screen, add one user as the Content Contributor, and another as a Content Reviewer. Click “Next”.</p>" +
        "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/14022012_Invite_Web_Project_Users.png\" alt=\"Invite Web Project Users\" /></div>" +
        "<p align=\"center\">Invite Web Project Users</p><p>No need to sending emails at this stage. On the \"Notify Users\" screen, leave default option as \"No\" and select \"Next\". On the summary screen, review the information, use \"Back\" to make changes or select \"Finish\". Note that there are now 3 users working on the project: admin and the two users who were just invited.</p>" +
        "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/14022012_Staging_Sandbox.png\" alt=\"Staging Sandbox\" /></div>" +
        "<p align=\"center\">Staging Sandbox</p><p>Log off as admin and log in as the Content Reviewer. Navigate to Company Home -> Web Projects, select the web project where user was invited. Note that the “Web Forms” section is not present in the user sandbox, preventing him from adding or modifying content to the web project.</p>" +
        "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/14022012_Hosting_Items_on_the_Website.png\" alt=\"Hosting Items on the Website\" /></div>" +
        "<p align=\"center\">No Modified Items</p><p>Log off and log on as the Content Contributor. Navigate to the same project and verify that the “Web Forms” section is present in the sandbox and the user can add and modify content.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_14022012_k = "Alfresco CMS Software development restrict access to content";
        public const string content_14022012_d = "Restricting Access to Content in Alfresco";

        //"Assigning a Workflow to the Web Form"
        public const string content_12022012_b = "<p>Alfresco includes two types of workflow out of the box. The Simple Workflow is content-oriented, and the Advanced Workflow is task-oriented. Out of the box, the Simple Workflow has only two steps, one for approval and one for rejection.</p>";
        public const string content_12022012_r = "<p>Here is one way to assign a Simple Workflow to the Web Form, if it is associated with a Web Project.</p><p>Navigate to Company Home -> Web Projects, select the web project and choose Actions -> Edit Web Project Settings in the top right part of the screen. Select \"Next\" until you get to the \"Configure Web Forms\" screen. If the web form does not have a workflow associated with it, a small exclamation mark will be displayed.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/12022012_Select_Web_Content_Forms.png\" alt=\"Select Web Content Forms\" /></div>" +
            "<p align=\"center\">Workflow not Configured</p><p>Select \"Configure Workflow\". On the \"Configure Workflow\", use \"Search\" option to display users. Choose a user which will be reviewing and approving content and select \"Add to List\".</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/12022012_Configure_Workflow.png\" alt=\"Configure Workflow\" /></div>" +
            "<p align=\"center\">Add Users to Workflow</p><p>Click \"OK\" on the right to complete the workflow configuration. The exclamation mark should no longer be displayed. Select \"Next\" to get to \"Configure Workflow\" screen. There should only be one workflow available, \"Web Site Submission\". Select \"Add to list\". Once again, the exclamation mark will be displayed.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/12022012_Edit_Web_Project_Wizard.png\" alt=\"Edit Web Project Wizard\" /></div>" +
            "<p align=\"center\">Configure Web Project Workflow</p><p>Use the \"Configure Workflow\" in the same way as was done for a web form. Select \"Finish\" to save changes. Now it is time to create some content and check what the workflow does. On the Web Project screen, select \"Create Content\" against the web form for which the workflow was just configured. Create some content and on the last screen check the \"Submit these X files when wizard finishes\". Select \"Finish\". (I'm not exactly sure if that's the correct way to go - did I have to configure workflow twice, first on the web form and then on the web project? Maybe not. I think, the way to avoid it is to add workflow at the time of creation, both on a project and a form. That would likely be the best practice.)</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/12022012_Create_Web_Content_Wizard.png\" alt=\"Create Web Content Wizard\" /></div>" +
            "<p align=\"center\">Submit Items When Wizard Finishes</p><p>Now the wizard asks to provide some more details, so fill in the Label and Description for the items being submitted. Select \"OK\" to submit.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/12022012_Submit_Items.png\" alt=\"Submit Items\" /></div>" +
            "<p align=\"center\">Submit Items</p><p>Next, log out from the user who created content and log in as a user who was specified as a Content Manager. \"My Alfresco Dashboard\" will be displayed, where under \"My Tasks To Do\" the task to review the submission should appear. Select \"Manage Task\" to review the submission.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/12022012_My_Alfresco_Dashboard.png\" alt=\"My Alfresco Dashboard\" /></div>" +
            "<p align=\"center\">My Tasks To Do</p><p>Let's first reject the submission. Provide the user with some helpful comment and select \"Reject\" on the right.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/12022012_Manage_Task_Review.png\" alt=\"Manage Task Review\" /></div>" +
            "<p align=\"center\">Review Workflow Task</p><p>Now you should be back on the \"My Alfresco Dashboard\" and you can notice that there are no more entries under \"My Tasks To Do\". This means that the hard job of the content reviewer is done for now. Log off as this user and log back on as the content creator. Now you'll be on the \"My Alfresco Dashboard\" and you'll see that you have a task to do, which has a \"Rejected\" type. Use \"Manage Task\" to fix this! On the \"Manage Task\" screen you can see joe's comment, so select \"Edit\" icon to modify the content. Make the changes and save them. Now on the \"Manage Task\" screen, select \"Resubmit for Review\" on the right. Logout as this user and log back on as the content reviewer. There's a task now in the \"My Tasks To Do\", with a small (2), which tells me how many times the content was submitted. Choose \"Manage Task\".</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/12022012_My_Tasks_to_do.png\" alt=\"My Tasks to do\" /></div>" +
            "<p align=\"center\">My Tasks To Do - 2</p><p>Now the content looks better (let's at least assume that) and the reviewer selects \"Approve\". Again, the task is gone - log out one last time and log back on as the content creator. There are no more tasks too, but navigate to the Company Home -> Web Projects, select the web project and expand \"Recent Snapshots\". Now there is the content there which can be deployed, as described in a previous post - feel free to deploy if you wish.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/12022012_Hosting_Items_on_the_Website.png\" alt=\"Hosting Items on the Website\" /></div>" +
            "<p align=\"center\">Recent Snapshots</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_12022012_k = "Alfresco CMS Software development assign workflow to webform";
        public const string content_12022012_d = "Assigning a Workflow to the Web Form";

        //"Simple Reporting with Alfresco"
        public const string content_09022012_b = "<p>Basic static reports can be generated in Alfresco by using XSLT (or FreeMarker) to build the static pages. As an example, I have created a simple list of all items generated by a web form. One approach would be to create a separate web form for this task. In this case a list can be generated an any time by using this form to create content. The approach I used requires slightly less work (no new web form is created), but also slightly less flexibility (an index is created only when a new item is created). For a start, I created a web form that adds simple items, using the following XSL file:</p>";
        public const string content_09022012_r = "<pre class=\"brush: xml\">" +
            @"&lt;?xml version=""1.0"" encoding=""UTF-8""?&gt;<br />&lt;xsl:stylesheet version=""1.0"" xmlns:xhtml=""http://www.w3.org/1999/xhtml""<br /> xmlns:xsl=""http://www.w3.org/1999/XSL/Transform""<br /> xmlns:fn=""http://www.w3.org/2005/02/xpath-functions""<br /> exclude-result-prefixes=""xhtml""&gt;<br /><br /> &lt;xsl:output method=""html"" encoding=""UTF-8"" indent=""yes"" doctype-public=""-//W3C//DTD XHTML 1.0 Transitional//EN"" doctype-system=""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"" /&gt;<br /><br /> &lt;xsl:template match=""/""&gt;<br />  &lt;html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en""&gt;<br />  &lt;head&gt;<br />  &lt;/head&gt;<br />  &lt;body&gt;<br />   &lt;div id=""main_content""&gt;<br />    &lt;h1&gt;Item ID: &lt;xsl:value-of select=""/item/id""/&gt;&lt;/h1&gt;  <br />    &lt;p&gt;Item Name: &lt;xsl:value-of select=""/item/name""/&gt;&lt;/p&gt;<br />    &lt;p&gt;Expiry Date:&lt;xsl:value-of select=""/item/expiryDate""/&gt;&lt;/p&gt;<br />   &lt;/div&gt;<br />  &lt;/body&gt;<br />  &lt;/html&gt;<br /> &lt;/xsl:template&gt;<br />&lt;/xsl:stylesheet&gt;" + "</pre><p>Next step is to create an XSL template to redner an HTML list of items. This is the simplest I could come up with:</p><pre class=\"brush: xml\">" +
            @"&lt;?xml version=""1.0"" encoding=""UTF-8""?&gt;<br />&lt;xsl:stylesheet version=""1.0"" xmlns:xhtml=""http://www.w3.org/1999/xhtml""<br /> xmlns:xsl=""http://www.w3.org/1999/XSL/Transform""<br /> xmlns:fn=""http://www.w3.org/2005/02/xpath-functions""<br /> exclude-result-prefixes=""xhtml""&gt;<br /><br />&lt;xsl:output method=""html"" encoding=""UTF-8"" indent=""yes"" doctype-public=""-//W3C//DTD XHTML 1.0 Transitional//EN"" doctype-system=""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"" /&gt;<br /><br />&lt;xsl:template match=""/""&gt;<br /><br />&lt;xsl:variable name=""itemList"" select=""alf:parseXMLDocuments('item', '/items')""/&gt;<br /><br /> &lt;html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en""&gt;<br /> &lt;head&gt;<br /> &lt;/head&gt;<br /> &lt;body&gt;<br />  &lt;ul&gt;<br />   &lt;xsl:for-each select=""$itemList""&gt;<br />    &lt;xsl:variable name=""selectedVar""&gt;<br />     &lt;xsl:choose&gt;<br />     &lt;xsl:when test=""position() = 1""&gt;selected&lt;/xsl:when&gt;<br />     &lt;xsl:otherwise&gt;leaf&lt;/xsl:otherwise&gt;<br />     &lt;/xsl:choose&gt;<br />    &lt;/xsl:variable&gt;<br />    &lt;li class=""{$selectedVar}""&gt;<br />     &lt;xsl:variable name=""fileNameFixed""&gt;<br />      &lt;xsl:call-template name=""fixFileName""&gt;<br />       &lt;xsl:with-param name=""fileName""&gt;<br />        &lt;xsl:value-of select=""@alf:file_name"" /&gt;<br />       &lt;/xsl:with-param&gt;<br />      &lt;/xsl:call-template&gt;<br />     &lt;/xsl:variable&gt;<br />     &lt;a href=""{$fileNameFixed}""&gt;&lt;xsl:value-of select=""id"" /&gt;&lt;span&gt;&nbsp;&lt;xsl:value-of select=""name"" /&gt;&lt;/span&gt;&lt;/a&gt;<br />    &lt;/li&gt;<br />   &lt;/xsl:for-each&gt;<br />  &lt;/ul&gt;<br /> &lt;/body&gt;<br /> &lt;/html&gt;<br />&lt;/xsl:template&gt;<br /><br />&lt;xsl:template name=""fixFileName""&gt;<br /> &lt;xsl:param name=""fileName"" /&gt;<br />  &lt;xsl:value-of select=""concat(substring-before($fileName, '.xml'), '.html')"" /&gt;<br />&lt;/xsl:template&gt;<br />&lt;/xsl:stylesheet&gt;" +
            "</pre><p>The template calls parseXMLDocuments function of AVMRemote API to get a list of items. Note the parameters. First parameter is the name of the web form that was used to generate items. If the parameter does not match to the web form name exactly, it is likely that an empty list will be generated. I spent some time struggling with that! The second parameter is just the location where to get items from. As I understand, it is optional and the whole repository may be searched if the parameter is omitted. Next the template goes through each item and generates a link to it. The fixFileName function only replaces the file extension.</p><p>When the template is complete, navigate to Data Dictionary > Web Forms > Edit Web Form (for the desired web form). Select \"Next\" to get to the Configure Templates dialog. Locate the saved template and set the correct output path, i.e. “/${webapp}/items/index.html”. Select \"Add to list\", then \"Next\" and \"Finish\". As the result, the web form will create an \"Item\" each time it is used. Also, it will recreate the \"index.html\" page, which will now contain the newly added item.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/09022012_Configure_Templates.png\" alt=\"Configure Templates\" /></div><p>This is the sample HTML generated by the web form.</p><pre class=\"brush: xml\">" +
            @"&lt;!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd""&gt;<br />&lt;html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:fn=""http://www.w3.org/2005/02/xpath-functions"" xml:lang=""en"" lang=""en""&gt;<br /> &lt;head /&gt;<br /> &lt;body&gt;<br />  &lt;ul&gt;<br />   &lt;li class=""selected""&gt;&lt;a href=""Item7.html""&gt;7&lt;span&gt;amazing achievement&lt;/span&gt;&lt;/a&gt;&lt;/li&gt;<br />   &lt;li class=""leaf""&gt;&lt;a href=""Item6.html""&gt;6&lt;span&gt;a real item&lt;/span&gt;&lt;/a&gt;&lt;/li&gt;<br />   &lt;li class=""leaf""&gt;&lt;a href=""item8.html""&gt;8&lt;span&gt;Improvement&lt;/span&gt;&lt;/a&gt;&lt;/li&gt;<br />  &lt;/ul&gt;<br /> &lt;/body&gt;<br />&lt;/html&gt;<br />" +
            "</pre><p>This is the simples example I managed to come up with. Another reporting option, which is more advanced, is to writh web scripts to define a REST API for the Alfresco database content. Then the API is used to create, read and delete data in the backend repository and return responses in HTML, XML or JSON.</p><p><strong>Reference:</strong></p><p><a href=\"http://www.amazon.com/Alfresco-Developer-Guide-ebook/dp/B0058TIC76/\">Alfresco Developer Guide</a></p><p><a href=\"http://wiki.alfresco.com/wiki/WCM_roles\">Web Content Management Roles</a></p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_09022012_k = "Alfresco CMS Software development simple reporting";
        public const string content_09022012_d = "Simple Reporting with Alfresco";

        //"Web Form and Web Project with Alfresco"
        public const string content_06022012_b = "<p>Alfresco uses the Web Forms to capture content and store it as XML in the repository. Web forms allow creation of structured XML based on the schema definition (XSD). An XSLT can be associated with the Web Form to convert Alfresco web form XML into other formats. Creating a web form is a good starting point for creating web content.</p><p>Run Alfresco Explorer, login as admin and navigate to Company Home > Data Dictionary > Web Forms. Select Create Web Form from the Create menu. The Create Web Form Wizard will start.</p>";
        public const string content_06022012_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/06022012_Web_Forms.png\" alt=\"Web Forms\" /></div>" +
            "<p>I'll use this web form to create \"items\". The item has an id, a name and an expiry date. I prepared a simple XML schema definition that I will provide to the web form so it could later give me a user friendly way to create content.</p><pre class=\"brush: xml\">" +
            @"&lt;?xml version=""1.0""?&gt;<br />&lt;xs:schema xmlns:xs=""http://www.w3.org/2001/XMLSchema""<br /> xmlns:alf=""http://www.alfresco.org""<br /> elementFormDefault=""qualified""&gt;<br /><br /> &lt;xs:element name=""item""&gt;<br />  &lt;xs:complexType&gt;<br />   &lt;xs:sequence&gt;<br />    &lt;xs:element name=""id"" type=""xs:integer""/&gt;<br />    &lt;xs:element name=""name"" type=""xs:normalizedString""/&gt;<br />    &lt;xs:element name=""expiryDate"" type=""xs:date""/&gt;<br />   &lt;/xs:sequence&gt;<br />  &lt;/xs:complexType&gt;<br /> &lt;/xs:element&gt;<br />&lt;/xs:schema&gt;" +
            "</pre><p>I'm providing this schema on the Web Form Details. The \"Output path pattern\" defines where the items will be created in the project structure. /${webapp}/items/${name}.xml will create xml files in the \"items\" folder.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/06022012_Create_Web_Form_Wizard.png\" alt=\"Create Web Form Wizard\" /></div>" +
            "<p>Alfresco stores the item details in the xml form, but can easily render it into html if I provide a template. I prepared a very simple template.</p><pre class=\"brush: xml\">" +
            @"&lt;?xml version=""1.0"" encoding=""UTF-8""?&gt;<br />&lt;xsl:stylesheet version=""1.0"" xmlns:xhtml=""http://www.w3.org/1999/xhtml""<br /> xmlns:xsl=""http://www.w3.org/1999/XSL/Transform""<br /> xmlns:pr=""http://www.my.com/corp/pr""<br /> xmlns:fn=""http://www.w3.org/2005/02/xpath-functions""<br /> exclude-result-prefixes=""xhtml pr fn""&gt;<br /><br /> &lt;xsl:output method=""html"" encoding=""UTF-8"" indent=""yes"" doctype-public=""-//W3C//DTD XHTML 1.0 Transitional//EN"" doctype-system=""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"" /&gt;<br /><br /> &lt;xsl:template match=""/""&gt;<br />  &lt;html xmlns=""http://www.w3.org/1999/xhtml"" xml:lang=""en"" lang=""en""&gt;<br />  &lt;head&gt;<br />  &lt;/head&gt;<br />  &lt;body&gt;<br />   &lt;div id=""main_content""&gt;<br />    &lt;h1&gt;Item ID: &lt;xsl:value-of select=""/item/id""/&gt;&lt;/h1&gt;  <br />    &lt;p&gt;Item Name: &lt;xsl:value-of select=""/item/name""/&gt;&lt;/p&gt;<br />    &lt;p&gt;Expiry Date:&lt;xsl:value-of select=""/item/expiryDate""/&gt;&lt;/p&gt;<br />   &lt;/div&gt;<br />  &lt;/body&gt;<br />  &lt;/html&gt;<br /> &lt;/xsl:template&gt;<br />&lt;/xsl:stylesheet&gt;" +
            "</pre><p>I could choose to store XML and HTML files in separate locations, but for now I would not bother. After filling in the details, I need to press \"Add to list\" so my template is saved with the form.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/06022012_Create_Web_Form_Wizard_2.png\" alt=\"Create Web Form Wizard\" /></div>" +
            "<p>I could create a workflow for review and approval of the changes, but for now I'll leave it till later and select \"No not now\" on the \"Configure Workflow\" screen. Last screen is just a review of the details and after selecting \"Finish\" my web form is created.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/06022012_Create_Items_for_Web_Project.png\" alt=\"Create Items for Web Project\" /></div>" +
            "<p>Next I need a web project that will utilize my form. Navigate to Company Home > Web Projects and select Create Web Project in the Create menu. The Create Web Project Wizard will start.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/06022012_Web_Projects.png\" alt=\"Web Projects\" /></div>" +
            "<p>Fill in the details, fields are really freetext and Use as a template is not required. In step two, choose \"Create a new empty Web Project\". In step three, add a deployment receiver. I will be using my local PC, hence the 'localhost'. I also found out that I can not make the receiver to work unless I use port 50500 and specify a Target Name of 'avm'.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/06022012_Deployment_Receivers.png\" alt=\"Deployment Receivers\" /></div>" +
            "<p>In step four, there should be at least one web form listed in \"Select Web Forms\" - the one created earlier. Select \"Add to list\" to associate the web form to the web project. Further steps are about workflow and users - something that can be configured later. After the last step, select \"Finish\" and the web project is created.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/06022012_Hosting_Items_on_the_Web_Site.png\" alt=\"Hosting Items on the Web Site\" /></div>" +
            "<p>All is ready to generate some content for the project. Click on the name of the project and you will see the sandboxes for the project. There should be at least two: Local sandbox of the user who's logged in and the Staging Sandbox. The user sandbox should now have a web form which has an action \"Create Content\". That's what I'll do.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/06022012_Hosting_Items_on_the_Web_Site_2.png\" alt=\"Hosting Items on the Web Site\" /></div>" +
            "<p>I'll create my first item, so I'll call it item1 and specify Type as Content and Content Type as xml. Next step gives me a way to enter content details - based on my XSD, Alfresco gives me a smaller box to enter an integer, a longer box to enter a string and a date picker for a date. How cute.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/06022012_Author_Web_Content.png\" alt=\"Author Web Content\" /></div>" +
            "<p>When I'm done, item1.xml and item1.html are created. I can view them if I choose \"Browse Website\" and navigate to \"items\" folder.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/06022012_Browse_Files.png\" alt=\"Browse Files\" /></div>" +
            "<p>Here's how my XML and HTML files look like - more or less the way I expected them to be.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/06022012_Browse_Files_XML.png\" alt=\"Browse Files XML\" /></div>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/06022012_Item.png\" alt=\"Item\" /></div>" +
            "<br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_06022012_k = "Alfresco CMS Software development web form web project";
        public const string content_06022012_d = "Web Form and Web Project with Alfresco";

        //"Installing Alfresco"
        public const string content_03022012_b = "<p>A fully functional trial version of Alfresco can be downloaded from the company website. Somewhat confusingly, there are two separate links, for <a href=\"http://www.alfresco.com/products/document-management/\">Document Management</a> and for <a href=\"http://www.alfresco.com/products/web-content-management/\">Web Content Management</a>, but the downloads seem to be identical. It appears that the difference may be in the modules that are installed if \"Easy\" option is used.</p>";
        public const string content_03022012_r = "<p>Additionally, the Community version of the product is freely available. Both Community and Enterprise versions are Open Source, but Enterprise also offers support and certain extensions. There is a comparison of the functionality, but it is fairly high-level.</p><p>I'm using a local PC as both a server and a client for my evaluation experiments. The installation process was fairly straightforward.</p><ul><li>Select language</li><li>Select \"Advanced\" installation type</li><li>Check all boxes</li><li>Leave all the settings default on next screens up to the admin password screen</li><li>Create admin password</li><li>Leave all the settings default on next screens up to the service startup configuration</li><li>Select \"Auto\" option – configure servers to start automatically</li><li>Wait for the installation to complete</li></ul><br /><br />" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/03022012_Setup.png\" alt=\"Setup\" /></div>" +
            "<p style=\"text-align: center\">\"Advanced Install\" options</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/03022012_Alfresco_Share.png\" alt=\"Alfresco Share\" /></div>" +
            "<p style=\"text-align: center\">Alfresco Share</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/03022012_Company_Home.png\" alt=\"Company Home\" /></div>" +
            "<p style=\"text-align: center\">Alfresco Explorer</p><p>After installation, the Alfresco Share is located at <u>http://127.0.0.1:8080/share/page/user/admin/dashboard</u> and Alfresco Explorer at <u>http://127.0.0.1:8080/alfresco</u>.</p><p>Share and Explorer is a separate topic, which I do not yet understand thoroughly - the Explorer provides means to create, modify and publish content. All my experiments so far have been done using Explorer. The purpose of Alfresco Share appears to be team collaboration so it may become useful when (and if) at some stage more than one person will be working on the content.</p><p><strong>References:</strong></p><a href=\"http://www.scribd.com/doc/77087023/Open-Source-Web-Content-Management-in-Alfresco\">Open Source Web Content Management in Alfresco</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_03022012_k = "Alfresco CMS Software development install";
        public const string content_03022012_d = "Installing Alfresco";

        //"Understanding Alfresco"
        public const string content_02022012_b = "<p>I've just started at the new role and the first 'real' task I've been given was to identify if the Content Management System is suitable for storing some data which was previously stored in the database. This is quite a new concept for me and there are a lot of questions to answer. User friendly display? Editing and saving? Versioning? Approval? Trail of changes? Referential integrity?</p>";
        public const string content_02022012_r = "<p>What I need out of an CMS is</p><ul><li>Ability to store data in XML form</li><li>Ability to display data in a user-friendly manner</li><li>Ability to edit and save data</li><li>Ability to apply scripts or rules on check-in of data</li><li>Keeping an audit trail of changes made to data</li><li>Version control and options available to keep release history and versioning of documents</li><li>Ability to keep multiple edits or branches of documents</li><li>Ability to extract data</li><li>Ability to apply a workflow to a document such as make a review mandatory before the document is released</li></ul><p>As an example of a CMS, we chose <a href=\"http://www.alfresco.com\">Alfresco</a>. Alfresco is an enterprise content management system that includes content repository, an out-of-the-box web portal framework, a CIFS interface that provides file system compatibility, a web content management system and a workflow.</p><p>There are two different repository implementations within Alfresco. They are DM store and WCM store (Data Management and Web Content Management). They are not equivalent in terms of functionality.</p><p>WCM is built on top of the core products and adds some functionality. However, it was developed in such way that it created a separation between DM and WCM from an API perspective, so WCM is not always a subset of DM functionality. Some features are only available in WCM.</p><p>Main differences:</p><ul><li>DM uses a proprietary XML-based description of the content model, while WCM uses XML schema. Main idea – not to have a mix between two models.</li><li>Data entered into a WCM web form is saved as XML that conforms to the XSD defined by the user. There is no similar facility for capturing data as XML within DM.</li><li>XSLT (or Freemaker) can be used to transform WCM data into other formats. In DM, no such functionality exist out-of-the-box.</li><li>WCM does not allow configuration of rules (trigger actions against newly-added, updated or deleted documents).</li><li>DM allows assigning users and groups to roles at folder and file level. WCM only allows doing that on a project level, no lower, but can be implemented by writing custom code using API.</li></ul><p>General advice on choosing between DM and WCM:</p><p>Choose WCM if:</p><ul><li>Authors are comfortable with Alfresco Web Client and Web Forms</li><li>Minimal time to be spent on presentation tier</li><li>Need the ability to rollback change sets</li><li>Only need to link content to basic URLs</li><li>Need to have a staging environment</li><li>Simple workflow is sufficient</li></ul><p>Choose DM if:</p><ul><li>Workflow must be exposed via the portal</li><li>Developers are experienced with XML/XSLT, WYSIWYG editors and jBPM</li><li>Can spend time upfront designing a robust presentation tier</li><li>Custom solution is desired and the overall solution is simple</li></ul><p>References</p><a href=\"http://ecmarchitect.com/archives/2009/08/31/1038\">Understanding the differences between Alfresco repository implementations</a><br><a href=\"http://blogs.captechconsulting.com/blog/philip-kedy/alfresco-wcm-or-dm-what-the-best-choice-your-enterprise-portal\">Alfresco WCM or DM: What is the best choice for your enterprise portal?</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_02022012_k = "Alfresco CMS Software development";
        public const string content_02022012_d = "Understanding Alfresco";

        //"TFS Is Back : Migrate from VSS to TFS 2010"
        public const string content_09012012_b = "<p>With Team Foundation Server 2010, Microsoft still did not provide a fully automated tool for migration from Visual SourceSafe.</p><p>In fact, the MSDN guide on migration from Visual SourceSafe is about 14 screens long on my monitor and does not cover in much detail some related actions such as backing up and restoring single projects from a SourceSafe database or creating a new Team Project, only providing links to other MSDN guides and articles.</p><p>We have a huge Visual SourceSafe database here which, quite frankly, may fall over any time due to sheer size or the fact that it has not been analysed for a few years. There is no way it is going to be converted to TFS fully, as it holds heaps of stuff that just does not belong there. So each developer will probably move projects he or she is responsible for.</p>";
        public const string content_09012012_r = "<p>Below is my take on a complete instruction for migrating a project from Visual SourceSafe to TFS 2010, using Visual Studio 2010, that I prepared for other developers.</p><p style=\"font-size:5;font-weight:bold\">1. Before you begin</p><p style=\"font-size:4;font-weight:bold\">1.1. Machine configuration</p><p><ul><li>Install Visual Studio on the machine that will perform the migration</li><li>Install Visual SourceSafe 2005</li><li>Install the <a href=\"http://archive.msdn.microsoft.com/KB950185/Release/ProjectReleases.aspx?ReleaseId=1123\">update that is associated with article 950185 in Microsoft Knowledge Base</a></li><li>Make sure you know the SourceSafe database admin password</li></ul></p><p style=\"font-size:4;font-weight:bold\">1.2. Permissions</p><p><ul><li>Know the Admin password to the Visual SourceSafe database</li><li>Be an Administrator of your PC</li><li>Be a member of TFS Server Administrators or Collection Administrators</li></ul></p><p style=\"font-size:5;font-weight:bold\">2. Backup and restore SourceSafe project</p><p style=\"font-size:4;font-weight:bold\">2.1. Backup SourceSafe project</p><p style=\"font-size:3;font-style:italic;font-weight:bold\">2.1.1. Use the command line tool</p><p>For this example, the \\Server\\VSS\\database\\VSS-DB SourceSafe database and $/MyRoot/MyProject projects were used.</p><p>The backup and restore tools, ssarc and ssrestor are located in the SourceSafe folder, usually C:\\Program Files\\Microsoft Visual SourceSafe. From the command prompt, navigate to that folder and run the command</p><p><strong>ssarc -d- -i -yadmin,password -s\\Server\\VSS\\database\\VSS-DB CodeProject.ssa \"$/MyRoot/MyProject\"</strong> (replace password with SourceSafe admin password, and specify the desired SourceSafe database location and backup database name).</p><p>If the warning \"For reliability and performance reasons using the analyze, archive and restore utilities over network is not recommended. Do you want to continue? (Y/N)\", select Y.</p><p>The command will create a backup database CodeProject.ssa in the Visual SourceSafe folder.</p><p style=\"font-size:3;font-style:italic;font-weight:bold\">2.1.2. Use the Visual SourceSafe Administration UI</p><ul><li>Run Start &rarr; Programs &rarr; Microsoft Visual SourceSafe &rarr; Microsoft Visual SourceSafe Administration</li><li>Select Archive &rarr; Archive Projects from menu</li><li>Select a project to back up</li><li>Select \"Next\"</li><li>Select \"Save Data to File\", point to the location of the file</li><li>Select \"Next\"</li><li>Select \"Archive all of the data\"</li><li>Select \"Finish\"</li><li>The message box \"Archive/restore successfully completed\" will appear</li></ul>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/09012012_Archive_Wizard.png\" alt=\"Archive Wizard\" /></div>" +
            "<p style=\"font-size:4;font-weight:bold\">2.2. Create an empty local SourceSafe database</p><ul><li>Create a folder for a new SourceSafe database (i.e. \"D:\\VSSTFSTransfer\")</li><li>Start Visual SourceSafe 2005</li><li>Select File &rarr; Open SourceSafe database</li><li>Choose \"Add\"</li><li>\"Add SourceSafe Database\" wizard will start</li><li>Select \"Next\"</li><li>Select \"Create a New Database\"</li><li>Select \"Next\"</li><li>Browse to the folder you created, select it and select \"Next\"</li><li>Specify the name for the database (can use default) and select \"Next\"</li><li>Leave default Team Version Control model and select \"Next\"</li><li>Choose \"Finish\"</li><li>Close Visual SourceSafe 2005</li></ul>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/09012012_Add_Source_Safe_Database.png\" alt=\"Add Source Safe Database\" /></div>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/09012012_Add_Source_Safe_Database_Wizard.png\" alt=\"Add Source Safe Database Wizard\" /></div>" +
            "<p style=\"font-size:4;font-weight:bold\">2.3. Restore a project to an empty SourceSafe database</p><p style=\"font-size:3;font-style:italic;font-weight:bold\">2.3.1. Use the command line tool</p><p>From the command prompt, navigate to Visual SourceSafe folder and run the command</p><p><strong>ssrestor \"-p$/MyRoot/MyProject\" -sD:\\VSSTFSTransfer -yadmin,password CodeProject.ssa \"$/MyRoot/MyProject\"</strong></p><p>The command will restore the project into the SourceSafe database located in D:\\VSSTFSTransfer.</p><p>Open the database with Visual SourceSafe to verify files were restored.</p><p style=\"font-size:3;font-style:italic;font-weight:bold\">2.3.2. Use the Visual SourceSafe Administration UI</p><ul><li>Run Start &rarr; Programs &rarr; Microsoft Visual SourceSafe &rarr; Microsoft Visual SourceSafe Administration</li><li>Select Archive &rarr; Restore Projects</li><li>Point to the location where the backup database was saved</li><li>Select \"Next\"</li><li>Click \"OK\" on the warning</li><li>Select the project to restore</li><li>Select \"Next\"</li><li>Leave the default choice \"Restore to the project the item was archived from\"</li><li>Select \"Finish\"</li><li>Click \"OK\" on the warning</li><li>The message box \"Archive/restore successfully completed\" will show</li></ul>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/09012012_Restore_Wizard.png\" alt=\"Restore Wizard\" /></div>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/09012012_Destination_is_not_the_same.png\" alt=\"Destination is not the same\" /></div>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/09012012_Restore_Wizard_2.png\" alt=\"Restore Wizard\" /></div>" +
            "<p style=\"font-size:5;font-weight:bold\">3. Create a new Team Project</p><p>In Visual Studio 2010 open the \"Team Explorer\" tab. If you are not connected to the server, select \"Connect to Team Project\". If the server is not in the list, select \"Servers\". On \"Add/Remove Team Foundation Server\" select \"Add\". Fill in the TFS credentials.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/09012012_Add_Team_Foundation_Server.png\" alt=\"Add Team Foundation Server\" /></div>" +
            "<p>On the \"Connect to Team Project\" screen choose the collection and project(s) to connect to and select \"Connect\".</p><p>In the \"Team Explorer\" tab, right-click the TFS server and select \"New Team Project\". Specify the name of the project and select \"Next\". Choose the process template and select \"Next\". Specify source control settings (\"Create an empty source control folder\" for a conversion project) and select \"Finish\". Visual Studio will create a new Team Project.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/09012012_New_Team_Project.png\" alt=\"New Team Project Server\" /></div>" +
            "<p style=\"font-size:5;font-weight:bold\">4. Analyse SourceSafe database</p><p>Before migrating the data, Visual SourceSafe Converter tool should be used to analyse any issues with the data.</p><p style=\"font-size:3;font-style:italic;font-weight:bold\">4.1.1. Create a settings file</p><p>A settings file to use for the example above:</p><pre class=\"brush:xml\">" +
            @"&lt;?xml version=""1.0"" encoding=""utf-8""?&gt;<br />&lt;SourceControlConverter&gt;<br /> &lt;ConverterSpecificSetting&gt;<br />  &lt;Source name=""VSS""&gt;<br />   &lt;VSSDatabase name=""D:\VSSTFSTransfer""&gt;&lt;/VSSDatabase&gt;<br />   &lt;UserMap name=""D:\VSSTFSTransfer\UserMap.xml""&gt;&lt;/UserMap&gt;<br />  &lt;/Source&gt;<br />  &lt;ProjectMap&gt;<br />   &lt;Project Source=""$/MyRoot/MyProject/""&gt;&lt;/Project&gt;<br />  &lt;/ProjectMap&gt;<br /> &lt;/ConverterSpecificSetting&gt;<br /> &lt;Settings&gt;<br />   &lt;Output file=""D:\VSSTFSTransfer\Analysis.xml""&gt;&lt;/Output&gt;<br /> &lt;/Settings&gt;<br />&lt;/SourceControlConverter&gt;" + "</pre><p>VSSDatabase is the path to the SourceSafe database to be converted.</p><p>UserMap is the path to the user map file to be created</p><p>ProjectMap lists all the projects to be converted. To convert the whole SourceSafe database, use the following:</p><pre class=\"brush:xml\">" +
            @"&lt;ProjectMap&gt;<br /> &lt;Project From=""$/""&gt;&lt;/Project&gt;<br />&lt;/ProjectMap&gt;" + "</pre><p>Output file is the result of the analysis report.</p><p style=\"font-size:3;font-style:italic;font-weight:bold\">4.1.2. Analyse the data</p><p>The VSSConverter utility is located in the Visual Studio 2010 Common folder, usually C:\\Program Files\\Microsoft Visual Studio 10.0\\Common7\\IDE</p><p>Save the settings file as settings.xml and run VSSConverter in the analysis mode, specifying the path to the settings file, i.e.</p><p><strong>VSSConverter Analyze D:\\VSSTFSTransfer\\settings.xml</strong></p><p>Enter the SourceSafe database password. If the process completes successfully, the Analysis.xml and UserMap.xml will be created in the D:\\VSSTFSTransfer folder. Review and resolve issues found by the Analyze feature, if applicable.</p><p style=\"font-size:3;font-style:italic;font-weight:bold\">4.1.3. Create a migration settings file</p><p>Modify the UserMap.xml. Here is the sample user map which is the output from the previous step:</p><pre class=\"brush:xml\">" +
            @"&lt;?xml version=""1.0"" encoding=""utf-8""?&gt;<br />&lt;UserMappings xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""&gt;<br />  &lt;UserMap From=""Admin"" To="""" /&gt;<br />  &lt;UserMap From=""abc123"" To="""" /&gt;<br />  &lt;UserMap From=""def456"" To="""" /&gt;<br />  &lt;UserMap From=""ghi789"" To="""" /&gt;<br />&lt;/UserMappings&gt;" + "</pre><p>For correct history migration, provide the \"To\" users in the user map. These are the valid Team Foundation Server users. Example:</p><pre class=\"brush:xml\">" +
            @"&lt;?xml version=""1.0"" encoding=""utf-8""?&gt;<br />&lt;UserMappings xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema""&gt;<br />  &lt;UserMap From=""Admin"" To=""MYDOMAIN\administrator"" /&gt;<br />  &lt;UserMap From=""abc123"" To=""MYDOMAIN\abc123"" /&gt;<br />  &lt;UserMap From=""def456"" To=""MYDOMAIN\def456"" /&gt;<br />  &lt;UserMap From=""ghi789"" To=""MYDOMAIN\ghi789"" /&gt;<br />&lt;/UserMappings&gt;" + "</pre><p>Modify the settings file for migration.</p><pre class=\"brush:xml\">" +
            @"&lt;?xml version=""1.0"" encoding=""utf-8""?&gt;<br />&lt;SourceControlConverter&gt;<br /> &lt;ConverterSpecificSetting&gt;<br />  &lt;Source name=""VSS""&gt;<br />   &lt;VSSDatabase name=""D:\VSSTFSTransfer""&gt;&lt;/VSSDatabase&gt;<br />   &lt;UserMap name=""D:\VSSTFSTransfer\UserMap.xml""&gt;&lt;/UserMap&gt;<br />  &lt;/Source&gt;<br />  &lt;ProjectMap&gt;<br />   &lt;Project Source=""$/MyRoot/MyProject"" Destination=""$/MyProject""&gt;&lt;/Project&gt;<br />  &lt;/ProjectMap&gt;<br /> &lt;/ConverterSpecificSetting&gt;<br /> &lt;Settings&gt;<br />  &lt;TeamFoundationServer name=""MYTFServer"" port=""8080"" protocol=""http"" collection=""tfs/MyCol""&gt;&lt;/TeamFoundationServer &gt;<br />  &lt;Output file=""D:\VSSTFSTransfer\Migrate.xml""&gt;&lt;/Output&gt;<br /> &lt;/Settings&gt;<br />&lt;/SourceControlConverter&gt;" + "</pre><p>The Project Destination is the project name in the TFS.</p><p>The TeamFoundationServer settings are set according to the TFS Setup as shown in the screenshot.</p><p>Specify a name for the migration file output.</p><p style=\"font-size:5;font-weight:bold\">5. Run the migration process</p><p style=\"font-size:4;font-weight:bold\">5.1. Run the process</p><p>Run the converter tool with the migrate attribute.</p><p><strong>VSSConverter Migrate D:\\VSSTFSTransfer\\settings.xml</strong></p><p style=\"font-size:4;font-weight:bold\">5.2. Analyse the output and resolve issues</p><p>Check the errors and warnings in the Migrate.xml which was created by the migration too.</p><p style=\"font-size:5;font-weight:bold\">6. If something went wrong</p><p>If you’re not happy with the results, you can always delete the project from TFS 2010. Use on your own risk! Check the parameters, make sure that the spelling is correct, make backups, check again and make a local backup of everything just in case.</p><p><strong>TFSDeleteproject /force /collection:http://MYTFServer:8080/tfs/MyCol MyProject</strong></p><p>Read the warning and decide. Press Y to continue. Press enter.</p><p>Now you can start from scratch.</p><p style=\"font-size:4;font-weight:bold\">7. References and further information</p><a href=\"http://msdn.microsoft.com/en-us/library/ms253060.aspx\">Migrate from Visual SourceSafe</a><br><a href=\"http://msdn.microsoft.com/en-us/library/ms252587.aspx\">Team Foundation Server Permissions</a><br><a href=\"http://msdn.microsoft.com/en-us/library/ms252477.aspx\">Configuring Users, Groups, and Permissions</a><br><a href=\"http://msdn.microsoft.com/en-us/library/ms253090(VS.90).aspx\">VSSConverter Command-Line Utility for Source Control Migration</a><br><a href=\"http://msdn.microsoft.com/en-us/library/ms181482(v=VS.100).aspx\">TFSDeleteProject: Deleting Team Projects</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_09012012_k = "Migrating VSS TFS 2010";
        public const string content_09012012_d = "TFS Is Back : Migrate from VSS to TFS 2010";

        //"Learning MVC: SQL CE Membership Provider for ASP.Net"
        public const string content_15122011_b = "<p>I tried uploading my MVC application on the web server for the first time. That proved to be slightly more complicated compared to using a server in the local network. I chose www.discountasp.net as a provider due to several positive reviews I've found. Registration, of course, was easy, and accessing the \"control panel\" too. The machine I develop on, however, is behind the proxy and the corporate firewall, so for deployment I had to choose publish to local PC and then copy the files over to my home pc and then to the provider's ftp. Doing the application part was really easy - just copied the files to the ftp, deleted the default \"index.htm\" page, stopped and started the web site from the control panel, and I could see the application main page straight away. The troubles, as I expected, started with database. I tried modifying connection strings to point correctly to the corresponding folders on the host, but sometimes I got back the 500 error, and sometimes something slightly more meaningful like that one:</p>";
        public const string content_15122011_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/15122011_Server_Error.png\" alt=\"Server Error\" /></div>" +
            "<p>I was slowly going through the host's manuals and the knowledge base, but what solved all my issues was the <a href=\"http://sqlcemembership.codeplex.com/\">SQL Compact ASP.NET Membership, Role and Profile provider</a>. Essentially, that's just four files that need to be added to the project's App_Code folder. Next, the web.config file has to be modified according to instructions on <a href=\"http://sqlcemembership.codeplex.com/\">Codeplex</a> or in the <a href=\"http://erikej.blogspot.com/2010/08/sql-server-compact-40-aspnet-membership.html\">author's blog</a>.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/15122011_App_Code.png\" alt=\"App_Code\" /></div>" +
            "<p>And then it just works - no need for SQL Express, no additional configuration - just copied the files over to the host. Extremely positive experience.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_15122011_k = "Software development ASP.NET MVC sql ce membership provider";
        public const string content_15122011_d = "Learning MVC: SQL CE Membership Provider for ASP.Net";

        //"Learning MVC: User Data in a Partial View"
        public const string content_09122011_b = "<p>After spending some time trying to apply a somewhat decent layout to the site, I decided to put some common information in the sidebar. This information would be the same on every page, but will be visible only to logged in users. Initially I rendered it as a section, but that's clearly inefficient as the section tag has to be added to each and every view. That's against the DRY principle of MVC - Do not Repeat Yourself. Much more obvious decision is to place the common part into the partial view, and render this view right in the master page layout. I had to overcome a few obstacles, however, before I achieved the desired result. Here's the outcome:</p>";
        public const string content_09122011_r = "<p>First step was to create a partial view, which I called \"Stats.cshtml\" and placed under \"Shared\" folder together with other common views. Here is the initial Stats.cshtml:</p><pre class=\"brush:xml\">" +
            @"&lt;div id=""sidebar"" role=""complementary""&gt;<br />&lt;div class=""callout""&gt;<br /> &lt;h3&gt;Stats:&lt;/h3&gt;<br />&lt;/div&gt;<br />&lt;section class=""tbl""&gt;<br /> &lt;table cellspacing=""0""&gt;<br /> &lt;caption&gt;Your Things To Do&lt;/caption&gt;<br />  &lt;tr&gt;<br />   &lt;th scope=""col""&gt;<br />    In Basket Items:<br />   &lt;/th&gt;<br />   &lt;th scope=""col""&gt;<br />      <br />   &lt;/th&gt;<br />  &lt;/tr&gt;<br /> &lt;/table&gt;<br />&lt;/section&gt;<br />&lt;/div&gt;" + "</pre><p>Which can be rendered from the _layout.cshtml using several methods, such as Render or RenderPartial. More about which one to choose in references below. Let's say I use RenderPartial the following way:</p><pre class=\"brush:xml\">" +
            @"&lt;content&gt;<br />&lt;div id=""main"" role=""main""&gt;<br /> @RenderBody()<br />&lt;/div&gt;<br />    <br /> @if(Request.IsAuthenticated){<br />  Html.RenderPartial(""Stats"");<br /> }<br />&lt;/content&gt;" + "</pre><p>This solves the bit where the sidebar is rendered properly, and only visible to the logged in users. Now to the second part - how to display user-specific information? Currently the layout knows nothing about it. One of the ways is to create a controller for the partial view. I created a controller \"StatsController\" and placed some pretty basic code in it:</p><pre class=\"brush:csharp\">" +
            @"public class StatsController : Controller<br />{<br /> private modelGTDContainer db = new modelGTDContainer();<br /> private InBasketRepository repository = new InBasketRepository();<br /> //<br /> // GET: /Stats/<br /><br /> [Authorize]<br /> public PartialViewResult Stats(Users user)<br /> {<br />  var inbasket = repository.FindUserInBasketItems(user.UserID);<br />  return PartialView(inbasket.ToList());<br /> }<br />}" + "</pre><p>Note the use of PartialViewResult instead of the usual ViewResult. When I used a ViewResult I got a fairly nasty infinite loop. Now my RenderPartial is not so useful anymore, because it does not accept a controller as a parameter. So I have to change it to either Html.Action or Html.RenderAction. One of the overloads accepts a view name and a controller name, this is the one I need:</p><pre class=\"brush:xml\">" +
            @"&lt;content&gt;<br />&lt;div id=""main"" role=""main""&gt;<br /> @RenderBody()<br />&lt;/div&gt;<br />    <br /> @if(Request.IsAuthenticated){<br />  Html.RenderAction(""Stats"", ""Stats"");<br /> }<br />&lt;/content&gt;" + "</pre><p>And that's about it. The last bit is to specify the model in the Stats.cshtml and to pass some output to the page - at least a simple label:</p><pre class=\"brush:xml\">" +
            @"@model IEnumerable&lt;GTD.Models.InBasket&gt;<br />           <br />@if (Request.IsAuthenticated)<br />{<br />    &lt;div id=""sidebar"" role=""complementary""&gt;<br />    &lt;div class=""callout""&gt;<br />     &lt;h3&gt;Stats:&lt;/h3&gt;<br />    &lt;/div&gt;<br />    &lt;section class=""tbl""&gt;<br /> &lt;table cellspacing=""0""&gt;<br /> &lt;caption&gt;Your Things To Do&lt;/caption&gt;<br />    &lt;tr&gt;<br />        &lt;th scope=""col""&gt;<br />            In Basket Items:<br />        &lt;/th&gt;<br />        &lt;th scope=""col""&gt;<br />       @Html.Label(Model.Count().ToString())<br />           <br />        &lt;/th&gt;<br />    &lt;/tr&gt;<br />    &lt;/table&gt;<br />    &lt;/section&gt;<br />    &lt;/div&gt;<br />}" + "</pre><p>And that's what the user will see:</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/09122011_SideBar.png\" alt=\"SideBar\" /></div>" +
            "<p>References:</p><a href=\"http://www.arrangeactassert.com/when-to-use-html-renderpartial-and-html-renderaction-in-asp-net-mvc-razor-views/\">When to use Html.RenderPartial and Html.RenderAction in ASP.NET MVC Razor Views</a><br><a href=\"http://stackoverflow.com/questions/4692977/asp-net-mvc-3-layout-cshtml-controller\">ASP.NET MVC 3 _Layout.cshtml Controller</a><br><a href=\"http://forums.asp.net/t/1742396.aspx/1\">Passing data from View to Partial View</a><br><a href=\"http://stackoverflow.com/questions/4247950/asp-net-mvc3-razor-syntax-help-im-getting-stuck-in-an-infinite-loop\">ASP.NET MVC3 Razor syntax help - I'm getting stuck in an infinite loop</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_09122011_k = "Software development ASP.NET MVC user data in partial view";
        public const string content_09122011_d = "Learning MVC: User Data in a Partial View";

        //"Learning MVC: Scalable Navigation"
        public const string content_06122011_b = "<p>Today I tried applying a lesson from \"Bulletproof Web Design\" to scalable navigation. The goal, essentially is to avoid code wherever possible, avoid using images and allow the size of the tabs to be scalable.</p><p>The \"bulletproof\" approach, in short, is to use the nav element from HTML5 and wrap a list of tabs into it. Here's the whole of the HTML:</p>";
        public const string content_06122011_r = "<pre class=\"brush:xml\">" +
            @"&lt;nav role=""navigation""&gt;<br /> &lt;ul id=""menu""&gt;<br />  &lt;li id=""nav-home""&gt;@Html.ActionLink(""Home"", ""Index"", ""Home"")&lt;/li&gt;<br />  &lt;li id=""nav-about""&gt;@Html.ActionLink(""About"", ""About"", ""Home"")&lt;/li&gt;<br /> &lt;/ul&gt;<br />&lt;/nav&gt;" + "</pre><p>And here's the css that I applied following the book (and had to adjust some things here and there):</p><pre class=\"brush:css\">" +
            @"nav[role=""navigation""] <br />{ <br />  display: block;<br />  margin: 0; <br />  padding: 10px 0 0 0px; <br />  list-style: none; <br />  background: #FFCB2D;<br />}<br /><br />nav[role=""navigation""] li <br />{ <br />  float: left; <br />  margin: 0 1px 0 0; <br />  padding: 0; <br />  font-family: ""Lucida Grande"", sans-serif; <br />  font-size: 80%; <br />}<br /><br />nav[role=""navigation""] ul  <br />{<br />  float:left;<br />  width:100%;<br />  margin: 0; <br />  padding: 10px 0 0 0px; <br />  list-style: none; <br />  background: #FFCB2D url(images/nav_bg.gif) repeat-x bottom left; <br />}<br /><br />nav[role=""navigation""] ul li a { <br />  float: right; <br />  display: block; <br />  margin: 0; <br />  padding: 4px 8px; <br />  color: #333; <br />  text-decoration: none; <br />  border: 1px solid #9B8748; <br />  border-bottom: none; <br />  background: #F9E9A9 url(img/off_bg.gif) repeat-x top left; <br />} <br />  <br />nav[role=""navigation""] ul li a:hover<br />{ <br />  color: #333; <br />  padding-bottom: 5px; <br />  border-color: #727377; <br />  background: #FFF url(images/on_bg.gif) repeat-x top left; <br />}</pre><p>And if anyone's interested, the gif files I used are as follows:</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/06122011_nav_bg.png\" alt=\"nav_bg\" /></div>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/06122011_on_bg.png\" alt=\"on_bg\" /></div>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/06122011_off_bg.png\" alt=\"off_bg\" /></div>" +
            "<br /><p>And I got to the point where the tabs were functional and nicely displayed fairly quickly.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/06122011_Log_On.png\" alt=\"Log On\" /></div>" +
            "<p>Now I got to the point where I had to make the tab \"stick\" in the selected state, so it would be visible which tab is currently selected. And the way it was done in the book was by adding an id element to the body and assigning a value to it. The css was then modified like this:</p><pre class=\"brush:css\">" + @"nav[role=""navigation""] ul li a:hover, body#home #nav-home a, body#about #nav-about a, body#inbasket #nav-inbasket a<br />{ <br />  color: #333; <br />  padding-bottom: 5px; <br />  border-color: #727377; <br />  background: #FFF url(images/on_bg.gif) repeat-x top left; <br />}" + "</pre><p>So in this case the hovering and selection is combined in one css declaration. However, the problem I had was that I could not just go to individual pages and set the correct id elements in page bodies. The way the Razor engine works, of course, is by rendering all the common HTML in the _Layout.cshtml, including the body tag. To achieve my goal, I had to modify the body tag after the page was rendered. That was not as hard a I expected - I wrote a small HTML helper which added a couple of javaScript lines to the page</p><pre class=\"brush:csharp\">" +
            @"public static IHtmlString BodyTagUpdate(this HtmlHelper helper, string text)<br />{<br /> return new HtmlString(@""&lt;script type=""""text/javascript""""&gt;<br />       document.body.id ='"" + text + ""';"" +<br />       ""&lt;/script&gt;"");<br />}</pre>" + "<p>and then I added a call to this helper on any page that I had to.</p><pre class=\"brush:csharp\">" +
            @"@Html.BodyTagUpdate(""about"")" + "</pre><p>And it worked. Now I was at this stage.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/06122011_The_Project.png\" alt=\"The Project\" /></div>" +
            "<p>The last thing I added was displaying certain tabs only for the users that are logged on. This turned out to be extremely easy. This is the modified HTML of a navigation element:</p><pre class=\"brush:xml\">" +
            @"&lt;nav role=""navigation""&gt;<br /> &lt;ul id=""menu""&gt;<br />  &lt;li id=""nav-home""&gt;@Html.ActionLink(""Home"", ""Index"", ""Home"")&lt;/li&gt;<br />  &lt;li id=""nav-about""&gt;@Html.ActionLink(""About"", ""About"", ""Home"")&lt;/li&gt;<br />  @if(Request.IsAuthenticated) {<br />  &lt;li id=""nav-inbasket""&gt;@Html.ActionLink(""In Basket"", ""../InBasket/Index"")&lt;/li&gt;<br />  }<br /> &lt;/ul&gt;<br />&lt;/nav&gt;" + "</pre><p><p>And the very last thing was to get rid of the built-in Log On/Log Off div and move it into the last tab. It involved writing a couple extra HTML helpers - one to render correct text, and the other is essentially an extension to the ActionLink which allows to pass HTML in and out so the link can be formatted. It is not critical but may become more useful later.</p><pre class=\"brush:csharp\">" +
            @"public static IHtmlString LogOnOff(this HtmlHelper helper, string text, bool isLogon)<br />{<br /> if (isLogon)<br /> {<br />  text = ""Log On"";<br /> }<br /> else<br /> {<br />  text = @""&lt;strong&gt;"" + text + @""&lt;/strong&gt; - Log Off"";<br /> }<br /><br /> return new HtmlString(text);<br />}<br /><br />public static IHtmlString ActionHTML(this HtmlHelper helper, string action, string controller, string text)<br />{<br /> var url = new UrlHelper(helper.ViewContext.RequestContext);<br /><br /> var linkWriter = new HtmlTextWriter(new StringWriter());<br /> linkWriter.AddAttribute(HtmlTextWriterAttribute.Href, url.Action(action, controller));<br /> linkWriter.RenderBeginTag(HtmlTextWriterTag.A);<br /> linkWriter.Write(text);<br /> linkWriter.RenderEndTag(); //A<br /><br /> return new HtmlString(linkWriter.InnerWriter.ToString());<br />}" + "</pre><p>The partial view _LogOnPartial is now a bit simplified:</p><pre class=\"brush:csharp\">" +
            @"@if(Request.IsAuthenticated) {<br /> string s = @User.Identity.Name;<br /> IHtmlString t = Html.LogOnOff(s, false);<br />    @Html.ActionHTML(""LogOff"", ""Account"", t.ToString())<br />}<br />else {<br /> @Html.ActionHTML(""LogOn"", ""Account"", ""Log On"")<br />}" + "</pre><p>And the div that was rendering it into the _Layout.cshtml has now moved into the navigation area:</p><pre class=\"brush:xml\">" +
            @"&lt;nav role=""navigation""&gt;<br /> &lt;ul id=""menu""&gt;<br />  &lt;li id=""nav-home""&gt;@Html.ActionLink(""Home"", ""Index"", ""Home"")&lt;/li&gt;<br />  &lt;li id=""nav-about""&gt;@Html.ActionLink(""About"", ""About"", ""Home"")&lt;/li&gt;<br />  @if(Request.IsAuthenticated) {<br />  &lt;li id=""nav-inbasket""&gt;@Html.ActionLink(""In Basket"", ""../InBasket/Index"")&lt;/li&gt;<br />  }<br />  &lt;li id=""nav-log""&gt;@Html.Partial(""_LogOnPartial"")&lt;/li&gt;<br /> &lt;/ul&gt;<br />&lt;/nav&gt;" +
            "</pre><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/06122011_Application_Main_Page.png\" alt=\"Application Main Page\" /></div>" +
            "<p>Reference:</p><a href=\"http://www.amazon.com/Bulletproof-Web-Design-flexibility-protecting/dp/0321808355\">Bulletproof Web Design: Improving flexibility and protecting against worst-case scenarios with HTML5 and CSS3 (3rd Edition)</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_06122011_k = "Software development ASP.NET MVC scalable navigation";
        public const string content_06122011_d = "Learning MVC: Scalable Navigation";

        //"Learning MVC: Custom Html Helpers"
        public const string content_03122011_b = "<p>Html helpers are used to render HTML and, in most cases, return a string which is then rendered as part of web page. The underlying idea is to reduce development work, decrease the amount of typing and generally provide a readable markup. However, I quickly found that the helpers provided with the MVC framework are small in number. Fortuantely, it is relatively easy to write your own html helpers. There are several ways to do it, but so far I liked extending the HtmlHelper class the best. There are a couple of \"gotchas\" there, but after that it looks elegant and efficient.</p>";
        public const string content_03122011_r = "<p>I started with creating a folder in my solution called \"HtmlHelpers\" and creating the Helpers.cs class there. The class is part of the namespace \"HtmlHelpers\". The first \"gotcha\", or two, is to add the namespace to the Web.config file. The second, smaller \"gotcha\" is that it may not be enough to add it to the main Web.config class. If, for example, the helper is used by the view which resides in the Views folder, then the Web.config in the Views folder (if it exists) also needs to have the namespace registered.</p><pre class=\"brush:xml\">" +
            @"&lt;pages&gt;<br />  &lt;namespaces&gt;<br /> &lt;add namespace=""System.Web.Helpers""/&gt;<br /> &lt;add namespace=""System.Web.Mvc""/&gt;<br /> &lt;add namespace=""System.Web.Mvc.Ajax""/&gt;<br /> &lt;add namespace=""System.Web.Mvc.Html""/&gt;<br /> &lt;add namespace=""System.Web.Routing""/&gt;<br /> &lt;add namespace=""System.Web.WebPages""/&gt;<br /> &lt;add namespace=""HtmlHelpers""/&gt;<br />  &lt;/namespaces&gt;<br />&lt;/pages&gt;" + "</pre><p>Both the class and the extension methods have to be static. The type of the extension method can not be \"string\" - that's another \"gotcha\". The engine will escape all the HTML tags by default. This is why the HtmlString should be returned. That was my last \"gotcha\". Here's the whole class:</p><br /><pre class=\"brush:csharp\">" +
            @"namespace HtmlHelpers<br />{<br />    public static class Helpers<br />    {<br />        public static IHtmlString BulletedList(this HtmlHelper helper, string data)<br />        {<br />            string[] items = data.Split('|');<br /><br />            var writer = new HtmlTextWriter(new StringWriter());<br />            writer.RenderBeginTag(HtmlTextWriterTag.Ul);<br /><br />            foreach (string s in items)<br />            {<br />                writer.RenderBeginTag(HtmlTextWriterTag.Li);<br />                writer.Write(helper.Encode(s));<br />                writer.RenderEndTag();<br />            }<br /><br />            writer.RenderEndTag();<br />            return new HtmlString(writer.InnerWriter.ToString());<br />        }<br /><br />        public static IHtmlString Paragraph(this HtmlHelper helper, string text)<br />        {<br />            return new HtmlString(""&lt;p&gt;"" +text + ""&lt;/p&gt;"");<br />        }<br />    }<br />}" +
            "</pre><p>The first method takes strings delimited by \"|\" character and creates a bulleted list from them. Here I used HtmlTextWriter. In the second method I've shown that the HtmlTextWriter is not absolutely necessary - it is possible to just add tags \"by hand\".</p><p>This kind of usage</p><p>@Html.Paragraph(\"A custom paragraph\")<br>@Html.BulletedList(\"Item 1|Item 2|Item 3|Item 4\")</p><p>Provides the following output</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/03122011_Custom_HTML_Helper.png\" alt=\"Custom HTML Helper\" /></div>" +
            "<p>References:</p><a href=\"http://stackoverflow.com/questions/6950887/custom-htmlhelper-renders-text-and-not-markup\">Custom HtmlHelper Renders Text and not Markup</a><br><a href=\"http://stephenwalther.com/blog/archive/2009/03/03/chapter-6-understanding-html-helpers.aspx\">Understanding HTML Helpers</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_03122011_k = "Software development ASP.NET MVC custom html helpers";
        public const string content_03122011_d = "Learning MVC: Custom Html Helpers";

        //"Learning MVC: Adding Autocomplete Dropdown"
        public const string content_02122011_b = "<p>I added the autocomplete function to one of the views. Turned out to be a simple task if I use the jQuery-provided function. Initially I tried to use Dylan Verheul's plugin, but inevitably ended up with the \"Object does not support this property or method\" the reason for which I still did not find. Very frustrating. Anyway.</p><p>First I added the following line to the _Layout.shtml under Views/Shared</p>";
        public const string content_02122011_r = "<pre class=\"brush:js\">" +
            @"<script src=\""@Url.Content(""~/Scripts/jquery-ui-1.8.11.min.js"")"" type=""text/javascript""></script></pre>" + "<p>Next, I used the Named Sections to place the javascript that I want to be in my view. In the _Layout.shtml file I added the following line:</p><pre class=\"brush:js\">" +
            @"@RenderSection(""JavaScript"", required: false)" + "</pre><p>Now I can add the \"JavaScript\" section to any view in the following manner:</p><pre class=\"brush:js\">" +
            @"@section JavaScript<br />{<br />&lt;script type=""text/javascript""&gt;<br />  $(function () {<br />   $(""#Title"").autocomplete({<br />    source: ""/InBasket/Find"",<br />    minLength: 1,<br />    select: function (event, ui) {<br />     if (ui.item) {<br />      $(""#Title"").val(ui.item.value);<br />     }<br />    }<br />   });<br />  });<br /> &lt;/script&gt;}" +
            "</pre><p>and the section will be added to the view when it's rendered. The script above binds the autocomplete function to the Title input field. The \"/InBasket/Find\" calls the Find method in the InBasketController (which I'll write a bit later). The minLength specifies how many characters have to be in the box before the autocomplete fires. If my database is large, I may set it to 3, 5 or more to avoid huge responses where everything starting with \"a\" is returned. But for now I just want to test the functionality, so I set it to \"1\". And then, when I select an item from the autocomplete list, it sets the value of my Title input box to this item.</p><p>So that's the View part, now the Controller part. I started in my repository and added a function to return all user's InBasket item titles that start with a particular string, and the results should not be case-sensitive.</p><pre class=\"brush:csharp\">" +
            "//used by autocomplete: return all inbasket items where Title starts with a string provided<br />public IQueryable<InBasket> FindUserInBasketItemsByTitle(string title, int userID)<br />{<br /> var inBaskets = db.InBaskets.Where(item => item.UserID == userID);<br /> inBaskets = inBaskets.Where(item => item.Title.ToLower().StartsWith(title.ToLower()));<br /> return inBaskets;<br />}" + "</pre><p>Next, I added the Find method to the View. The method gets the value from the Title input box and returns the JSON array of values. One thing to note: it is important that the string parameter is called \"term\". I didn't know that initially and was wondering for a while why my parameter \"s\" is always null. Here is the whole Find method:</p><pre class=\"brush:csharp\">" +
            @"public JsonResult Find(string term, Users user)<br />{<br /> var inBaskets = repository.FindUserInBasketItemsByTitle(term, user.UserID);<br /> var titles = new List<string>();<br /> foreach(InBasket inBasket in inBaskets)<br /> {<br />  titles.Add(inBasket.Title);<br /> }<br /> return Json(titles, JsonRequestBehavior.AllowGet);<br />}" +
            "</pre><p>And that's all - it only takes a dozen or two lines of code. The result is horribly ugly at the moment, but it's a proof of concept:</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/02122011_Autocomplete_Dropdown.png\" alt=\"Autocomplete Dropdown\" /></div>" +
            "<p>References:</p><a href=\"http://volaresystems.com/Blog/post/Autocomplete-dropdown-with-jQuery-UI-and-MVC.aspx\">Autocomplete dropdown with jQuery UI and MVC</a><br><a href=\"http://weblogs.asp.net/scottgu/archive/2010/12/30/asp-net-mvc-3-layouts-and-sections-with-razor.aspx\">ASP.NET MVC 3: Layouts and Sections with Razor</a><br><a href=\"http://stackoverflow.com/questions/4311783/asp-net-mvc-3-razor-include-js-file-in-head-tag\">ASP.Net MVC 3 Razor: Include js file in Head tag</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_02122011_k = "Software development ASP.NET MVC Autocomplete dropdown";
        public const string content_02122011_d = "Learning MVC: Adding Autocomplete Dropdown";

        //"Learning MVC: Installing Application on IIS"
        public const string content_01122011_b = "<p>Today I tried installing the MVC application on IIS 7.</p><p>It was not an extremely complicated task, as long as understood the prerequisites. I installed the .NET Framework 4 on the server that has IIS7. I then registered the .NET Framework with IIS by running the registration tool (aspnet_regiis.exe)</p>";
        public const string content_01122011_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/01122011_regiis.png\" alt=\"regiis\" /></div>" +
            "<p>Before publishing a project, it is a good idea to use the \"Add Deployable Dependencies\" to make sure all required libraries are added to the published application. Initially I missed this step and started getting errors like the one below.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/01122011_Configuration_Error.png\" alt=\"Configuration Error\" /></div>" +
            "<p>Initially I tried to solve them by manually copying the required dlls to the bin folder of the application, but got tired quickly and found a better solution, as mentioned above.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/01122011_Add_Deployable_Dependencies.png\" alt=\"Add Deployable Dependencies\" /></div>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/01122011_Add_Deployable_Dependencies_2.png\" alt=\"Add Deployable Dependencies\" /></div>" +
            "<p>It generated a rather long list of files, it probably would not be a good idea to copy them all manually one by one.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/01122011_Solution_Explorer.png\" alt=\"Solution Explorer\" /></div>" +
            "<p>Next, I published the project to a file system.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/01122011_Publish_Web.png\" alt=\"Publish Web\" /></div>" +
            "<p>Created a directory on the server that is running IIS under wwwroot/test, copied the published application to the server, created the virtual directory on the Default Web Site and pointed it to my wwwroot/test folder. That was enough to be able to start the application and see the welcome page. Unfortunately, that is not the end of it. At this point I can navigate to the \"Register\" page, but the App_Data folder does not yet exist and also when I try to go my InBasket page directly, I get a server error. Additionally, my local version of the application, which worked fine, now displays the same behaviour. Quite a few things to fix!</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/01122011_Server_Error.png\" alt=\"Server Error\" /></div>" +
            "<p>References</p><a href=\"http://iwantmymvc.com/2011-03-23-bin-deploy-aspnet-mvc-3-visual-studio\">Bin deploy required dependencies for MVC 3 projects with Visual Studio 2010 SP1</a><br><a href=\"http://haacked.com/archive/2008/11/26/asp.net-mvc-on-iis-6-walkthrough.aspx\">ASP.NET MVC on IIS 6 Walkthrough</a><br><a href=\"http://msdn.microsoft.com/en-us/library/k6h9cz8h.aspx\">ASP.NET IIS Registration Tool (Aspnet_regiis.exe)</a><br><a href=\"http://www.hanselman.com/blog/BINDeployingASPNETMVC3WithRazorToAWindowsServerWithoutMVCInstalled.aspx\">BIN Deploying ASP.NET MVC 3 with Razor to a Windows Server without MVC installed</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_01122011_k = "Software development ASP.NET MVC Installation IIS";
        public const string content_01122011_d = "Learning MVC: Installing Application on IIS";

        public const string content_28112011_b = "<p>A model binder is a powerful concept in MVC. Implementing a model binder allows to pass an object to the controller methods automatically by the framework. In my case, I was looking for a way to get the id of the currently logged in user. This has to be checked any time user-specific data is accessed - which is, essentially, always. Placing extra code into each and every method of every controller does not look like a good solution. Here's how a model binder can be used:</p><p>First, implement IModelBinder. I only need it to return an int, and I get this int from my Users table.</p>";
        public const string content_28112011_r = "<pre class=\"brush:csharp\">" +
            @"public class IUserModelBinder : IModelBinder<br />{<br /> public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)<br /> {<br />  int userID = 0;<br />  if (controllerContext == null) <br />  {<br />   throw new ArgumentNullException(""controllerContext"");<br />  }<br />  if (bindingContext == null)<br />  {<br />   throw new ArgumentNullException(""bindingContext"");<br />  }<br />  if (Membership.GetUser() != null)<br />  { <br />   MembershipUser member = Membership.GetUser();<br />   string guid = member.ProviderUserKey.ToString();<br /><br />   using (modelGTDContainer db = new modelGTDContainer())<br />   {<br />    userID = db.Users.Single(t => t.UserGUID == guid).UserID;<br />   }<br />  }<br />  return userID;<br /> }<br />}" +
            "</pre><p>Next, I need to register the model binder when the application starts - in the global.asax.cs I only need to add one line to Application_Start</p><pre class=\"brush:csharp\">" +
            @"protected void Application_Start()<br />{<br /> AreaRegistration.RegisterAllAreas();<br /><br /> RegisterGlobalFilters(GlobalFilters.Filters);<br /> RegisterRoutes(RouteTable.Routes);<br /> ModelBinders.Binders[typeof(int)] = new IUserModelBinder();<br />}" +
            "</pre><p>And finally, I add an int parameter to any controller method that needs a userID to be passed. And, by the magics of MVC, it is there! This is a huge saving of work by adding just a few lines of code.</p><pre class=\"brush:csharp\">" +
            @"//<br />// GET: /InBasket/<br />[Authorize]<br />public ViewResult Index(int userID)<br />{<br /> var inbasket = repository.FindUserInBasketItems(userID);<br /> return View(inbasket.ToList());<br />}" +
            "</pre><p>What's bad - I have to hit a database every time I access any data to get the user ID, and then hit a database again when I select the actual data for the user. How to fix it? Well, one way is to use the user Guid as a primary key for the Users table. However, it is a string, not an int. Performance may suffer anyway, and the indexing will be affected. But, thanks to a model binder, if I want to change this in the future, it will only have to be done in one place.</p><p>References:</p><a href=\"http://www.hanselman.com/blog/IPrincipalUserModelBinderInASPNETMVCForEasierTesting.aspx\">IPrincipal (User) ModelBinder in ASP.NET MVC for easier testing</a><br><br><a href=\"http://stackoverflow.com/questions/5195027/multi-user-app-with-mvc3-asp-net-membership-user-authentication-data-separa\">Multi User App with MVC3, ASP.NET Membership - User Authentication / Data Separation</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_28112011_k = "MVC C# ASP.NET Software Development model binders";
        public const string content_28112011_d = "Learning MVC: Model Binders";

        public const string content_27112011_b = "<p>What is the easiest way to validate class properties that have to be stored in a database - for example, if a database field \"Title\" has a limit of 50 characters, how do I enforce it best? I could set a \"Required\" attribute directly on the class property, but the Visual Studio Designer that generated this class may not like it. And anyway, if ever need to change the model and regenerate the database, the attribute is likely to be wiped anyway.</p>";
        public const string content_27112011_r = "<p>A better idea may be to specify a special class that will handle validation (\"buddy class\" - funny name which seems to be an official term). I can add a partial declaration to the existing class which will not be wiped if the model changes, and in this declaration I will specify the class that handles validation. As long as the property names of the buddy class exactly match those of the actual class, I should be fine and the valiation will be handled for me by my model!</p><p>The code looks like that:</p><pre class=\"brush:csharp\">" +
            @"[MetadataType(typeof(InBasket_Validation))]<br />public partial class InBasket<br />{<br /><br />}<br /><br />public class InBasket_Validation<br />{<br /> [Required(ErrorMessage = ""Title is Required"")]<br /> [StringLength(100, ErrorMessage = ""Title can not be longer than 100 characters"")]<br /> public string Title { get; set; }<br /><br /> [Required(ErrorMessage = ""Content is Required"")]<br /> [StringLength(5000, ErrorMessage = ""Content can not be longer than 5000 characters"")]<br /> public string Content { get; set; }<br />}" +
            "</pre><p>The Metadata attribute specifies the buddy class, and the buddy class specifies validation requirements. The partial InBasket class is empty cause I don't want to add anything to the actual class functionality. The code builds (why wouldn't it? It's more important if it works), and I'll test it when I'm done with the views.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_27112011_k = "MVC C# ASP.NET Software Development validation";
        public const string content_27112011_d = "Learning MVC: A Quick Note on Validation";

        //"Learning MVC: A Repository Pattern"
        public const string content_26112011_b = "<p>A repository is just a place where data querying is encapsulated. There are several main reasons for a repository:</p>";
        public const string content_26112011_r = "<ul><li>Avoid repetition. If I need to write a query, I will first check the repository - maybe it was already implemented</li><li>Encapsulation. Keep all data related code in the same place. Makes refactoring easier and separates logic from data</li><li>Unit testing. Tests can be written against the repository and, if necessary, in such way that the real database is not required</li></ul><p>For the purpose of my sample application, which I explain later, I will now add a repository for the \"In Basket\". It's extremely simple: each user can have multiple items in the basket. A user can view, edit and delete any of his items. So I need a small number of methods:</p><pre class=\"brush:csharp\">" +
            @"public class InBasketRepository<br />{<br />    private modelGTDContainer db = new modelGTDContainer();<br /><br />    //return all in basket items for a certain user<br />    public IQueryable<InBasket> FindUserInBasketItems(int userID)<br />    { <br />        return db.InBaskets.Where(item => item.UserID == userID);<br />    }<br /><br />    public InBasket GetInBasketItem(int id)<br />    {<br />        return db.InBaskets.Single(item => item.InBasketID == id);<br />    }<br /><br />    public void AddInBasketItem(InBasket item)<br />    {<br />        db.InBaskets.AddObject(item);<br />    }<br /><br />    public void DeleteInBasketItem(InBasket item)<br />    {<br />        db.InBaskets.DeleteObject(item);<br />    }<br /><br />    //persistence<br />    public void Save()<br />    {<br />        db.SaveChanges();<br />    }<br />}</pre>" +
            "<p>It seems logical for the repository to exist in the Models folder.</p><p>And that's it for now - the next step is to create view(s) which will use the repository.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_26112011_k = "MVC C# ASP.NET Software Development repository pattern";
        public const string content_26112011_d = "Learning MVC: A Repository Pattern";

        //"Learning MVC: A multi-user application concept"
        public const string content_23112011_b = "<p>As a first experiment with MVC framework, I decided to consider the application that has multiple users where each user has some information stored in the local database. I.e. his \"To Do List\", to which no one else should have access. The problem, then, is to find a way to uniquely identify the user when he logs on (and, on a later stage, to select data that belongs to this user). Here's a bit of a naive first approach.</p>";
        public const string content_23112011_r = "<p>Create a database to hold the users, with the GUID being the primary key and ID being an identity and autoincremental. I used SQL CE 4.</p><p>App_Data -> Add -> New Item -> SQL Server Compact 4.0 Local Database -> dbUsers.sdf</p><p>Tables -> Create Table</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/23112011_New_Table.png\" alt=\"New Table\" /></div>" +
            "<p>Create a model from database. Project -> Add New Item -> Data -> ADO.NET Entity Data Model -> modelUsers.edmx -> Add -> Generate From Database -> dbUsers.mdf -> specify the tblUsers table and Finish.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/23112011_tblUser.png\" alt=\"tblUser\" /></div>" +
            "<p>Create a Controller to work with the Users class</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/23112011_Add_Controller.png\" alt=\"Add Controller\" /></div><p>Some useful bits of code in the controller:</p><p>To create a user</p><pre class=\"brush:csharp\">" +
            @"[HttpPost]<br />public ActionResult Create(tblUser tbluser)<br />{<br /> if (ModelState.IsValid)<br /> {<br />  db.tblUsers.AddObject(tbluser);<br />  db.SaveChanges();<br />  return RedirectToAction(""Index"");  <br /> }<br /><br /> return View(tbluser);<br />}" +
            "</pre><p>To get user details</p><pre class=\"brush:csharp\">public ViewResult Details(int id)<br />{<br /> tblUser tbluser = db.tblUsers.Single(t => t.UserID == id);<br /> return View(tbluser);<br />}</pre><p>Next, I'm going to try and stick some code into the AccountController.cs provided by the MVC application template. I want to insert a new user into my database table when the new user is registered and I want to get the user ID from the database when the user is authenticated successfully. In the future, probably, user ID may not be required at all and I can make the User GUID a primary key.</p><p>So that's how it looks in the Register method of the AccountController:</p><pre class=\"brush:csharp\">" +
            @"if (createStatus == MembershipCreateStatus.Success)<br />{<br /> //Insert a user into the database<br /><br /> tblUser user = new tblUser();<br /><br /> MembershipUser mUser = Membership.GetUser(model.UserName);<br /> if (mUser != null)<br /> {<br />  user.UserGUID = mUser.ProviderUserKey.ToString();<br /><br />  using (dbUsersEntities db = new dbUsersEntities())<br />  {<br />   db.tblUsers.AddObject(user);<br />   db.SaveChanges();<br />  }<br /> }<br /><br /> FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);<br /> return RedirectToAction(""Index"", ""Home"");<br />}" +
            "</pre><p>And this is in the LogOn method of the AccountController:</p><pre class=\"brush:csharp\">" +
            @"if (Membership.ValidateUser(model.UserName, model.Password))<br />{<br /> //user is valid, find his ID in the tblUsers<br /> tblUser tbluser;<br /> using (dbUsersEntities db = new dbUsersEntities())<br /> {<br />  MembershipUser mUser = Membership.GetUser(model.UserName);<br />  if (mUser != null)<br />  {<br />   string guid = mUser.ProviderUserKey.ToString();<br />   tbluser = db.tblUsers.Single(t => t.UserGUID == guid);<br />  }<br /> }<br /><br /> FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);<br /> if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith(""/"")<br />  && !returnUrl.StartsWith(""//"") && !returnUrl.StartsWith(""/\\""))<br /> {<br />  return Redirect(returnUrl);<br /> }<br /> else<br /> {<br />  return RedirectToAction(""Index"", ""Home"");<br /> }<br />}" +
            "</pre><p>And a quick Index view for the UsersController to verify that the users are actually inserted in the database:</p><pre class=\"brush:html\">" +
            "@foreach (var item in Model) {<br />    &lt;tr&gt;<br />  &lt;td&gt;<br />   @Html.DisplayFor(modelItem =&gt; item.UserID)<br />  &lt;/td&gt;<br />  &lt;td&gt;<br />   @Html.DisplayFor(modelItem =&gt; item.UserGUID)<br />  &lt;/td&gt;<br />    &lt;/tr&gt;<br />}" +
            "</pre><p>Register a user</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/23112011_New_Entity.png\" alt=\"New Entity\" /></div><p>And then verify that a user with that ID and GUID is now present in the tblUsers.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/23112011_Index_Page.png\" alt=\"Index Page\" /></div><p>The concept looks feasible, now on to refining and improving it.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_23112011_k = "MVC C# ASP.NET Software Development multi-user application";
        public const string content_23112011_d = "Learning MVC: A multi-user application concept";

        //"NuGet, Entity Framework 4.1 and DbContext API"
        public const string content_22112011_b = "<p>NuGet is a \"Package Manager\" that can and should be used with Visual Studio 2010 because it makes installing and updating the libraries, frameworks and extensions so much easier. To install NuGet, I go to Tools -> Extension Manager within Visual Studio and search for the NuGet in the online gallery. In the search results, all I have to do is click \"Install\".</p>";
        public const string content_22112011_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/22112011_Extension_Manager.png\" alt=\"Extension Manager\" /></div>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/22112011_NuGet_Package_Manager.png\" alt=\"NuGet Package Manager\" /></div>" +
            "<p>Now, what if I have Entity Framework installed and want to update version 4 to 4.2? I don't have to search it somewhere on download.microsoft.com or elsewhere. Right within my project I go to References, right-click and select \"Add library package reference\".</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/22112011_Add_Library_Package_Reference.png\" alt=\"Add Library Package Reference\" /></div>" +
            "<p>The Entity Framework is installed, but it's version 4.1.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/22112011_Entity_Framework.png\" alt=\"Entity Framework\" /></div>" +
            "<p>I select \"Updates\" from the menu on the left and immediately see that 4.2 is available. I select \"Update\", accept terms and conditions and I'm done.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/22112011_Entity_Framework_2.png\" alt=\"Entity Framework\" /></div>" +
            "<p>Steps that are not required: searching for an update package, manual download of the package, manual uninstall of the previous version, manual install of the new version, verifying that I save the downloaded package in an easily accessible location in case anyone in my team also needs it ... Time saved per package update: anywhere between 3 and 30 minutes.</p><p>However, it does not always go smoothly. Just today I tried to add a package \"on the fly\". Right-click my model, go to \"Add code generation item\", select \"ADO.NET C# DbContext Generator\" and click \"Install\". And here Visual Studio stopped responding.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/22112011_Add_Code_Generation_Item.png\" alt=\"Add Code Generation Item\" /></div>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/22112011_DbContext_Generator.png\" alt=\"DbContext Generator\" /></div>" +
            "<p>I killed it, repeated the sequence of actions and it stopped responding again. So I started it and added the package through the Tools -> Extension Manager as described above and it worked perfectly. So, don't ask too much from your favourite IDE.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_22112011_k = "Software development visual studio entity framework nuget dbcontext api";
        public const string content_22112011_d = "NuGet, Entity Framework 4.1 and DbContext API";

        //"Tortoise SVN for Windows and Checking Out Code from Google"
        public const string content_18112011_b = "<p>While I did not have a chance to properly configure my GitHub access yet (I think my corporate network is blocking some connections, so I'll try from home) I needed to checkout some code from code.google.com.</p>";
        public const string content_18112011_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/18112011_SVN_Command_Line.png\" alt=\"SVN Command Line\" /></div>" +
            "<p>Following the advice, I searched around for a Windows SVN client and downloaded Tortoise SVN</p><p><a href=\"http://tortoisesvn.net/downloads.html\">Tortoise SVN</a></p><p>It does not have a UI as such.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/18112011_TortoiseSVN.png\" alt=\"TortoiseSVN\" /></div>" +
            "<p>It is integrated into Windows Explorer and displays the menu on the right-click. To get code, I have to select \"SVN Checkout\".</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/18112011_SVN_Checkout.png\" alt=\"SVN Checkout\" /></div>" +
            "<p>The checkout screen is quite self-explanatory.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/18112011_Checkout.png\" alt=\"Checkout\" /></div>" +
            "<p>However, my first attempt was unsuccessful.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/18112011_Repository_Browser.png\" alt=\"Repository Browser\" /></div>" +
            "<p>I immediately suspected the corporate proxy server. Tortoise SVN has settings that are accessed through Program Files, so after some digging around I came up with the correct network settings.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/18112011_SVN_Settings.png\" alt=\"SVN Settings\" /></div>" +
            "<p>Things went smoothly from there on.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/18112011_Repository_Browser_2.png\" alt=\"Repository Browser\" /></div>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/18112011_Checkout_Finished.png\" alt=\"Checkout Finished\" /></div>" +
            "<p>Much easier than GitHub/GitExtensions so far! From zero knowledge about the application (Tortoise SVN) to a checked out solution in, probably, about 10 minutes - about as much as this post took, and even less if I was accessing it from home without any proxies. Next time I'll try to add some of my code to code.google.com</p><p>Reference:</p><p><a href=\"http://stackoverflow.com/questions/41766/how-do-i-download-code-using-svn-tortoise-from-google-code\">How do I download code using SVN/Tortoise from Google Code?</a></p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_18112011_k = "Windows Tortoise SVN Google code";
        public const string content_18112011_d = "Tortoise SVN for Windows and Checking Out Code from Google";

        //"Generating a C# Class Based on the Underlying SQL"
        public const string content_14112011_b = "<p>If a class structure is based on the underlying database, it may be useful to be able to automatically generate a class \"stub\" based on the SQL Server table. I looked up the ways to do it without too much upfront time investment, and decided to follow one of the approaches.</p>";
        public const string content_14112011_r = "<p>First, a table have to be created to define the types in SQL Server and corresponding types in the language of choice - C# for me. The table is created by the following script:</p><pre class=\"brush:sql\">" +
            @"/****** Object:  Table [dbo].[DbVsCSharpTypes]    Script Date: 03/20/2010 03:07:56 ******/<br />IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DbVsCSharpTypes]') <br />AND type in (N'U'))<br />DROP TABLE [dbo].[DbVsCSharpTypes]<br />GO<br /><br />/****** Object:  Table [dbo].[DbVsCSharpTypes]    Script Date: 03/20/2010 03:07:56 ******/<br />SET ANSI_NULLS ON<br />GO<br /><br />SET QUOTED_IDENTIFIER ON<br />GO<br /><br />CREATE TABLE [dbo].[DbVsCSharpTypes](<br /> [DbVsCSharpTypesId] [int] IDENTITY(1,1) NOT NULL,<br /> [Sql2008DataType] [varchar](200) NULL,<br /> [CSharpDataType] [varchar](200) NULL,<br /> [CLRDataType] [varchar](200) NULL,<br /> [CLRDataTypeSqlServer] [varchar](2000) NULL,<br /><br /> CONSTRAINT [PK_DbVsCSharpTypes] PRIMARY KEY CLUSTERED <br />(<br /> [DbVsCSharpTypesId] ASC<br />)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]<br />) ON [PRIMARY]<br /><br />GO<br /><br />SET NOCOUNT ON;<br />SET XACT_ABORT ON;<br />GO<br /><br />SET IDENTITY_INSERT [dbo].[DbVsCSharpTypes] ON;<br />BEGIN TRANSACTION;<br />INSERT INTO [dbo].[DbVsCSharpTypes]([DbVsCSharpTypesId], [Sql2008DataType], [CSharpDataType], [CLRDataType], [CLRDataTypeSqlServer])<br />SELECT 1, N'bigint', N'short', N'Int64, Nullable<Int64>', N'SqlInt64' UNION ALL<br />SELECT 2, N'binary', N'byte[]', N'Byte[]', N'SqlBytes, SqlBinary' UNION ALL<br />SELECT 3, N'bit', N'bool', N'Boolean, Nullable<Boolean>', N'SqlBoolean' UNION ALL<br />SELECT 4, N'char', N'char', NULL, NULL UNION ALL<br />SELECT 5, N'cursor', NULL, NULL, NULL UNION ALL<br />SELECT 6, N'date', N'DateTime', N'DateTime, Nullable<DateTime>', N'SqlDateTime' UNION ALL<br />SELECT 7, N'datetime', N'DateTime', N'DateTime, Nullable<DateTime>', N'SqlDateTime' UNION ALL<br />SELECT 8, N'datetime2', N'DateTime', N'DateTime, Nullable<DateTime>', N'SqlDateTime' UNION ALL<br />SELECT 9, N'DATETIMEOFFSET', N'DateTimeOffset', N'DateTimeOffset', N'DateTimeOffset, Nullable<DateTimeOffset>' UNION ALL<br />SELECT 10, N'decimal', N'decimal', N'Decimal, Nullable<Decimal>', N'SqlDecimal' UNION ALL<br />SELECT 11, N'float', N'double', N'Double, Nullable<Double>', N'SqlDouble' UNION ALL<br />SELECT 12, N'geography', NULL, NULL, N'SqlGeography is defined in Microsoft.SqlServer.Types.dll, which is installed with SQL Server and can be downloaded from the SQL Server 2008 feature pack.' UNION ALL<br />SELECT 13, N'geometry', NULL, NULL, N'SqlGeometry is defined in Microsoft.SqlServer.Types.dll, which is installed with SQL Server and can be downloaded from the SQL Server 2008 feature pack.' UNION ALL<br />SELECT 14, N'hierarchyid', NULL, NULL, N'SqlHierarchyId is defined in Microsoft.SqlServer.Types.dll, which is installed with SQL Server and can be downloaded from the SQL Server 2008 feature pack.' UNION ALL<br />SELECT 15, N'image', NULL, NULL, NULL UNION ALL<br />SELECT 16, N'int', N'int', N'Int32, Nullable<Int32>', N'SqlInt32' UNION ALL<br />SELECT 17, N'money', N'decimal', N'Decimal, Nullable<Decimal>', N'SqlMoney' UNION ALL<br />SELECT 18, N'nchar', N'string', N'String, Char[]', N'SqlChars, SqlString' UNION ALL<br />SELECT 19, N'ntext', NULL, NULL, NULL UNION ALL<br />SELECT 20, N'numeric', N'decimal', N'Decimal, Nullable<Decimal>', N'SqlDecimal' UNION ALL<br />SELECT 21, N'nvarchar', N'string', N'String, Char[]', N'SqlChars, SqlStrinG SQLChars is a better match for data transfer and access, and SQLString is a better match for performing String operations.' UNION ALL<br />SELECT 22, N'nvarchar(1), nchar(1)', N'string', N'Char, String, Char[], Nullable<char>', N'SqlChars, SqlString' UNION ALL<br />SELECT 23, N'real', N'single', N'Single, Nullable<Single>', N'SqlSingle' UNION ALL<br />SELECT 24, N'rowversion', N'byte[]', N'Byte[]', NULL UNION ALL<br />SELECT 25, N'smallint', N'smallint', N'Int16, Nullable<Int16>', N'SqlInt16' UNION ALL<br />SELECT 26, N'smallmoney', N'decimal', N'Decimal, Nullable<Decimal>', N'SqlMoney' UNION ALL<br />SELECT 27, N'sql_variant', N'object', N'Object', NULL UNION ALL<br />SELECT 28, N'table', NULL, NULL, NULL UNION ALL<br />SELECT 29, N'text', N'string', NULL, NULL UNION ALL<br />SELECT 30, N'time', N'TimeSpan', N'TimeSpan, Nullable<TimeSpan>', N'TimeSpan' UNION ALL<br />SELECT 31, N'timestamp', NULL, NULL, NULL UNION ALL<br />SELECT 32, N'tinyint', N'byte', N'Byte, Nullable<Byte>', N'SqlByte' UNION ALL<br />SELECT 33, N'uniqueidentifier', N'Guid', N'Guid, Nullable<Guid>', N'SqlGuidUser-defined type(UDT)The same class that is bound to the user-defined type in the same assembly or a dependent assembly.' UNION ALL<br />SELECT 34, N'varbinary ', N'byte[]', N'Byte[]', N'SqlBytes, SqlBinary' UNION ALL<br />SELECT 35, N'varbinary(1), binary(1)', N'byte', N'byte, Byte[], Nullable<byte>', N'SqlBytes, SqlBinary' UNION ALL<br />SELECT 36, N'varchar', N'string', N'String, Char[]', N'SqlChars, SqlStrinG SQLChars is a better match for data transfer and access, and SQLString is a better match for performing String operations.' UNION ALL<br />SELECT 37, N'xml', NULL, NULL, N'SqlXml'<br />COMMIT;<br />RAISERROR (N'[dbo].[DbVsCSharpTypes]: Insert Batch: 1.....Done!', 10, 1) WITH NOWAIT;<br />GO<br /><br />SET IDENTITY_INSERT [dbo].[DbVsCSharpTypes] OFF;" +
            "</pre><p>Here is what results from the script:</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/14112011_SQL_Server_Table.png\" alt=\"SQL Server Table\" /></div>" +
            "<p>Next, a function that will return the C# type when the SQL Server type is passed to it will be required. It will take it from that table that was just created. This is the script for the function:</p><pre class=\"brush:sql\">" +
            @"/****** Object:  UserDefinedFunction [dbo].[funcGetCLRTypeBySqlType]    <br />Script Date: 03/23/2010 15:25:09 ******/<br />IF  EXISTS (SELECT * FROM sys.objects <br />WHERE object_id = OBJECT_ID(N'[dbo].[funcGetCLRTypeBySqlType]') <br />AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))<br />DROP FUNCTION [dbo].[funcGetCLRTypeBySqlType]<br />GO <br />/****** Object:  UserDefinedFunction [dbo].[funcGetCLRTypeBySqlType]     <br />Script Date: 03/23/2010 15:25:09 ******/<br />SET ANSI_NULLS ON<br />GO <br />SET QUOTED_IDENTIFIER ON<br />GO <br />CREATE FUNCTION [dbo].[funcGetCLRTypeBySqlType]<br />(@SqlType [nvarchar] (200)) <br />RETURNS [varchar](200) <br />WITH EXECUTE AS CALLER<br />AS <br />BEGIN<br />declare @ClrType varchar(200) <br />SET @ClrType = ( SELECT  TOP 1 CSharpDataType FROM DbVsCSharpTypes <br />WHERE Sql2008DataType= @SqlType)<br />-- Return the result of the function<br />RETURN @ClrType END <br />/*<doc>Used for automatic conversation between tsql and C# types </doc>*/<br />GO" +
            "</pre><p>Sample of the usage - nothing hard yet.</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/14112011_funcGetCLRTypeBySQLType.png\" alt=\"funcGetCLRTypeBySQLType\" /></div>" +
            "<p>A small function just because I want my private variable start from a lower case character</p><pre class=\"brush:sql\">" +
            @"set ANSI_NULLS ON<br />set QUOTED_IDENTIFIER ON<br />go<br /><br />CREATE FUNCTION [dbo].[lowerCaseFirstCharacter]<br />(@Input [nvarchar] (200))<br />RETURNS [varchar](200) <br />WITH EXECUTE AS CALLER<br />AS <br />BEGIN<br />declare @Result varchar(200) <br />declare @Len int<br />SET @Len = LEN(@Input)<br />SET @Result = LOWER(SUBSTRING(@Input, 1, 1)) + SUBSTRING(@Input, 2, @Len-1)<br /><br />RETURN @Result <br />END" +
            "</pre><p>And, finally, the stored procedure that generates some C# code:</p><pre class=\"brush:sql\">" +
            @"IF  EXISTS (SELECT * FROM sys.objects <br />WHERE object_id = OBJECT_ID(N'[dbo].[procUtils_GenerateClass]') AND type in (N'P', N'PC'))<br />DROP PROCEDURE [dbo].[procUtils_GenerateClass]<br />GO <br />/****** Object:  StoredProcedure [dbo].[procUtils_GenerateClass]     <br />Script Date: 03/20/2010 13:10:40 ******/<br />SET ANSI_NULLS ON<br />GO <br />SET QUOTED_IDENTIFIER ON<br />GO  <br />CREATE PROCEDURE [dbo].[procUtils_GenerateClass]<br />@TableName [varchar](50)<br />WITH EXECUTE AS CALLER<br />AS<br />BEGIN -- proc start                                      <br />SET NOCOUNT ON;                                       <br />DECLARE @DbName nvarchar(200 ) <br />select @DbName = DB_NAME()               <br />declare @strCode nvarchar(max) <br />set @strCode = ''  <br /><br />BEGIN TRY        --begin try<br />set @strCode = @strCode +  'namespace ' + @DbName + '.Gen' + CHAR(13) + '{' + CHAR(13)<br />set @strCode = @strCode + CHAR(9) + 'public class ' + @TableName + CHAR(13) + CHAR(9) + '{ ' + CHAR(13)<br /><br />DECLARE @ColNames TABLE                                <br />(                                <br />Number [int] IDENTITY(1,1), --Auto incrementing Identity column<br />ColName [varchar](300) , --The string value                        ,         <br />DataType varchar(50) ,  --the datatype                       <br />IS_NULLABLE nvarchar(5) , --should we add =null in front         <br />CHARACTER_MAXIMUM_LENGTH INT        <br />)                                <br />--Decalre a variable to remember the position of the current delimiter                                <br />DECLARE @CurrentDelimiterPositionVar INT                                 <br />DECLARE @PkColName varchar(200)      <br />set @PkColName = ''<br />declare @ColumnName varchar(200)  <br />--Decalre a variable to remember the number of rows in the table                                <br />DECLARE @Count INT                                <br /><br />INSERT INTO @ColNames         <br />SELECT column_name ,  Data_type , IS_NULLABLE , CHARACTER_MAXIMUM_LENGTH  <br />from INFORMATION_SCHEMA.COLUMNS                             <br />where TABLE_NAME=@TableName                                <br />--Initialize the looper variable                                <br />SET @CurrentDelimiterPositionVar = 1                                <br />--Determine the number of rows in the Table                                <br />SELECT @Count=max(Number) from @ColNames                                <br />--A variable to hold the currently selected value from the table                                <br />DECLARE @ColName varchar(300);                                <br />DECLARE @DataType varchar(50)                      <br />DECLARE @IS_NULLABLE VARCHAR(5)        <br />DECLARE @CHARACTER_MAXIMUM_LENGTH INT        <br />--Loop through until all row processing is done              <br />WHILE @CurrentDelimiterPositionVar <= @Count --1st loop        <br />BEGIN                                <br />--Load current value from the Table                                <br />SELECT @ColName = ColName FROM @ColNames         <br />WHERE Number = @CurrentDelimiterPositionVar        <br />SELECT @DataType = DataType FROM @ColNames         <br />WHERE Number = @CurrentDelimiterPositionVar               <br />SELECT @IS_NULLABLE = IS_NULLABLE FROM @ColNames         <br />WHERE Number = @CurrentDelimiterPositionVar                    <br />SELECT @CHARACTER_MAXIMUM_LENGTH = CHARACTER_MAXIMUM_LENGTH FROM @ColNames         <br />WHERE Number = @CurrentDelimiterPositionVar                     <br />-- get the C# type based on the passed sqlType, )( needs the DbVsCSharpTypes table ) <br />set @DataType =( SELECT  dbo.funcGetCLRTypeBySqlType(@DataType) ) <br />IF @IS_NULLABLE = 'YES'        <br />set @DataType = @DataType + '?' <br />DECLARE @varPrivate nvarchar(200) <br />set @varPrivate = '_' + dbo.lowerCaseFirstCharacter(@ColName)<br /><br />--GENERATE THE PRIVATE MEMBER <br />SET @StrCode = @strCode + CHAR(9)+ CHAR(9) + 'private ' + @DataType + ' ' +  @varPrivate + ';' + CHAR(13) + CHAR(13) <br />-- GENERATE THE PUBLIC MEMBER <br />SET @StrCode = @strCode + CHAR(9)+ CHAR(9) + 'public ' + @DataType +  ' ' + @ColName + CHAR(13) + CHAR(9)+ CHAR(9) + '{' + CHAR(13)<br />SET @StrCode = @strCode + CHAR(9) + CHAR(9) + CHAR(9) + 'get { return ' + @varPrivate + ' } ' + CHAR(13)<br />SET @strCode = @strCode + CHAR(9) + CHAR(9) + CHAR(9) + 'set { ' + @varPrivate +' = value ; }' + CHAR(13) <br />SET @strCode = @strCode + CHAR(9) + CHAR(9) + '}' + CHAR(13) <br /><br />if @CurrentDelimiterPositionVar != @Count         <br />SET @StrCode = @StrCode + ''        <br />IF @DataType != 'timestamp'        <br /> <br />set @strCode = @strCode + char(13)     <br />SET @CurrentDelimiterPositionVar = @CurrentDelimiterPositionVar + 1;                                <br />END                                <br />set @strCode = + @strCode + char(9) + ' } //class ' + @TableName + CHAR(13)<br />set @strCode = + @strCode + ' } //namespace  ' + CHAR(13)<br /><br />PRINT @strCode<br />END TRY                                  <br />BEGIN CATCH                                  <br />print ' Error number: ' + CAST(ERROR_NUMBER() AS varchar(100)) +                         <br />'Error message: ' + ERROR_MESSAGE() + 'Error severity: ' +           <br />CAST(ERROR_SEVERITY() AS varCHAR(9)) +                         <br />'Error state: ' + CAST(ERROR_STATE() AS varchar(100)) +           <br />'XACT_STATE: ' + CAST(XACT_STATE() AS varchar(100))                                  <br />END CATCH                                   <br />END --procedure end                                       <br />/* <doc> Generates a C# class base on DataType conversion</doc>*/<br />GO" +
            "</pre><p>Here's a test table I used to check the results:</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/14112011_Database_Table.png\" alt=\"Database Table\" /></div>" +
            "<p>Here's the stored procedure output sample:</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/14112011_Class_Code.png\" alt=\"Class Code\" /></div>" +
            "<p>References</p><p><a href=\"http://stackoverflow.com/questions/2488383/how-can-i-programatically-convert-sql-data-types-to-net-data-types\">How can I programatically convert SQL data-types to .Net data-types?</a></p><p><a href=\"http://ysgitdiary.blogspot.com/2010/03/how-to-generate-classes-based-on-tables.html\">how-to generate classes based on tables in ms sql 2008</a></p><p><a href=\"https://plus.google.com/113035264903177615519/posts/N4qLkwQPmFE#113035264903177615519/posts/N4qLkwQPmFE\">Function to get the C# type based on the tsql type</a></p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_14112011_k = "C# Software development SQL generate class";
        public const string content_14112011_d = "Generating a C# Class Based on the Underlying SQL";

        //"Understanding Git, GitHub and GitExtensions"
        public const string content_11112011_b = "<p>At this stage I would like learn how to use the GitHub.</p><p><a href=\"https://github.com/\">GitHub - Social Coding</a></p><p>This is useful in case I want to make my code accessible to anyone over the Internet. Let's say I want to show someone the example of my work. I create a GitHub account, use Git as my code repository and somehow synchronise it with GitHub. Then any other person will be able to \"get the latest version\" (or should I say \"pull\"?) of my code via GitHub. Okay, I just summed up all my knowledge about Git and GitHub until today.</p><p>My goal is to be able to commit and update a Visual Studio project, so I looked and found out that everyone recommends GitExtensions.</p>";
        public const string content_11112011_r = "<p><a href=\"http://code.google.com/p/gitextensions/\">Git Extensions</a></p><p>This tool allows to control Git without the command line (a handy thing for a long-time Windows user like me!), works well under Windows and has a plugin for Visual Studio. So far, sounds like a perfect tool for my purposes.</p><p>The first thing I did was created a GitHub account.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/11112011_Git_Plans_and_Pricing.png\" alt=\"Git Plans and Pricing\" /></div>" +
            "<p>And created a new repository</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/11112011_Welcome_to_GitHub.png\" alt=\"Welcome to GitHub\" /></div>" +
            "<p>Next, I installed the GitExtension using default settings, briefly checked the 58-page manual and started the gui. The program automatically checks my settings on startup and it looks all was good - I just had to create a username for myself.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/11112011_Git_Extensions_Settings.png\" alt=\"Git Extensions Settings\" /></div>" +
            "<p>Looked around a little bit and found how to start the Git gui, then look at my repository.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/11112011_Git_Extensions_Running.png\" alt=\"Git Extensions Running\" /></div>" +
            "<p>Found out how to check the items in. Go a rather scary message but decided to ignore it for now and see if it affects anything later.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/11112011_Git_Extensions_Error.png\" alt=\"Git Extensions Error\" /></div>" +
            "<p>For now it looks like my files are in. Next time, I will explore the checking in and out.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/11112011_Created_New_Project.png\" alt=\"Created New Project\" /></div>" +
            "<br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_11112011_k = "software development git windows github GitExtensions";
        public const string content_11112011_d = "Understanding Git, GitHub and GitExtensions";

        //"Flash in WPF Application the MVVM way (the easy part)"
        public const string content_09112011_b = "<p>Now I have to use my WFPFlashLibrary I created in the last post in my main WPF view. I have to add the namespace for the project that contains my Flash control.</p>";
        public const string content_09112011_r = "<pre class=\"brush:csharp\">" + @"xmlns:Flash=""clr-namespace:WPFFlashLibrary;assembly=WPFFlashLibrary""" + "</pre><p>I place my control in the WPF markup and bind Movie and Play.</p><pre class=\"brush:xml\">" + @"&lt;Grid&gt;<br /> &lt;Flash:FlashControl Width=""400"" Height=""400"" Movie=""{Binding Movie,UpdateSourceTrigger=PropertyChanged}"" Play=""{Binding Play,UpdateSourceTrigger=PropertyChanged}"" /&gt;<br />&lt;/Grid&gt;" + "</pre><p>This is the sample code which should be placed in the view. The Init will contain any code that needs to run on the ViewModel creation and will return the instance of the ViewModel. The PlayFlash will be then called right in the constructor of the view for simplicity, but of course it does not have to be there - it can be triggered whenever necessary.</p><pre class=\"brush:csharp\">" +
            @"public partial class TestFlashView : System.Windows.Controls.UserControl<br />{<br /> public TestFlash(IUnityContainer container)<br /> {<br />  InitializeComponent();<br /><br />  DataContext = container.Resolve<TestFlashViewModel>().Init(container);<br />  (DataContext as TestFlashViewModel).PlayFlash();<br /> }<br />}" + "</pre><p>And this is the implementation of the ViewModel. As soon as the PlayFlash() assigns values to the Movie and Play, the control will play the Flash animation (assuming the file is in the specified location!).</p><pre class=\"brush:csharp\">" +
            @"public class TestFlashViewModel : ViewModelBase<br />{<br /> public TestFlashViewModel(IUnityContainer container):base(container)<br /> {<br /><br /> }<br /> <br /> virtual public TestFlashViewModel Init(IUnityContainer container)<br /> {<br />  //initialization - login etc.<br />  return this;<br /> }<br /><br /> //*****************************************************************************************<br /><br /> #region properties<br /><br /> string _movie;<br /> public string Movie<br /> {<br />  get { return _movie; }<br />  set { OnPropertyChanged(ref _movie , value,""Movie""); }<br /> }<br /><br /> bool _play;<br /> public bool Play<br /> {<br />  get { return _play; }<br />  set { OnPropertyChanged(ref _play, value, ""Play""); }<br /> }<br /><br /> #endregion<br /><br /> public void PlayFlash()<br /> {<br />  Movie = @""c:\flash\flash.swf"";<br />  Play = true;<br /> }<br />}" + "</pre><p>And that's the end of my small investigation. Unfortunately I found out that the plans have changed, the scope has been reduced and the flash movie is not required any longer. So I won't play with this control for anymore for now and move on to other things. Still was worth the effort.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_09112011_k = "Software development C# MVVM WPF Flash";
        public const string content_09112011_d = "Flash in WPF Application the MVVM way (the easy part)";

        //"Flash in WPF Application the MVVM way (the hard part)"
        public const string content_05112011_b = "<p>The Flash ActiveX control can not be added directly to the XAML file. Okay, the solution is well-known - just like with the Windows Forms control, you can use WindowsFormsHost to, well, host it - that's what the host is for. Then we add some code to the code-behind to load the movie and play it, and everything works. Right? Sort of. What if I'm trying my best to do the things the MVVM way? My code-behind file is usually empty, and all the business logic happens in the ViewModel. My XAML file does not have any button1_click event handlers, but rather is linked to the ViewModel by binding and is notified when something changes by means of OnPropertyChanged. What to do? The hard part is to come up with something that can be placed into the XAML file. The easy part is to place that something into the XAML file and bind it to the ViewModel. I'll start with the hard part, and use Visual Studio 2010.</p>";
        public const string content_05112011_r = "<p>Let's assume that the user controls are in a separate project. So I create a new project using the \"WPF User Control Library\" template and call it WPFControlLibrary. Let's delete UserControl1.xaml so it doesn't get in the way. First I'll add references to the COM components I may need.</p><p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/05112011_ShockwaveFlashObject.png\" alt=\"ShockwaveFlashObject\" /></div>" +
            "</p><p>Now I add a User Control (not the User Control - WPF yet!) and call it FlashWrapper. I add the AxShockwaveFlash control to it and call it axShockwafeFlash. For now let's worry only about loading and playing a movie. That's all the code I'll need:</p><p><pre class=\"brush:csharp\">" +
            "using System.Windows.Forms;<br /><br />namespace WPFControlLibrary<br />{<br />    public partial class FlashWrapper : UserControl<br />    {<br />        public FlashWrapper()<br />        {<br />            InitializeComponent();<br />        }<br /><br />        public void LoadMovie(string movie)<br />        {<br />            axShockwaveFlash.Movie = movie;<br />        }<br /><br />        public void Play()<br />        {<br />            axShockwaveFlash.Play();<br />        }<br /><br />        public void Stop()<br />        {<br />            axShockwaveFlash.Stop();<br />        }<br />    }<br />}" + "</pre></p><p>Now is the time to add the User Control - WPF. I call it FlashWPF.xaml. The XAML file is where the WindowsFormsHost comes to play - here I will host my FlashWrapper. Don't forget to add a reference to WindowsFormsIntegration!</p><p><pre class=\"brush:xml\">" +
            @"&lt;UserControl x:Class=""WPFControlLibrary.FlashWPF""<br />             xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""<br />             xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""<br />             xmlns:mc=""http://schemas.openxmlformats.org/markup-compatibility/2006"" <br />             xmlns:d=""http://schemas.microsoft.com/expression/blend/2008"" <br />             xmlns:local=""clr-namespace:WPFControlLibrary"" <br />             mc:Ignorable=""d"" <br />             d:DesignHeight=""300"" d:DesignWidth=""300""&gt;<br />    &lt;Grid  &gt;<br />        &lt;WindowsFormsHost &gt;<br />            &lt;WindowsFormsHost.Child&gt;<br />                &lt;local:FlashWrapper x:Name=""FlashPlayer"" /&gt;<br />            &lt;/WindowsFormsHost.Child&gt;<br />        &lt;/WindowsFormsHost&gt;<br />    &lt;/Grid&gt;<br />&lt;/UserControl&gt;" + "</pre></p><p>And still not much of C# code yet.</p><p><pre class=\"brush:csharp\">" +
            @"using System.Windows.Controls;<br /><br />namespace WPFControlLibrary<br />{<br />    public partial class FlashWPF : UserControl<br />    {<br />        public FlashWPF()<br />        {<br />            InitializeComponent();<br />        }<br /><br />        public void LoadMovie(string movie)<br />        {<br />            FlashPlayer.LoadMovie(movie);<br />        }<br /><br />        public void Play(bool value)<br />        {<br />            if (value)<br />                FlashPlayer.Play();<br />            else<br />                FlashPlayer.Stop();<br />        }<br />    }<br />}" + "</pre></p><p>Last, and the most complex and cryptic step is to add a custom control. This will be a link between the MVVM application and the WPF control which hosts the wrapper which wraps the ActiveX control.</p><p>This is the ViewModel.<br />That is bound to the WPF custom control.<br />That hosts the C# wrapper.<br />That wraps the ActiveX control.<br />That plays the movie.</p><p>I think I got carried away a bit.</p><p>Ok, let's add a Custom Control - WPF and call it FlashControl. That's how it looks if all was done correctly:</p><p><pre class=\"brush:csharp\">" +
            @"public class FlashControl : Control<br />{<br /> static FlashControl()<br /> {<br />  DefaultStyleKeyProperty.OverrideMetadata(typeof(FlashControl), new FrameworkPropertyMetadata(typeof(FlashControl)));<br /> }<br />}" + "</pre></p><p>I have to modify it to expose three DependencyProperties: Movie, Play, and finally itself so it can be created in the MVVM application. And this is the end result:</p><p><pre class=\"brush:csharp\">" +
            @"public class FlashControl : Control<br />{<br /> static FlashControl()<br /> {<br />  DefaultStyleKeyProperty.OverrideMetadata(typeof(FlashControl), new FrameworkPropertyMetadata(typeof(FlashControl)));<br /> }<br /><br /> public FlashControl()<br /> {<br />  FlashPlayer = new FlashWPF();<br /> }<br /><br /> //*****************************************************************************************<br /><br /> //Movie property definition<br /><br /> public static readonly DependencyProperty MovieProperty = DependencyProperty.RegisterAttached(""Movie"", typeof(string), typeof(FlashControl), new PropertyMetadata(MovieChanged));<br /><br /> private static void MovieChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)<br /> {<br />  (d as FlashControl).Movie = (string)e.NewValue;<br /> }<br /><br /> //Play movie property definition<br /> public static readonly DependencyProperty PlayProperty = DependencyProperty.RegisterAttached(""Play"", typeof(bool), typeof(FlashControl), new PropertyMetadata(PlayChanged));<br /><br /> private static void PlayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)<br /> {<br />  (d as FlashControl).Play = (bool)e.NewValue;<br /> }<br /><br /> //Flash player WindowFormHost<br /> public static readonly DependencyProperty FlashPlayerProperty = DependencyProperty.RegisterAttached(""FlashPlayer"", typeof(FlashWPF), typeof(FlashControl), new PropertyMetadata(FlashPlayerChanged));<br /><br /> private static void FlashPlayerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)<br /> {<br />  (d as FlashControl).FlashPlayer = (FlashWPF)e.NewValue;<br /> }<br /><br /> //*****************************************************************************************<br /><br /> public string Movie<br /> {<br />  get { return (string)this.GetValue(MovieProperty); }<br />  set <br />  { <br />   this.SetValue(MovieProperty, value);<br />   FlashPlayer.LoadMovie(value);<br />  }<br /> }<br /><br /> public bool Play<br /> {<br />  get { return (bool)this.GetValue(PlayProperty); }<br />  set <br />  { <br />   this.SetValue(PlayProperty, value);<br />   FlashPlayer.Play(value);<br />  }<br /> }<br /><br /> public FlashWPF FlashPlayer<br /> {<br />  get { return (FlashWPF)this.GetValue(FlashPlayerProperty); }<br />  set { this.SetValue(FlashPlayerProperty, value); }<br /> }<br />}" +
            "</pre></p><p>And the XAML file (which is for some reason called Generic.xaml and also when I tried to rename it I started getting errors, so I decided to leave the name alone) - I modified it slightly, but that's a matter of personal preference:</p><p><pre class=\"brush:xml\">" +
            @"&lt;ResourceDictionary<br />    xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""<br />    xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""<br />    xmlns:local=""clr-namespace:WPFControlLibrary""&gt;<br />    &lt;Style TargetType=""{x:Type local:FlashControl}""&gt;<br />        &lt;Setter Property=""Template""&gt;<br />            &lt;Setter.Value&gt;<br />                &lt;ControlTemplate TargetType=""{x:Type local:FlashControl}""&gt;<br />                    &lt;Grid HorizontalAlignment=""Stretch"" VerticalAlignment=""Stretch""&gt;<br />                        &lt;ContentControl Content=""{TemplateBinding FlashPlayer}"" /&gt;<br />                    &lt;/Grid&gt;<br />                &lt;/ControlTemplate&gt;<br />            &lt;/Setter.Value&gt;<br />        &lt;/Setter&gt;<br />    &lt;/Style&gt;<br />&lt;/ResourceDictionary&gt;" + "</pre></p><p>And the hard part is over!</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_05112011_k = "Software development C# MVVM WPF Flash";
        public const string content_05112011_d = "Flash in WPF Application the MVVM way (the hard part)";

        //"Small Things Learned Today"
        public const string content_03112011_b = "<p>While writing the previous post, I had to find out how to show the special HTML characters, like <strong>&amp;nbsp;</strong></p><p>Because if I just type <strong>&amp;nbsp;</strong> I will only see the space. The browser will automatically convert the <strong>&amp;nbsp;</strong> to space, the <strong>&amp;amp;</strong> to an ampersand and so on. But I wanted to display exactly what I had in my XML.</p>";
        public const string content_03112011_r = "<p>So, to display <strong>&amp;nbsp;</strong> just as it is, I have to type <strong>&amp;amp;nbsp;</strong> in my post. <strong>&amp;amp;</strong> gets converted to ampersand and the <strong>nbsp;</strong> part is just displayed as it is.</p><p>What did I type to display <strong>&amp;amp;nbsp;</strong> in the line above? Well, <strong>&amp;amp;amp;nbsp;</strong> of course. I'd better stop here before I confuse myself and everyone else.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_03112011_k = "web development HTML special characters";
        public const string content_03112011_d = "Showing special HTML characters in markup";

        //"A Confusing Issue with the Ampersand in the XML"
        public const string content_02112011_b = "<p>Everyone knows that some symbols such as <strong>&lt;</strong>, <strong>&gt;</strong>, <strong>&amp;</strong>, <strong>&quot;</strong> \"break\" the XML. That's why Server.HtmlEncode is used to replace them with correct HTML code, like <strong>&amp;amp;</strong> for the ampersand and so on. After that replacement the XML is supposed to be \"safe\". However, not under every possible condition. One of the applications I work on prints barcodes on tickets. The number is encoded using Interleaved 2 of 5 format. The encoding is performed by a function provided by the company IDAutomation and the output generally looks like this: <strong>\"Ë'Zj`!/ÉI?!&!Ì\"</strong>. The output is then passed through the Server.HtmlEncode and added to the XML, which is fed to a printer.</p>";
        public const string content_02112011_r = "<p>However, yesterday I received a bug report and the error essentially boiled down to</p><blockquote>Type : System.Xml.XmlException, System.Xml, Version=2.0.0.0, Culture=neutral, PublicKeyToken=blah Message : An error occurred while parsing EntityName. Line 1, position 2832.<br />Source : System.Xml<br />Help link :<br />LineNumber : 1<br />LinePosition : 2832<br />SourceUri :<br />Data : System.Collections.ListDictionaryInternal<br />TargetSite : Void Throw(System.Exception)<br />Stack Trace :    at System.Xml.XmlTextReaderImpl.Throw(Exception e)<br />   at System.Xml.XmlTextReaderImpl.Throw(String res, String arg)<br />   at System.Xml.XmlTextReaderImpl.Throw(String res)<br />   at System.Xml.XmlTextReaderImpl.ParseEntityName()<br />   at System.Xml.XmlTextReaderImpl.ParseEntityReference()<br />   at System.Xml.XmlTextReaderImpl.Read()<br />   at System.Xml.XmlLoader.LoadNode(Boolean skipOverWhitespace)<br />   at System.Xml.XmlLoader.LoadDocSequence(XmlDocument parentDoc)<br />   at System.Xml.XmlLoader.Load(XmlDocument doc, XmlReader reader, Boolean preserveWhitespace)<br />   at System.Xml.XmlDocument.Load(XmlReader reader)<br />   at System.Xml.XmlDocument.LoadXml(String xml)<br />   at PrintController.AddDoc(String xmlString)</blockquote><p>And, fortunately, I got two XML samples, one of those was causing an error, and the other one was not.</p><p>This bit in the encoded XML did not cause any problems:</p><pre class=\"brush:xml\">" + @"&lt;text x=""centre"" y=""620"" font=""IDAutomationHI25L"" size=""20"" bold=""false"" italic=""false"" underline=""false""&gt;<br />&amp;#203;'Zj`!/&amp;#201;I5!'!&amp;#204;&lt;/text&gt;" + "</pre><p>This one, however, did, regardless of the \"safely encoded\" ampersand</p><pre class=\"brush:xml\">" +
            @"&lt;text x=""centre"" y=""620"" font=""IDAutomationHI25L"" size=""20"" bold=""false"" italic=""false"" underline=""false""&gt;<br />&amp;#203;'Zj`!/&amp;#201;I?!&amp;amp;!&amp;#204;&lt;/text&gt;" + "</pre><p>Solution? I had to think about it and that's what I came up with.</p><p><pre class=\"brush:vb\">" + @"xmlData = xmlData.Replace(""barcode"", Server.HtmlEncode(mybarcode).Replace(""&amp;amp;"", ""&amp;#038;""))" + "</pre></p><p>Because <strong>\"&amp;#038;\"</strong> is the HTML ASCII value for <strong>\"&\"</strong>. And it worked like a charm. Now I just need to convert it to a small function instead which takes care of all \"strange\" characters: <, >, & and \"</p><p>References</p><p><a href=\"http://msdn.microsoft.com/en-us/library/ms525347(v=vs.90).aspx\">Server.HTMLEncode Method</a></p><p><a href=\"http://www.nationalfinder.com/html/char-asc.htm\">HTML ASCII Characters</a></p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_02112011_k = "xml ampersand web html";
        public const string content_02112011_d = "A Confusing Issue with the Ampersand in the XML";

        //"Setting up a Composite WPF Application Properly"
        public const string content_25102011_b = "<p>It's time to step back from working on bits and pieces of something large and remind myself how to create a MVVM project from scratch. There are probably templates available now but I'll start with an empty C# WPF project and call it MVVM for simplicity. Renamed MainWindow to Shell everywhere in the solution because Shell is the conventional name for a top-level window in the application built in the Composite Application Library.</p><p>Composite Application Library displays and hides controls through the use of Regions. Several controls can be displayed as regions, one of them is ItemsControl.</p><p>An attached property RegionManager.RegionName indicates which region is associated with the control. So, in the Shell.xaml I replaced the Grid element with the ItemsControl element:</p>";
        public const string content_25102011_r = "<pre class=\"brush:xml\">" + @"&lt;ItemsControl Name=""MainRegion"" cal:RegionManager.RegionName=""MainRegion""/&gt;" + "</pre><p>For the code to compile, the following line has to be added to the Window tag</p><pre class=\"brush:xml\">" +
            @"xmlns:cal=""http://www.codeplex.com/CompositeWPF""" + "</pre><p>and the Microsoft.Practices.Prism dll has to be referenced in the project which contains the definition of RegionManager.RegionName</p><p>The bootstrapper initializes the application build using the Composite Application Library. At this point the bootstrapper only returns the new instance of a Shell class. Added the Bootstrapper class to the solution:</p><pre class=\"brush:csharp\">" +
            @"class Bootstrapper : UnityBootstrapper<br />{<br /> protected override DependencyObject CreateShell()<br /> {<br />  return this.Container.Resolve<Shell>();<br /> }<br /><br /> protected override void InitializeShell()<br /> {<br />  base.InitializeShell();<br /><br />  App.Current.MainWindow = (Window)this.Shell;<br />  App.Current.MainWindow.Show();<br /> }<br /><br /> protected override void ConfigureModuleCatalog()<br /> {<br /> }<br />}" + "</pre><p>At this point, the reference to Microsoft.Practices.Prism.UnityExtensions is required (for UnityBootstrapper). And not to forget the</p><pre class=\"brush:csharp\">" +
            @"using Microsoft.Practices.Unity;" + "</pre><p>otherwise the container would not resolve! Basically, Container.Resolve constructs an instance of the concrete class, resolving any dependencies that it has.</p><p>Now the bootstrapper has to run when the application starts. To achieve this, the Startup event of the application is handled in the App.xaml.cs file.</p><pre class=\"brush:csharp\">" +
            @"public partial class App : Application<br />{<br /> protected override void OnStartup(StartupEventArgs e)<br /> {<br />  base.OnStartup(e);<br />  Bootstrapper bootstrapper = new Bootstrapper();<br />  bootstrapper.Run();<br /> }<br />}" + "</pre><p>And since the instance of the Shell is manually created by the bootstrapper, the StartupUri attribute is not needed anymore in the App.xaml Application tag.</p><pre class=\"brush:xml\">" + @"&lt;Application x:Class=""MVVM.App""<br />             xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""<br />             xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml""&gt;<br />    &lt;Application.Resources&gt;<br />         <br />    &lt;/Application.Resources&gt;<br />&lt;/Application&gt;" + "</pre><p>The application is fully functional now!</p><p>References:</p><p><a href=\"http://msdn.microsoft.com/en-us/library/ff921141(v=PandP.20).aspx\">WPF Hands-On Lab: Getting Started with the Composite Application Library</a></p><p><a href=\"http://stackoverflow.com/questions/634459/what-does-this-mean-in-prism-unity-container-resolveshellpresenter\">What does this mean in Prism/Unity: Container.Resolve<ShellPresenter>()</a></p><p><a href=\"http://msdn.microsoft.com/en-us/library/microsoft.practices.composite.unityextensions.unitybootstrapper.aspx\">UnityBootstrapper Class</a></p><p><a href=\"http://msdn.microsoft.com/en-us/library/ff921139(v=pandp.20).aspx\">Bootstrapper</a></p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_25102011_k = "C# WPF software development correct applicaiton set up";
        public const string content_25102011_d = "Setting up a Composite WPF Application Properly";

        //"Small Things Learned Today"
        public const string content_24102011_b = "<strong><p>Drop a Default Constraint on a Table Before Dropping the Table.</p></strong><p>Trying to drop the colum from a database via a simple script</p>";
        public const string content_24102011_r = "<p><pre class=\"brush:sql\">" + @"ALTER TABLE [dbo].MYTABLENAME DROP COLUMN MYCOLUMNNAME" + "</pre></p><p>I came across the error I did not quite expect.</p><p><pre class=\"brush:sql\">" + @"Msg 5074, Level 16, State 1, Line 1<br />The object 'DF__MYTABLENAME__MYCOL__42ACE4D4' is dependent on column 'MYCOLUMNNAME'.<br />Msg 4922, Level 16, State 9, Line 1<br />ALTER TABLE DROP COLUMN MYCOLUMNNAME failed because one or more objects access this column." +
            "</pre></p><p>Turns out it is not possible to drop the column this way because there is a default constraint set on it.<br />However, the constraint name is 'DF__MYTABLENAME__MYCOL__42ACE4D4', which is fine if I'm doing it in the database I have full access to. I could even use the visual tool like Management Studio to just right-click and drop.</p><p>That's not possible, however, in my case, because the database is in the remote location and I need to write a script that I can send over and ask the person in charge to run it. I don't want to ask him 'hey, could you please run sp_help on the table MYTABLENAME and let me know what it tells you'.<br />That's where this little script comes handy:</p><p><pre class=\"brush:sql\">" +
            @"declare @default sysname, @sql nvarchar(max)<br /><br />select @default = name <br />from sys.default_constraints <br />where parent_object_id = object_id('MYTABLENAME')<br />AND type = 'D'<br />AND parent_column_id = (<br /> select column_id <br /> from sys.columns <br /> where object_id = object_id('MYTABLENAME')<br /> and name = 'MYCOLUMNNAME'<br /> )<br /><br />set @sql = N'alter table MYTABLENAME drop constraint ' + @default<br />exec sp_executesql @sql<br /><br />alter table MYTABLENAME drop column MYCOLUMNNAME<br /><br />go" + "</pre></p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_24102011_k = "Drop a Default Constraint on a Table Before Dropping the Table";
        public const string content_24102011_d = "Drop a Default Constraint on a Table Before Dropping the Table";

        //"WPF Commands Part 1"
        public const string content_16102011_b = "<p>I'm working on deeper understanding on how commands work in WPF. How do RoutedCommands work? First, I add a class to my solution and call it Commands.</p>";
        public const string content_16102011_r = "<pre class=\"brush:csharp\">" +
            @"public static class Commands<br />{<br /> public static readonly RoutedCommand MyClick = new RoutedCommand();<br />}" +
            "</pre><p>Next, I declare the command and add a binding in my User Control.</p><pre class=\"brush:xml\">" +
            @"&lt;Button <br /> Command=""{x:Static my:Commands.MyClick}"" <br /> Grid.Column=""2"" Height=""23"" HorizontalAlignment=""Left"" Margin=""0"" <br /> Name=""button1"" VerticalAlignment=""Top"" width=\""75"" Content=""Button""/&gt;<br />   <br />&lt;UserControl.CommandBindings&gt;<br /> &lt;CommandBinding <br />  Command=""{x:Static my:Commands.MyClick}"" <br />  CanExecute=""CommandBinding_CanExecute"" <br />  Executed=""CommandBinding_Executed""&gt;<br /> &lt;/CommandBinding&gt;       <br />&lt;/UserControl.CommandBindings&gt;" +
            "</pre><p>Next, I add the handlers for the CommandBinding_CanExecute and CommandBinding_Executed.</p><pre class=\"brush:csharp\">" +
            @"private void CommandBinding_CanExecute(object sender, System.Windows.Input.CanExecuteRoutedEventArgs e)<br />{<br /> e.CanExecute = true;<br />}<br /><br />private void CommandBinding_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)<br />{<br /> System.Windows.Forms.MessageBox.Show(""Executed in DimensionsView"");<br />}" +
            "</pre><p>So far, so good - the button is clicked, the message appears. However, that's too simple - next, I need to make sure my other user control will be notified when the button is clicked. This is the bit I was not able to do quickly on my first attempt. It appears that I need to implement a MVVM pattern to achieve this.</p><p>Fist I will implement the pattern in the simplest possible way. I'll create a ViewModel for my User Control. The ViewModel provides the mechanics of creating a command. The two functions serve the following purpose: CanClick() checks if the button click can happen. Some logic can be placed there and, if it returns false, the button will in fact be disabled. DoClick() actually runs the code that should happen on button click.</p><pre class=\"brush:csharp\">" +
            @"public class DimensionsViewModel<br />{<br /> private DelegateCommand _clickCommand;<br /><br /> public ICommand ClickCommand<br /> {<br />  get<br />  {<br />   if (_clickCommand == null)<br />   {<br />    _clickCommand = new DelegateCommand(new Action(DoClick), new Func<bool>(CanClick));<br />   }<br />   return _clickCommand;<br />  }<br /> }<br /><br /> private bool CanClick()<br /> {<br />  /* code to check if the button can be clicked */<br />  return true;<br /> }<br /><br /> private void DoClick()<br /> {<br />  System.Windows.Forms.MessageBox.Show(""Click in DimensionsView"");<br /> }<br />}" +
            "</pre><p>The Button in the xaml file needs to know which command to run.</p><pre class=\"brush:xml\">" +
            @"&lt;Button <br />    Command=""{Binding Path=ClickCommand}""<br />    Grid.Column=""2"" Height=""23"" HorizontalAlignment=""Left"" Margin=""0"" <br />    Name=""button1"" VerticalAlignment=""Top"" width=\""75"" Content=""Button""/&gt;&lt;/pre&gt;&lt;p&gt;" +
            "And finally, the User Control needs to know which ViewModel to use:&lt;/p&gt;&lt;pre class=\"brush:xaml\"&gt;&lt;" +
            @"UserControl x:Class=""DynamicGrid.DimensionsView""<br /><br /> ...<br />    xmlns:my=""clr-namespace:DynamicGrid""&gt;<br /> &lt;UserControl.DataContext&gt;<br />  &lt;my:DimensionsViewModel/&gt;<br /> &lt;/UserControl.DataContext&gt;<br /> ...<br />&lt;/UserControl&gt;" +
            "</pre><p>And this is it! What I have achieved so far: there is absolutely no code in the xaml.cs file. Here's proof, the whole contents of the DimensionsView.xaml.cs:</p><pre class=\"brush:csharp\">" +
            @"public partial class DimensionsView : UserControl<br />{<br /> public DimensionsView()<br /> {<br />  InitializeComponent();<br /> }<br />}" +
            "</pre><p>Now someone can work on the UI and edit the xaml file, and someone can work on the business logic and work with the ViewModel, and they would not even need to check out the same file and then merge their changes or bump into one another in any way.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_16102011_k = "Software development C# WPF commands";
        public const string content_16102011_d = "Studying WPF Commands";

        //"Using the EventAggregator"
        public const string content_13102011_b = "<p>The EventAggregator is the pattern that may be used in multiple scenarios, but in my case I have a WPF application that uses several User Control. When an action happens in a particular User Control (i.e. a user clicks the button), another User Controls or several User Controls react (i.e. draw an image). At first it looked like Routed Events and Commands are the way to go but I found out that could not easily build a working prototype so I decided to learn them at a later stage.</p>";
        public const string content_13102011_r = "<p>I found the explanation and a sample implementation here:</p><p><a href=\"http://www.minddriven.de/index.php/technology/development/design-patterns/event-aggregator-implementation\">Simple message-based Event Aggregator</a></p><p>My prototype included two user controls, and I used the implementation of IEventAggregator and ISubscription from the link above.</p><p>That's the solution structure:</p><p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/13102011_EventAggregator.png\" alt=\"EventAggregator\" /></div>" +
            "</p><p>The user control that publishes the event does it in the following way:</p><pre class=\"brush:csharp\">" +
            @"public partial class DimensionsView : UserControl<br />    {<br />        private EventAggregator _eventAggregator = null;<br /><br />        private EventAggregator EventAggregator<br />        {<br />            get <br />            {<br />                if (_eventAggregator == null)<br />                {<br />                    _eventAggregator = new EventAggregator();<br />                }<br />                return _eventAggregator;<br />            }<br />        }<br /><br />        public DimensionsView()<br />        {<br />            InitializeComponent();<br />        }<br /><br />        private void button1_Click(object sender, RoutedEventArgs e)<br />        {<br />            //publish a message that will be subscribed to in another user control<br />            EventAggregatorInstance.EAggregator.Publish(new BasicMessage(""test""));<br />        }</pre>" +
            "<p>And the subscribing user control declares an action it's interested in explicitly and subscribes to it.</p><pre class=\"brush:csharp\">" +
            @"public partial class GridView : UserControl<br />    {<br />        private Action<BasicMessage> someAction;<br /><br />        private EventAggregator _eventAggregator = null;<br /><br />        private EventAggregator EvengAggregator<br />        {<br />            get <br />            {<br />                if (_eventAggregator == null)<br />                {<br />                    _eventAggregator = new EventAggregator();<br />                }<br />                return _eventAggregator;<br />            }<br />        }<br /><br />        public GridView()<br />        {<br />            InitializeComponent();<br />            someAction = message => Test(message);<br />            var subscription = EventAggregatorInstance.singletonAggregator.Subscribe(someAction);<br />        }<br /><br />        private void Test(BasicMessage msg)<br />        {<br />            PerformAction();<br />        }<br /><br />        public void PerformAction()<br />        {<br />            System.Windows.Forms.MessageBox.Show(""Action Performed"");<br />        }<br />    }" +
            "</pre><p>That's pretty much all that's required to build a working prototype. Of course, an instance of the EventAggregator has to be created for the application to work. This can be done in multiple ways, I chose one of the simplest - a singleton.</p><pre class=\"brush:csharp\">" +
            @"public static class EventAggregatorInstance<br />    {<br />        public static EventAggregator EAggregator = null;<br /><br />        public static EventAggregator singletonAggregator<br />        {<br />            get<br />            {<br />                if (EAggregator == null)<br />                {<br />                    EAggregator = new EventAggregator();<br />                }<br />                return EAggregator;<br />            }<br />        }<br />    }" +
            "</pre><p>I haven't given up on the Routed Events and Commands, that's my next subject to explore!</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_13102011_k = "C# software development EventAggregator WPF";
        public const string content_13102011_d = "Using the EventAggregator";

        //"First Encounter with Windows 7"
        public const string content_12102011_b = "<p>I need to make sure that a couple of internal applications are working properly under Windows 7. That includes installing all required hardware drivers and documenting the process. It has been reasonably frustrating so far since not many drivers are installed without problems and some COM components are not registered properly and in general the process is very far from smooth. I don't think I've ever dealt with so many errors in one day, here's a subset. (There's nothing particularly interesting here except they all happened today under various circumstances).</p>";
        public const string content_12102011_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/12102011_The_Procedure_Entry_Point.png\" alt=\"The Procedure Entry Point\" /></div>" +
            "<br /><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/12102011_Smart_Card_Reader.png\" alt=\"Smart Card Reader\" /></div>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/12102011_Card_Printer.png\" alt=\"Card Printer\" /></div>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/12102011_Card_Printer_2.png\" alt=\"Card Printer 2\" /></div>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/12102011_NUnit.png\" alt=\"NUnit\" /></div>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/12102011_System_Exception.png\" alt=\"System Exception\" /></div>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/12102011_System_Exception_2.png\" alt=\"System Exception 2\" /></div>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/12102011_File_Not_Found_Exception.png\" alt=\"File Not Found Exception\" /></div><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_12102011_k = "starting windows 7";
        public const string content_12102011_d = "First Encounter with Windows 7";

        //"Customising the app.config file during installation"
        public const string content_11102011_b = "<p>Since we have several environments - development, testing etc., and each environment uses a separate web service it is helpful to be able to specify which environment to use as an installation step. The alternative is to manually edit the app.config whenever needed.</p><p>Turns out it can be easily done (in just about 10 minutes if you know how to, plus some additional time to find the relevant information) using the Visual Studio Setup and Deployment project. Here's my walkthrough, I'm still using Visual Studio 2005:</p>";
        public const string content_11102011_r = "<p>- Under \"Setup and Deployment project\", in the \"Solution Explorer\", select \"View->User Interface\".</p><p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/11102011_User_Interface.png\" alt=\"User Interface\" /></div>" +
            "</p><p>- Right-click \"Start\", select \"Add Dialog\", select the appropriate dialog. In my case, I chose \"RadioButtons - 4 buttons\".</p><p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/11102011_Add_Dialog.png\" alt=\"Add Dialog\" /></div>" +
            "</p><p>- Set the properties for the dialog. These are mostly self-explanatory: ButtonXLabel is what the user will see, ButtonXValue is what the ButtonProperty will be set to if this option is selected.</p><p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/11102011_Properties.png\" alt=\"Properties\" /></div>" +
            "</p><p>- Move the \"RadioButtons\" element up in the tree to the position just after \"Welcome\"</p><p>- Under \"Setup and Deployment project\", in the \"Solution Explorer\", select \"View->Custom Actions\"</p><p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/11102011_Custom_Actions.png\" alt=\"Custom Actions\" /></div>" +
            "</p><p>- Right-click \"Custom Actions\", select \"Add Custom Action\". Navigate to Application Folder and select \"Add Output\". Select Primary Output for the main project.</p><p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/11102011_Add_Project_Output_Group.png\" alt=\"Add Project Output Group\" /></div>" +
            "</p><p>- Set the CustomActionData to the value similar to the one in the screenshot. TARGETENV is what ButtonPropery is called, and the custom action assigns it to the TargetEnv</p><p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/11102011_Custom_Action_Data.png\" alt=\"Custom Action Data\" /></div>" +
            "</p><p>That's it for the Setup and Deployment project.</p><p>Now to the code part. The section of my app.config file which I need looks like that:</p><pre class=\"brush:csharp\">" +
            @"<!-- Web Service --><br />  <add key=""URL"" value=""http://test/MyWS.asmx"" /><br />  <add key=""URLLive"" value=""http://live/MyWS.asmx"" /><br />  <add key=""URLTest"" value=""http://test/MyWS.asmx"" /><br />  <add key=""URLSQA"" value=""http://sqa/MyWS.asmx"" /><br />  <add key=""URLDev"" value=""http://dev/MyWS.asmx"" />" + "</pre><p>I just need to read the appropriate value of the web service url and overwrite the \"URL\" setting with it. For that, I need the installer class.</p><p>- Right-click the main project and select \"Add -> New Item\". Add the Installer Class from the dialog window.</p><p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/11102011_Add_New_Item.png\" alt=\"Add New Item\" /></div>" +
            "</p><p>- In the installer class, I need to override the Install function. Again, mostly self-explanatory. Read the value of the target directory and the target value, open the app.config file, read the appropriate setting from it, assign the correct value to the setting and save the configuration file. Build and run.</p><pre class=\"brush:csharp\">" +
            @"<br />    [RunInstaller(true)]<br />    public partial class MyInstaller : Installer<br />    {<br />        public MyInstaller()<br />        {<br />            InitializeComponent();<br />        }<br /><br />        public override void Install(System.Collections.IDictionary stateSaver)<br />        {<br />            base.Install(stateSaver);<br /><br />            string targetDirectory = Context.Parameters[""TARGETDIR""];<br />            string buttonValue = Context.Parameters[""TargetEnv""];<br />            string exePath = string.Format(""{0}SetupSampleProject.exe"", targetDirectory);<br />            Configuration config = ConfigurationManager.OpenExeConfiguration(exePath);<br /><br />            string url = string.Empty;<br /><br />            switch(buttonValue)<br />            {<br />                case ""LIVE"":<br />                    url = config.AppSettings.Settings[""URLLive""].Value;<br />                    break;<br />                case ""TEST"":<br />                    url = config.AppSettings.Settings[""URLTest""].Value;<br />                    break;<br />                case ""SQA"":<br />                    url = config.AppSettings.Settings[""URLSQA""].Value;<br />                    break;<br />                case ""DEV"":<br />                    url = config.AppSettings.Settings[""URLDev""].Value;<br />                    break;<br />            }<br /><br />            config.AppSettings.Settings[""URL""].Value = url;<br /><br />            config.Save();<br />        }<br />    }<br />" + "</pre><p>This is what the user will see during installation (well, obviously I missed the text labels on the dialog, but the rest is working).</p><p><a href=\"http://2.bp.blogspot.com/-9WDo53k-cz4/TpQVJBcvn5I/AAAAAAAAAMs/vk8NGcNDUcY/s1600/8.PNG\"><img style=\"display:block; margin:0px auto 10px; text-align:center;cursor:pointer; cursor:hand;width: 320px; height: 259px;\" src=\"http://2.bp.blogspot.com/-9WDo53k-cz4/TpQVJBcvn5I/AAAAAAAAAMs/vk8NGcNDUcY/s320/8.PNG\" border=\"0\" alt=\"\"id=\"BLOGGER_PHOTO_ID_5662173876263559058\" /></a></p><p>References:</p><p><a href=\"http://msdn.microsoft.com/en-us/library/taa17f7s(v=VS.80).aspx\">RadioButtons User Interface Dialog Box</a></p><p>- Does not really give any more information compared to what is already visible in the IDE.</p><p><a href=\"http://devcity.net/Articles/339/1/article.aspx\">Installer Class and Custom Actions</a></p><p>- Good, but spread over several pages and really gives too much detail for this simple task</p><p><a href=\"http://raquila.com/software/configure-app-config-application-settings-during-msi-install/\">Configure App.config Application Settings During MSI Install</a></p><p>- Brief and informative, exactly what I needed.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_11102011_k = "software development app.config install customize";
        public const string content_11102011_d = "Customising the app.config file during installation";

        //"I broke things, so now I will jiggle things randomly until they unbreak"
        public const string content_10102011_b = "<p>I came across this great quote from Linus Torwalds (not even a quote, just more of a random thing he said):</p><p>This kind of \"I broke things, so now I will jiggle things randomly until they unbreak\" is not acceptable.</p>";
        public const string content_10102011_r = "<p><a href=\"http://thread.gmane.org/gmane.linux.kernel/1126136\">Subject: Re: Linux 2.6.39-rc3</a></p><p>I feel a bit sad because that's what I'm doing a lot recently - \"Oh, the API fails when I try to get visible image in JPG format? Well, how about infrared in BMP format? Oh, the API fails when I ask for a string? Well, I can live with a byte array, I'll do the conversion myself\".</p><p>That's a bad practice and I'm trying not to do that - my only excuse is the poor documentation supplied with third-party API's and SDK's.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_10102011_k = "random post";
        public const string content_10102011_d = "I broke things, so now I will jiggle things randomly until they unbreak";

        //"An Application Needs to Create An Event Source in the Event Log"
        public const string content_04102011_b = "<p>Spent about 6 hours today resolving the issue about writing events into the custom Event Log. Usual deal - application happily writes everything I ask it to write on my PC, but of course the client installs the application and nothing is logged. Turns out that the issue is well researched and is happening when the application is running under the account that does not have permissions to the EventLog entry in the registry, which can be verified by checking the \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\Eventlog\" node permissions</p>";
        public const string content_04102011_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/04102011_Registry_Editor.png\" alt=\"Registry Editor\" /></div>" +
            "<p>The custom event log is not created and the events are not getting logged but there is no exception raised so it's not obvious at the first glance what is happening. And I had to obtain a separate test login to replicate the behaviour.</p><p>There are several approaches and details may vary.<ul><li>The easiest and probably the most dangerous is to just give the users full control to the registry key.</li><li>Or you could just tell the user to only run the application when a member of Administrators group is logged on.</li><li>Or you could create the event log and source manually or using some tool for each installation.</li><li>A more proper and not so straightforward approach is to create the custom event log when the application is being installed. Generally I followed the steps described here</li></ul></p><p><a href=\"http://kanakaiah.wordpress.com/2007/10/11/create-a-new-event-source-in-the-event-log/\">Create a New Event Source in the Event Log</a></p><p><ul><li>Created a new project in my solution of the type Class Library</li><li>Added the code for the class that inherits from System.Configuration.Install.Installer and creates the event log</li><li>Built the project</li></ul></p><p>Next, however, the author suggested to directly use the resulting dll to create the event log by running the InstallUtil. That's not what I want my clients to go through, so I switched to another solution from here:</p><p><a href=\"http://msdn.microsoft.com/en-us/library/f5dcf6h3(vs.71).aspx\">Walkthrough: Installing an Event Log Component</a></p><p><ul><li>Added the output from my Class Library to the Setup and Deployment project</li><li>Created a custom action and assigned the output to the custom action</li><li>Built the Setup and Deployment project</li></ul></p><p>Early testing shows so far that now you have to be an Administrator to install the application. But the event log is created at the time of installation, while previously it was not created until the event had to be logged first time. And in the future any user can use that event log.</p><p>This page, by the way, suggested overriding the Install and Uninstall methods in the class that creates the event log, unlike the one I used, where the log is created right in the class constructor. But I didn't get deep enough to understand the benefit of this approach:</p><p><a href=\"http://www.devx.com/dotnet/Article/20849/1954\">Building Custom Installer Classes in .NET</a></p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_04102011_k = "create event source in event log software development";
        public const string content_04102011_d = "An Application Needs to Create An Event Source in the Event Log";

        //"Small things learned today: Raise Base Class Event in Derived Classes"
        public const string content_22082011_b = "<p>I have to admit that I did not know this before today.</p><p>If I have a base class that have controls, I can not directly subscribe to the events invoked by the controls of this class (or, more generally, I can not directly subscribe to any events declared by the base class, but in my case I was interested in button click events).</p>";
        public const string content_22082011_r = "<p>I have to use a simple technique to achieve my goal: In the base class, provide and EventHandler (my button is called \"Run Draw\", hence the names)</p><pre class=\"brush:csharp\">" +
            @"public event EventHandler<eventargs> RunDrawClicked;<br />protected virtual void OnRunDrawClicked(EventArgs e)<br />{<br />  EventHandler<eventargs> handler = RunDrawClicked;<br />  if (handler != null)<br />  {<br />    handler(this, e);<br />  }<br />}" + "</pre><p>base class can subscribe to its own button click, of course</p><pre class=\"brush:csharp\">" +
            @"protected void btnRunDraw_Click(object sender, System.EventArgs e)<br />{<br /> MessageBox.Show(""base"");<br /> OnRunDrawClicked(e);<br />}" + "</pre><p>and the derived class can subscribe to the event provided by the base class</p><pre class=\"brush:csharp\">" + @"protected override void OnRunDrawClicked(EventArgs e)<br />{<br /> MessageBox.Show(""derived"");<br /> base.OnRunDrawClicked(e);<br />}" + "</pre><p>Reference:</p><a href=\"http://msdn.microsoft.com/en-us/library/hy3sefw3(v=vs.80).aspx\">How to: Raise Base Class Events in Derived Classes</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_22082011_k = "C# software development Raise Base Class Event in Derived Classes";
        public const string content_22082011_d = "Small things learned today: Raise Base Class Event in Derived Classes";

        //"Third Party DLL random thoughts ..."
        public const string content_15082011_b = "<p>I'm always very happy then a third party dll or SDK comes with a code sample, in any language. I think this is the best what developers can do to ensure that users of their libraries will not have problems. It helped me a number of times (Oh, so I have to pass THIS to the function ... why didn't they mention it in the manual?).</p>";
        public const string content_15082011_r = "<p>But I like it a bit less when the code comes as a neat Visual Studio solution which no one obviously tested to make sure it compiles.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/15082011_Fatal_Error.png\" alt=\"Fatal Error\" /></div>" +
            "<p>So I made an empty header file bc_Content_Decoder_Constants.h and added it to the project and commented out the body of a function that used the constants from the header.</p><p>Now I'm up to the next challenge: I can run the application in release mode, but not in debug mode.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/15082011_Unable_To_Start_Program.png\" alt=\"Unable To Start Program\" /></div>" +
            "<p>Looks a bit vague.</p><p>I spent a while playing with linker settings and application manifest, but eventually ended up debugging the application in my head. In this particular case it was not as hard as it may sound - just looked at what functions were called in what sequence and duplicated that in my C# \"throwaway\" app and found a workaround to my problem. Sill, sample code from the third party was better than nothing.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_15082011_k = "software development third party dll";
        public const string content_15082011_d = "Random thoughts about third-party dlls";

        //"BindingList"
        public const string content_17072011_b = "A bug was reported in one of my applications. Well, the sequence of actions was not very likely to happen, but still it was a bug. In fact, when a certain action (printing a card) was performed, the program behaved as expected. When the same action was performed a second time, an exception was thrown. Here is what the stack trace said:<br />";
        public const string content_17072011_r = "System.IndexOutOfRangeException: Index -1 does not have a value.<br />atSystem.Windows.Forms.CurrencyManager.get_Item(Int32 index)<br />atSystem.Windows.Forms.CurrencyManager.get_Current()<br />at System.Windows.Forms.DataGridView.DataGridViewDataConnection.OnRowEnter(DataGridViewCellEventArgs e)<br />atSystem.Windows.Forms.DataGridView.OnRowEnter(DataGridViewCell&dataGridViewCell, Int32 columnIndex, Int32 rowIndex, Boolean canCreateNewRow, Boolean validationFailureOccurred)<br />at System.Windows.Forms.DataGridView.SetCurrentCellAddressCore(Int32 columnIndex, Int32 rowIndex, Boolean setAnchorCellAddress, Boolean validateCurrentCell, Boolean throughMouseClick)<br />at System.Windows.Forms.DataGridView.SetAndSelectCurrentCellAddress(Int32 columnIndex, Int32 rowIndex, Boolean setAnchorCellAddress, Boolean validateCurrentCell, Boolean throughMouseClick, Boolean clearSelection, Boolean forceCurrentCellSelection)<br />at System.Windows.Forms.DataGridView.MakeFirstDisplayedCellCurrentCell(Boolean includeNewRow)<br />atSystem.Windows.Forms.DataGridView.OnEnter(EventArgs e)<br />atSystem.Windows.Forms.Control.NotifyEnter()<br />atSystem.Windows.Forms.ContainerControl.UpdateFocusedControl()<br /><br />Well, one thing was obvious - I had nowhere to start from. The exception was not triggered by anything in my code. In fact, when I debugged the application, it happened after a GroupBox status was set to inactive.<br />There were people on the internet who had this problem before, and generally the suggestion was to look at the places where CurrentCell value of a DataGridView is accessed. Supposedly the problem was that CurrentCell value is null. However, I checked all places where CurrentCell was used and there were no indications of anything wrong happening.<br /><br />Eventually, I came across a simple and short comment which saved me.<br /><br /><a href=\"http://social.msdn.microsoft.com/Forums/en-US/winformsdatacontrols/thread/d13ab496-f1a3-47d8-9529-064eebc0a674/#aeaf492d-63f2-46f5-96be-3d4c2451000e\"> DataGridView - error driving me mad!</a><br /><br />\"I had the same problem. I changed the list bind to the datasource from a List to a BindingList.\"<br /><br />I still don't know what triggered the exception in the first place and don't have much time at the moment to spend on it - as it often happens, if it works, it's good enough already. And the application will be abandoned soon anyway and replaced by a completely new version. So it will remain a mystery for me. by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_17072011_k = "bindinglist software development";
        public const string content_17072011_d = "BindingList saves the day";

        //"Cryptic Error Message"
        public const string content_28062011_b = "<p>This is another cryptic error message the reason for which will go into the \"mystery\" basket for me.</p>";
        public const string content_28062011_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2011/28062011_Cryptic_Error_Message.png\" alt=\"Cryptic Error Message\" /></div>" +
            "<p>All I needed was to tweak a setup and deployment project a little, so I made a copy from an existing one, added and removed some files, compiled without errors and tried to install.</p><p>Well, I had to work around it for now since there is really not enough information to even start investigating.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_28062011_k = "random error";
        public const string content_28062011_d = "Cryptic Error Message";

        //"Where has my stored procedure parameter gone?"
        public const string content_10052010_b = "<p>I had to change the database and some code to allow for proper saving/reading of Unicode from the application. Changing the parameter type in the stored procedure from varchar to nvarchar and doing the same change to the appropriate database table columns was the easy part. Doing some adjustments to the code, well, was also the easy part but with a little trick hidden inside.</p><p>So, here's how the parameters are added to the collection:</p>";
        public const string content_10052010_r = "<pre class=\"brush:csharp\">" +
            @"_collParam.Add(DataManager.BuildSqlParameter(""@MessageText"", SqlDbType.VarChar,<BR> ParameterDirection.Input, Message.ToString()));" + "</pre><p>Just change VarChar to NVarChar, should be easy as pie.</p><p>And I end up with the SqlException \"Procedure or function 'udp_MyProcedure_ups' expects parameter '@MessageText', which was not supplied\". So, where has my parameter gone if I can clearly see that I'm adding it? The answer lies within the DataManager.BuildSqlParameter() function. Here's part of what it does:</p><pre class=\"brush:csharp\">" +
            @"if (value != "")<br /> {<br />  if (paramType == SqlDbType.Int)<br />   param.Value = Convert.ToInt32(value);<br />  else if (paramType == SqlDbType.Bit)<br />   param.Value = Convert.ToBoolean(value);<br />  else if (paramType == SqlDbType.DateTime)<br />  {<br />   param.Value = Convert.ToDateTime(value);<br />  }<br />  else if (paramType == SqlDbType.VarChar)<br />   param.Value = Convert.ToString(value);<br />  else if (paramType == SqlDbType.Float)<br />   param.Value = (float)Convert.ToDecimal(value);<br /> }" +
            "</pre><p>So what happens when the parameter does not belong to any of the types? It gets no value assigned, causing the exception. There is no \"default\" value. Easy to fix, but worth noting.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_10052010_k = "software development sql server stored procedure parameters";
        public const string content_10052010_d = "Where has my stored procedure parameter gone?";

        //"Unit Testing With Compact Framework and Visual Studio"
        public const string content_11122009_b = "<p>Following up my issue with running NUnit tests for the Windows Mobile application, I came across a couple of articles on using the unit testing framework integrated in Visual Studio 2008 which is now supposed to be user friendly.<br />The process starts with selecting the function name, right-clicking on it and selecting \"Create Unit Tests\"</p>";
        public const string content_11122009_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/11122009_Create_Unit_Tests_1.png\" alt=\"Create Unit Tests 1\" /></div>" +
            "<p>I can select the functions I want unit tests to be created for - I'll only choose one for now</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/11122009_Create_Unit_Tests_2.png\" alt=\"Create Unit Tests 2\" /></div>" +
            "<p>I am then prompted for the name of the project where my tests will be created. Visual Studio adds a new project to the solution and this is the code for the test method created for the function I chose.</p><pre class=\"brush:csharp\">" +
            @"<br />        /// <summary><br />        ///A test for CreateDatabase<br />        ///</summary><br />        [TestMethod()]<br />        public void CreateDatabaseTest()<br />        {<br />            DataBase target = new DataBase(); // TODO: Initialize to an appropriate value<br />            target.CreateDatabase();<br />            Assert.Inconclusive(""A method that does not return a value cannot be verified."");<br />        }" + "</pre><p>This is great, except that I want to test for the things I want to test. So, of course, I need to change that. That's probably closer to what I want to test in my method:</p><pre class=\"brush:csharp\">" +
            @"<br />        [TestMethod()]<br />        public void CheckDatabaseCreation()<br />        {<br />            DataBase target = new DataBase();<br />            target.SetFileName(@""\Program Files\TestDB\TTrack.sdf"");<br />            target.SetConnectionString(@""Data Source=\Program Files\TestDB\TTrack.sdf"");<br />            target.DeleteDatabase();<br />            target.CreateDatabase();<br />            target.RunNonQuery(target.qryInsertRecord);<br />            int count = target.RunScalar(target.qryCountUsers);<br />            Assert.AreEqual(count, 1);<br />        }" +
            "</pre><p>This is not so much different from the way tests are created in NUnit. In fact, so far there is no difference at all. Now, to run the test. There is a menu item \"Test\" in the top menu where I can select Test->Windows->Test View and the \"Test View\" becomes visible.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/11122009_Test_View.png\" alt=\"Test View\" /></div>" +
            "<p>There I can see my tests - the auto generated one and the one I added myself.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/11122009_Test_View_1.png\" alt=\"Test View 1\" /></div>" +
            "<br /><p>I can run all tests or select any combination of tests I want to run from the Test View and choose either \"Run Selection\" or \"Debug Selection\" (I did not find out yet what the difference is - if I place a breakpoint inside the test method and choose \"Debug Selection\", the execution does not break at the breakpoint). After the test(s) finished running, I can see the result in the Test Results window.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/11122009_Test_Run_Completed.png\" alt=\"Test Run Completed\" /></div>" +
            "<br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_11122009_k = "software development compact framework visual studio unit testing";
        public const string content_11122009_d = "Unit Testing With Compact Framework and Visual Studio";

        //"Compact Framework and NUnit"
        public const string content_24112009_b = "<p><p>I have the idea of a small application I could write for the Windows Mobile. The application will only use its local SQL Server database, at least initially, and it is really simple to create a local database on the device. The only things I need are the physical location of the sdf file on the device and the connection string.</p><p>In my \"DataBase\" class I generate them</p>";
        public const string content_24112009_r = "<pre class=\"brush:csharp\">" + @"private string GetLocalDatabasePath()<br />{<br /> string applicationPath = Path.GetDirectoryName(this.GetType().Assembly.GetName().CodeBase);<br /> string localDatabasePath = applicationPath + Path.DirectorySeparatorChar +<br />   ""TTrack.sdf"";<br /> return localDatabasePath;<br />}<br /><br />private string GetLocalConnectionString()<br />{<br /> string localConnectionString = ""Data Source="" +<br />   GetLocalDatabasePath();<br /><br /> return localConnectionString;<br />}" +
            "</pre><p>To create a database I just check if the database file already exists, and if not - I create it. Also, for testing purposes, the delete database function is used.</p><pre class=\"brush:csharp\">" +
            @"internal void CreateDatabase()<br />{<br /> if (!File.Exists(localDatabasePath))<br /> {<br />  using (SqlCeEngine engine = new SqlCeEngine(localConnectionString))<br />  {<br />   engine.CreateDatabase();<br />  }<br /><br />  RunNonQuery(qryCreateTables);<br /> }<br />}<br /><br />internal void DeleteDatabase()<br />{<br /> string dbPath = GetLocalDatabasePath();<br /> if (File.Exists(dbPath))<br /> {<br />  File.Delete(dbPath);<br /> }<br />}" +
            "</pre><p>The RunNonQuery bit is just the creation of tables in the database.</p><pre class=\"brush:csharp\">" +
            @"internal void RunNonQuery(string query)<br />{<br /> string connString = GetLocalConnectionString();<br /><br /> using (SqlCeConnection cn = new SqlCeConnection(connString))<br /> {<br />  cn.Open();<br />  SqlCeCommand cmd = cn.CreateCommand();<br />  cmd.CommandText = query;<br />  cmd.ExecuteNonQuery();<br /> }<br />}" +
            "</pre><p>The query for now just creates the simplest possible \"dummy\" table</p><pre class=\"brush:csharp\">" + @"internal string qryCreateTables = ""CREATE TABLE Users ("" +<br /> ""UserID uniqueidentifier PRIMARY KEY DEFAULT NEWID() NOT NULL, "" +<br /> ""Name NVARCHAR(50) NOT NULL )"";" +
            "</pre><p>The RunScalar, obviously, is used to run ExecuteScalar(). Some refactoring still required to improve the code.</p><pre class=\"brush:csharp\">" +
            @"internal int RunScalar(string query)<br />{<br /> string connString = GetLocalConnectionString();<br /><br /> using (SqlCeConnection cn = new SqlCeConnection(connString))<br /> {<br />  cn.Open();<br />  SqlCeCommand cmd = cn.CreateCommand();<br />  cmd.CommandText = query;<br />  return int.Parse(cmd.ExecuteScalar().ToString());<br /> }<br />}" +
            "</pre><p>Now that I can create a database, I can run this simple bit of code to see if it is working.</p><pre class=\"brush:csharp\">" +
            @"DataBase db = new DataBase();<br /> db.CreateDatabase();<br /> db.RunNonQuery(db.qryInsertRecord);<br /> MessageBox.Show(db.RunScalar(db.qryCountUsers).ToString());" + "</pre><p>Database gets created, a record gets inserted, a messagebox with \"1\" is shown. All is well.</p><p>Next, I decide to quickly create and run a simple test for database creation: If the database is present, I delete it, then create a new one, insert one record and test for the count of records indeed being one.</p><p>Here is the test I write in NUnit.</p><pre class=\"brush:csharp\">" +
            @"[Test]<br />public void CheckDatabaseCreation()<br />{<br /> DataBase db = new DataBase();<br /> db.SetFileName(@""\Program Files\TestDB\TTrack.sdf"");<br /> db.SetConnectionString(@""Data Source=\Program Files\TestDB\TTrack.sdf"");<br /> db.DeleteDatabase();<br /> db.CreateDatabase();<br /> db.RunNonQuery(db.qryInsertRecord);<br /> int count = db.RunScalar(db.qryCountUsers);<br /> Assert.AreEqual(count, 1);<br />}" + "</pre><p>This does not go as well, however:</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/24112009_Failed_Test.png\" alt=\"Failed Test\" /></div>" +
            "<p>What happened there? Oh, of course - the NUnit test runs on the desktop, but the code is supposed to run on the emulator (I don't use the actual device yet). So, it looks like I will have to work on the approach to testing ...</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_24112009_k = "Software development Compact Framework NUnit";
        public const string content_24112009_d = "Compact Framework and NUnit";

        //"Compact Framework and forms management"
        public const string content_19112009_b = "<p>This was my first experience with the Microsoft Compact Framework. The application is developed in Visual Studio 2005 and is targeted to run on Pocket PC 2003 devices. Basically, I had to extend a simple application that had only one form to add some functionality and a few more forms. I understand that the forms in Compact Framework are treated a bit differently compared to a desktop application. What to use for navigation between forms, Show() or ShowDialog()? I decided to use Show() because I have only about 5 forms, most of those are very simple and also, my application will be the only one running on the device. So I thought, if I create each form once and keep them all in memory, just showing and hiding them, it may use more memory, which I do not care that much about, but be easier on the device battery. Okay, I may be saying total nonsense here - I have about 7 days Compact Framework development experience at this very moment.</p>";
        public const string content_19112009_r = "<p>So I have a dictionary where all existing forms are kept.</p><pre class=\"brush:csharp\">" +
            @"private static Dictionary<string, Form> _applicationForms = new Dictionary<string,Form>();</pre>" + "<p>And the function that gets the form from the dictionary by name.</p><pre class=\"brush:csharp\">" +
            @"internal static Form GetFormByName(string formName)<br />{<br /> if (_applicationForms.ContainsKey(formName))<br /> {<br />  return _applicationForms[formName];<br /> }<br /> else<br /> {<br />  Form newForm = CreateFormByName(formName);<br />  AddFormIfNotExists(newForm);<br />  return newForm;<br /> }<br />}" +
            "</pre><p>And the function to create a form if it has not been yet created.</p><pre class=\"brush:csharp\">" +
            @"private static Form CreateFormByName(string name)<br />{<br /> Form form = new Form();<br /><br /> switch (name)<br /> { <br />  case Constants.frmFirst:<br />   form = new frmFirst();<br />   break;<br />   <br />   ...<br />   <br />  case Constants.frmLast:<br />   form = new frmLast();<br />   break;<br />  default:<br />   form = new frmLast();<br />   break;<br /> }<br /> return form;<br />}" + "</pre><p>And the function to add the form to the dictionary if it is not there.</p><pre class=\"brush:csharp\">" +
            @"internal static void AddFormIfNotExists(Form frm)<br />{<br /> if (!_applicationForms.ContainsKey(frm.Name))<br /> {<br />  _applicationForms.Add(frm.Name, frm);<br /> }<br />}" + "</pre><p>And when I need to show another form, I get it from the dictionary and show, and hide the current form.</p><pre class=\"brush:csharp\">" +
            @"internal static void ShowFromForm(Form source, string targetName)<br />{<br /> Form frm = GetFormByName(targetName);<br /> frm.Show();<br /> source.Hide();<br />}" + "</pre><p>There's a bit more to it, sometimes I need to find which form is currently visible etc, but these are the core things. Stupid? Good enough? I don't know ...</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_19112009_k = "C# Software development Compact Framework forms management";
        public const string content_19112009_d = "Compact Framework and forms management";

        //"Doing Some Stuff on Another Computer"
        public const string content_03102009_b = "<p>It is fairly easy to restart a service running on a remote computer. You only need to know two things - the name of a remote computer and the name of the service itself. No surprises.</p>";
        public const string content_03102009_r = "<pre class=\"brush:csharp\">" + @"private void RestartService(string MachineName, string ServiceName)<br />{<br /> using (ServiceController controller = new ServiceController())<br /> {<br />  controller.MachineName = MachineName;<br />  controller.ServiceName = ServiceName;<br />  controller.Stop();<br />  controller.Start();<br /> }<br />}" +
            "</pre><p>Almost as easy is to monitor the printers on the remote computer using WMI. This time, only the remote computer name is required. Here's a small function that returns the list of CustomPrinterObjects. CustomPrinterObject can be defined like this, for example:</p><pre class=\"brush:csharp\">" +
            @"public class CustomPrinterObject<br />{<br /> private string _name;<br /><br /> public string Name<br /> {<br />  get { return _name; }<br />  set { _name = value; }<br /> }<br /><br /> //many other properties<br /> ....<br /> <br />    private string _status;<br /><br /> public string Status<br /> {<br />  get { return _status; }<br />  set { _status = value; }<br /> }<br />}" + "</pre><p>Here's how I get the information about the printers on the remote computer:</p><pre class=\"brush:csharp\">" +
            @"public List<CustomPrinterObject> GetLocalPrinters(string serverName)<br />{<br /> string[] pStatus = {""Other"",""Unknown"",""Idle"",""Printing"",""WarmUp"",""Stopped Printing"",<br />        ""Offline""};<br /><br /> string[] pState = {""Paused"",""Error"",""Pending Deletion"",""Paper Jam"",""Paper Out"",<br />           ""Manual Feed"",""Paper Problem"", ""Offline"",""IO Active"",""Busy"",<br />           ""Printing"",""Output Bin Full"",""Not Available"",""Waiting"", <br />           ""Processing"",""Initialization"",""Warming Up"",""Toner Low"",<br />           ""No Toner"",""Page Punt"", ""User Intervention Required"",<br />           ""Out of Memory"",""Door Open"",""Server_Unknown"",""Power Save""};<br /><br /> List<CustomPrinterObject> printers = new List<CustomPrinterObject>();<br /><br /> string query = string.Format(""SELECT * from Win32_Printer"");<br /> ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);<br /> searcher.Scope = new ManagementScope(""\\\\"" + serverName + ""\\root\\CIMV2"");<br /> ManagementObjectCollection coll = searcher.Get();<br /> <br /> System.Windows.Forms.MessageBox.Show(coll.Count.ToString());<br /><br /> foreach (ManagementObject printer in coll)<br /> {<br />  CustomPrinterObject prn = new CustomPrinterObject();<br /><br />  foreach (PropertyData property in printer.Properties)<br />  {<br />   if (property.Value != null)<br />   {<br />    switch (property.Name)<br />    {<br />     case ""Name"":<br />      prn.Name = property.Value.ToString();<br />      break;<br />     case ""Comment"":<br />      prn.Comment = property.Value.ToString();<br />      break;<br />     case ""PrinterState"":<br />      prn.PrinterState = pState[Convert.ToInt32(property.Value)];<br />      break;<br />     case ""PrinterStatus"":<br />      prn.PrinterStatus = pStatus[Convert.ToInt32(property.Value)];<br />      break;<br />     case ""Location"":<br />      prn.Location = property.Value.ToString();<br />      break;<br />     case ""Type"":<br />      prn.Type = property.Value.ToString();<br />      break;<br />     case ""DriverName"":<br />      prn.Model = property.Value.ToString();<br />      break;<br />     case ""WorkOffline"":<br />      prn.Status = property.Value.ToString().Equals(""True"") ? ""Offline"" : ""Online"";<br />      break;<br />     default:<br />      break;<br />    }<br />   }<br />  }<br />  printers.Add(prn);<br /> }<br /> return printers;<br />}" +
            "</pre><p>Reading the registry contents on the remote machine is very easy again.</p><p>On the local computer I would open the key like this</p><pre class=\"brush:csharp\">" +
            @"RegistryKey rk = Registry.LocalMachine.OpenSubKey(subKey);" + "</pre><p>And on the remote I would do it like this</p><pre class=\"brush:csharp\">" +
            @"RegistryKey hklm = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, ""MyRemoteServer"");<br />                RegistryKey rk = hklm.OpenSubKey(subKey);" +
            "</pre><p>Obviously, all of the samples will work subject to permissions of the account that runs them. Errors will happen if the account does not have enough privileges to access the printers or services on the remote computer.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_03102009_k = "c# software development remote connection";
        public const string content_03102009_d = "How to connect to another computer and do Some Stuff on it programmatically";

        //"Simple WCF client/server"
        public const string content_01102009_b = "<p>So communicating between two windows services on the same computer is easy. But then I was asked, what if we decide in the future that we want these services to run on the separate machines? Well, I guess we'll need to make changes to both of them ... and that's exactly what we want to avoid. Okay, the WCF offers a few ways to host a service - in a managed application, in a managed windows service, in IIS, in WAS ... (<a href=\"http://msdn.microsoft.com/en-us/library/ms730158.aspx\">Hosting Options</a>) Since I already have windows services implemented, the choice is obvious. I looked up a couple of tutorials on the subject fairly quickly:<br /><a href=\"http://msdn.microsoft.com/en-us/library/ms733069.aspx\">How to: Host a WCF Service in a Managed Windows Service</a>, <a href=\"http://www.switchonthecode.com/tutorials/wcf-tutorial-basic-interprocess-communication\">WCF Tutorial - Basic Interprocess Communication</a></p>";
        public const string content_01102009_r = "<p>However, that was not quite enough for me because the first tutorial's problem was that it explained the configuration file a bit, but did not implement the \"client\", and the second tutorial implemented both server and client, but had no info on configuration at all. So I quickly got to the point where I could have a server and client running inside separate windows services on the same machine, but as soon as I tried taking one of the services away - to another computer on the network - different errors started to happen. Not enough time and space on explaining each error and what was the reason for it, just a few words on what I ended up with (which eventually worked).</p><p>Service implementation</p><p>To define and implement the function on the server:</p><pre class=\"brush:csharp\">" +
            @"[ServiceContract(Namespace=""MyNamespace.IMyInterface"")]<br />public interface IMyInterface<br />{<br /> [OperationContract]<br /> string ReturnMyString();<br />}<br /><br />public class MyInterfaceImplementation : IMyInterface<br />{<br /> public string ReturnMyString()<br /> {<br />  return ""My String"";<br /> }<br />}" +
            "</pre><p>To create the instance of the host, first define the host</p><pre class=\"brush:csharp\">" +
            @"private ServiceHost host;" + "</pre><p>In the service OnStart method</p><pre class=\"brush:csharp\">" +
            @"if (host != null)<br />{<br /> host.Close();<br />}<br /><br />host = new ServiceHost(typeof(MyInterfaceImplementation), new Uri[] { new Uri(http://MyServer:8080) });<br />host.AddServiceEndpoint(typeof(IMyInterface), new BasicHttpBinding(), ""MyMethod"");" +
            "</pre><p>In the service OnStop method</p><pre class=\"brush:csharp\">" +
            @"if (host != null)<br />{<br /> host.Close();<br /> host = null;<br />}<br />" +
            "</pre><p>This part was fairly easy.</p><p>Service configuration</p><p>This bit went into the app.config file inside the \"configuration\".</p><pre class=\"brush:xml\">" +
            @"&lt;system.serviceModel&gt;<br />    &lt;services&gt;<br />      &lt;service name=""MyNamespace.MyService"" behaviorConfiguration=""MyServiceBehavior""&gt;<br />        &lt;host&gt;<br />          &lt;baseAddresses&gt;<br />            &lt;add baseAddress=""http://MyServer:8000/MyMethod""/&gt;<br />          &lt;/baseAddresses&gt;<br />        &lt;/host&gt;<br />        &lt;!-- this endpoint is exposed at the base address provided by host--&gt;<br />        &lt;endpoint address="""" binding=""basicHttpBinding"" contract=""MyNamespace.IMyInterface"" /&gt;<br />      &lt;/service&gt;<br />    &lt;/services&gt;<br />    &lt;behaviors&gt;<br />      &lt;serviceBehaviors&gt;<br />        &lt;behavior name=""MyServiceBehavior""&gt;<br />          &lt;serviceMetadata httpGetEnabled=""true""/&gt;<br />          &lt;serviceDebug includeExceptionDetailInFaults=""False""/&gt;<br />        &lt;/behavior&gt;<br />      &lt;/serviceBehaviors&gt;<br />    &lt;/behaviors&gt;<br />&lt;/system.serviceModel&gt;" + "</pre><p>Note the service name attribute \"MyNamespace.MyService\" which is the windows service names, and the endpoint contract attribute, which is the ServiceContract Namespace attribute. Some small things are easy to get wrong, and the error messages will not be very informative.</p><p>Client implementation</p><pre class=\"brush:csharp\">" +
            @"[ServiceContract(Namespace=""MyNamespace.IMyInterface"")]<br />public interface IMyInterface<br />{<br /> [OperationContract]<br /> string ReturnMyString();<br />}<br /><br />public string MyClientString()<br />{<br /> string result = string.Empty;<br /><br /> string endpoint = ""http://MyServer:8000/MyMethod"";<br /><br /> ChannelFactory<IMyInterface> httpFactory = new ChannelFactory<IMyInterface>(<br />  new BasicHttpBinding(), new EndpointAddress(endpoint));<br /><br /> IMyInterface httpProxy = httpFactory.CreateChannel();<br /><br /> while (true)<br /> {<br />  result = httpProxy.ReturnMyString();<br />  if (result != string.Empty)<br />  {<br />   return result;<br />  }<br /> }<br />}" + "</pre><p>I missed the [ServiceContract(Namespace=\"MyNamespace.IMyInterface\")] bit initially on the interface definition and the error message was really not helping. It went like that: \"Exception: The message with Action 'http://tempuri.org/IMyInterface/ReturnMyString' cannot be processed at the receiver\" and so on. What tempuri.org? I never pun any tempuri.org in my project! OK, turns out it is some default name that was used because I have not provided my own.</p><p>Client configuration</p><p>Just a small bit of configuration was required here (and I'm not even 100% sure that all of it is required)</p><pre class=\"brush:xml\">" +
            @"&lt;system.serviceModel&gt;<br />    &lt;bindings&gt;<br />      &lt;basicHttpBinding&gt;<br />        &lt;binding name=""basicHttp""/&gt;<br />      &lt;/basicHttpBinding&gt;<br />    &lt;/bindings&gt;<br />    &lt;client&gt;<br />      &lt;!-- this endpoint is exposed at the base address provided by host--&gt;<br />      &lt;endpoint address="" binding=""basicHttpBinding"" contract=""MyNamespace.IMyInterface"" /&gt;<br />    &lt;/client&gt;<br />&lt;/system.serviceModel&gt;" +
            "</pre><p>Overall, it's just a few dozen lines of code, but it took me almost a whole day to get it working properly through the network.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_01102009_k = "WCF client server .net C# software development";
        public const string content_01102009_d = "Developing a Simple WCF client/server";

        //"A Small Unit Testing Gem"
        public const string content_16092009_b = "<p>Since I started writing unit tests for my code, I had this question in mind. Let's say I have a project that is a class library. I have a class in that library and this class has some internal methods. Like this:</p>";
        public const string content_16092009_r = "<pre class=\"brush:csharp\">" +
            @"public class MyClass<br />{<br /> public void MyPublicMethod<br /> {<br />  int k    <br />  // do something ...    <br />  int z = MyInternalMethod(k);<br />  // do something else ...  <br /> }  <br /> <br /> internal int MyInternalMethod(int i)  <br /> {<br />  // do something ...  <br /> }<br />}" + "</pre><p>Now I want to write unit tests for these methods. I would create a \"UnitTests\" project, reference the nunit.framework from it and write something like this:</p><pre class=\"brush:csharp\">" +
            @"[TestFixture]<br />public class UnitTests<br />{  <br /> private MyClass myClass;<br /> <br /> [SetUp]  <br /> public void SetupTest  <br /> {<br />  myClass = new MyClass();  <br /> }<br /><br /> [Test]<br /> public void TestMyInternalMethod  <br /> {<br />  int z = 100;<br />  int k = myClass.MyInternalMethod(z); //CAN NOT DO THIS!    <br />  Assert.AreEqual(k, 100000);  <br /> }<br /><br /> [TearDown]<br /> public void TearDown  <br /> {<br />  myClass = null;<br /> }<br />}" +
            "</pre><p>Of course, I can not do this, because of the MyInternalMethod scope. Today the <a href=\"http://stackoverflow.com/questions/1365309/unit-testing-and-the-scope-of-objects-how-to-test-private-internal-methods-etc\">StackOverflow guys</a> pointed me to this little gem which is very helpful.</p><p><a href=\"http://blog.theautomatedtester.co.uk/2008/10/net-gem-how-to-unit-test-internal.html\">.Net Gem - How to Unit Test Internal Methods </a></p><p>Here's the short summary:<br /><br />Go to the project that contains MyClass. Locate the AssemblyInfo.cs file. Add the following line to it:</p><pre class=\"brush:csharp\">" + @"[assembly: InternalsVisibleTo(""UnitTests"")]" + "</pre><p>Done!</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_16092009_k = "C# software development unit testing";
        public const string content_16092009_d = "A Small Unit Testing Gem";

        //"Thread Pooling"
        public const string content_10092009_b = "<p>I have to take care of multiple printers in my application. The \"Print Manager\" receives a list of jobs which is basically an XML file of a simple structure - a number of PrintJob nodes. Each print job has a printer assigned to it.</p>";
        public const string content_10092009_r = "<p>The Print Manager has to send each job to the appropriate printer, and also notify the sender of the XML of the completion or failure of each job. I'm sure tasks like these are common but somehow could not find good suggestions on implementing this one. I found a <a href=\"http://www.yoda.arachsys.com/csharp/miscutil/\">Miscellaneous Utility Library</a> though (written by <a href=\"http://meta.stackoverflow.com/questions/9134/jon-skeet-facts/9212\">Jon Skeet</a> himself by the way) which implemented a class called \"CustomThreadPool\", which allows creating multiple thread pools in a .NET application.</p><p>So, my approach so far is as follows: Get a print job. If a pool exists for this printer, place the job in a thread in the pool. Otherwise, create a pool and place the job in a thread in this pool. Get next job.</p><p>Here is how it looks like so far:</p><pre class=\"brush:csharp\">" + @"private List<CustomThreadPool> _printerThreads = new List<CustomThreadPool>();<br /><br />delegate Errors ThreadMethod(PrintJob job);<br /><br />private Errors InsertThread(PrintJob job)<br />{<br /> ProcessSinglePrintJob(job);<br />}<br /><br />// stuff ...<br /><br />public void ProcessPrintJobs()<br />{<br /> if (_printJobs != null)<br /> {<br />  foreach (PrintJob printJob in _printJobs)<br />  {<br />   if(String.IsNullOrEmpty(printJob.PrinterName))<br />   {<br />    printJob.JobResult = Errors.PrinterNameNotSpecified;<br />   }<br />   else if (String.IsNullOrEmpty(printJob.ReaderName) && printJob.IsEncodeSmartCard)<br />   {<br />    printJob.JobResult = Errors.SmartCardReaderNameNotSpecified;<br />   }<br />   else<br />   {<br />    CustomThreadPool pool = _printerThreads.Find(delegate(CustomThreadPool test)<br />    {<br />     return test.Name == printJob.PrinterName;<br />    });<br /><br />    if (pool == null)<br />    {<br />     pool = new CustomThreadPool(printJob.PrinterName);<br />    }<br /><br />    ThreadMethod method = new ThreadMethod(InsertThread);<br /><br />    pool.AddWorkItem(method, printJob);<br />   }<br />  }<br /> }<br />}" +
            "</pre><p>I don't have extensive experience with multithreading so this solution might not even work or it may be too complex for the task. I'll run some tests soon anyway with the actual printers.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_10092009_k = "Thread Pooling";
        public const string content_10092009_d = "Thread Pooling";

        //"Studying Interprocess Communication"
        public const string content_01092009_b = "<p>Today I had to solve a simple problem. Let's say there are two processes running on one computer. The first service polls a database for print jobs. As soon as a job is found, a second service has to send the job to the printer. So, effectively, I have to pass some data from one local service to another.</p>";
        public const string content_01092009_r = "<p>The first, \"amateurish\" solution that came to my mind was to write data to a text file by the \"polling\" service and read from that file by \"printing\" service. But I thought that the task like this should be a standard one and looked around. Here's one of the examples I found:</p><p><a href=\"http://www.switchonthecode.com/tutorials/dotnet-35-adds-named-pipes-support\">.NET 3.5 Adds Named Pipes Support</a></p><p>Here's the probably the simplest working example: First, I need to create two windows services. I add a timer to each service. I also add an event log to each of the services to be able to check if they work. One of the services will be a \"server\". Here's what goes into it's timer_Elapsed:</p><pre class=\"brush:csharp\">" +
            @"using (NamedPipeServerStream pipeServer = new NamedPipeServerStream(""testPipe"", PipeDirection.Out))<br />{<br /> pipeServer.WaitForConnection();<br /><br /> try<br /> {<br />  using (StreamWriter sw = new StreamWriter(pipeServer))<br />  {<br />   sw.AutoFlush = true;<br />   string dt = DateTime.Now.ToString();<br />   sw.WriteLine(dt);<br />   pollingEventLog.WriteEntry(dt + "" written by the server"");<br />  }<br /> }<br /> catch (IOException ex)<br /> {<br />  pollingEventLog.WriteEntry(ex.Message);<br /> }<br />}" + "</pre><p>The other service will be a \"client\". Here's what goes into it's timer_Elapsed:</p><pre class=\"brush:csharp\">" +
            @"using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(""."", ""testPipe"", PipeDirection.In))<br />{<br /> pipeClient.Connect();<br /> using (StreamReader sr = new StreamReader(pipeClient))<br /> {<br />  string temp;<br />  while ((temp = sr.ReadLine()) != null)<br />  {<br />   printManagerEventLog.WriteEntry(temp + "" read by the client"");<br />  }<br /> }<br />}" + "</pre><p>This is it - after both services are compiled, installed and started, their cooperation can be observed through the Event Log. Total time including googling, understanding the concept and implementing the working example - under 30 minutes.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_01092009_k = "software development Studying Interprocess Communication";
        public const string content_01092009_d = "Studying Interprocess Communication";

        //"Human Readable Entries For The Event Log"
        public const string content_24082009_b = "<p>Looks like I've been a bit busy recently!<br />Anyway, just a little trick I used today to produce human readable messages for the event log, avoiding complex switches or if/else blocks.<br />First, I put all possible errors in the enum, including the \"no error\", like this:</p>";
        public const string content_24082009_r = "<pre class=\"brush:csharp\">" +
            @"public enum Errors<br />{<br /> ProcessingCompletedSuccessfully = 0,<br /> CouldNotEstablishConnectionToPrinter = 1,<br /> ...<br /> GlobalSystemShutdownPreventedCompletingTheTaskInATimelyFashion = 999<br />}</pre>" + "<p>The event log is created as usual</p><pre class=\"brush:csharp\">" +
            @"private System.Diagnostics.EventLog pollingEventLog;<br /><br />if (!EventLog.SourceExists(""MyHumbleSource""))<br />{<br /> EventLog.CreateEventSource(""MyHumbleSource"", ""MyHumbleService"");<br />}<br />pollingEventLog.Source = ""MyHumbleSource"";<br />pollingEventLog.Log = ""MyHumbleService"";" +
            "</pre><p>The function should return the error code and the error code should be written to the event log</p><pre class=\"brush:csharp\">" +
            @"Errors error = PerformMyVeryComplexProcessing(XmlDocument data);<br />WriteErrorToLogFile(error);</pre>" +
            "<p>Finally, a small function that does the important stuff:</p><pre class=\"brush:csharp\">" +
            @"private void WriteErrorToLogFile(Errors error)<br />{<br /> string inputstr = error.ToString();<br /> Regex reg = new Regex(@""([a-z])[A-Z]"");<br /> MatchCollection col = reg.Matches(inputstr);<br /> int iStart = 0;<br /> string Final = string.Empty;<br /> foreach (Match m in col)<br /> {<br />  int i = m.Index;<br />  Final += inputstr.Substring(iStart, i - iStart + 1) + "" "";<br />  iStart = i + 1;<br /> }<br /> string Last = inputstr.Substring(iStart, inputstr.Length - iStart);<br /> Final += Last;<br /><br /> pollingEventLog.WriteEntry(Final);<br />}" +
            "</pre><p>I did not write the function myself - I got the solution from<br /><a href=\"http://xinyustudio.wordpress.com/2009/01/20/split-words-by-capital-letter-using-regex/\">Split words by capital letter using Regex</a></p><p>It takes the error, converts its name to string and splits the string by capital letters.<br />If the error returned was CouldNotEstablishConnectionToPrinter, then \"Could Not Establish Connection To Printer\" will be written to the event log.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_24082009_k = "Software development generating Human Readable Entries Event Log";
        public const string content_24082009_d = "Generating Human Readable Entries For The Event Log";

        //"WebOS Development - First Steps"
        public const string content_30072009_b = "<p>So, what is WebOS development about? First thing is to learn some 'Web OS speak'. The application begins with creating a 'stage' which is more or less like a main page for a website. The webpages, or what we call 'Forms' in Windows Forms, are called 'scenes'. These are html files. There is also code-behing for the scenes, it is javascript and called 'assistants' in Web OS speak. <p>";
        public const string content_30072009_r = "</p>The command to generate the application template is</p><p><strong>palm-generate -p \"{title:'My Application Title', id:com.mystuff.myapp, version:'1.0.0'}\" MyAppVersionOne</strong></p><p>More on application structure here</p><p><a href=\"http://developer.palm.com/index.php?option=com_content&view=article&id=1626\">Application Structure</a></p><p>So, my first app has two <s>pages</s> scenes. On the first one I can press a button and this will insert a record in a database table. Another button will take me to the second scene. On the second scene the database table records are displayed and the button that takes me back to the first scene. Pretty basic stuff. The scene markup is as simple as that</p><p><a href=\"http://4.bp.blogspot.com/_Pk-5kPvf7Gs/SnGaqDeg6HI/AAAAAAAAAG4/BmpO50dDGq8/s1600-h/scene.PNG\"><img style=\"display:block; margin:0px auto 10px; text-align:center;cursor:pointer; cursor:hand;width: 320px; height: 82px;\" src=\"http://4.bp.blogspot.com/_Pk-5kPvf7Gs/SnGaqDeg6HI/AAAAAAAAAG4/BmpO50dDGq8/s320/scene.PNG\" border=\"0\" alt=\"\"id=\"BLOGGER_PHOTO_ID_5364238678453446770\" /></a></p><p>The add_button adds the record, the display_button takes me to the next scene. Other divs I use for debugging - they just display text. Now to the <s>code-behind</s> assistant.</p><pre class=\"brush:js\">" +
            @"function FirstAssistant() {<br /> /* this is the creator function for your scene assistant object. It will be passed all the <br />    additional parameters (after the scene name) that were passed to pushScene. The reference<br />    to the scene controller (this.controller) has not be established yet, so any initialization <br />    that needs the scene controller hould be done in the setup function below. */<br />    this.db = null;<br />}<br /><br />FirstAssistant.prototype.setup = function() {<br /> this.CreateDB();<br /> Mojo.Event.listen($('add_button'), Mojo.Event.tap, this.AddEntry.bind(this));<br /> Mojo.Event.listen($('display_button'), Mojo.Event.tap, this.DisplayEntries.bind(this));<br /> $('result2').update('debug comment');<br />}" + "</pre><p>Okay, you can read the comments. Define variables, subscribe to events, open or create the database - set to go! This is the CreateDB function by the way</p><pre class=\"brush:js\">" +
            @"FirstAssistant.prototype.CreateDB = function(){<br /> try <br /> {<br />  this.db = openDatabase('SampleDB', '', 'Sample Data Store', 65536);<br />  if(!this.db)<br />  {<br />   $(result).update('error opening db');<br />  }<br />  else<br />  {<br />   $(result).update('opened db');<br />   <br />   var string = 'CREATE TABLE IF NOT EXISTS table1 (col1 TEXT NOT NULL DEFAULT ""nothing"", col2 TEXT NOT NULL DEFAULT ""nothing""); GO;';<br />    <br />   this.db.transaction( <br />    (function (transaction) { <br />    transaction.executeSql(string, [], this.createTableDataHandler.bind(this), this.errorHandler.bind(this)); <br />    }).bind(this) );<br />  }<br /> }<br /> catch (e)<br /> {<br />  $(result).update(e);<br /> }<br />}" +
            "</pre><p>WebOS uses Sqlite as a database engine. This is how a record is inserted:</p><pre class=\"brush:js\">" +
            @"FirstAssistant.prototype.AddEntry = function() {<br /> var string = 'INSERT INTO table1 (col1, col2) VALUES (""test"",""test""); GO;';<br /> <br /> this.db.transaction( <br />        (function (transaction) { <br />            transaction.executeSql(string, [], this.createRecordDataHandler.bind(this), this.errorHandler.bind(this)); <br />        }).bind(this) <br />    );  <br />}" + "</pre><p>This is how I move to the next scene:</p><pre class=\"brush:js\">" +
            @"FirstAssistant.prototype.DisplayEntries = function() {<br /> this.controller.stageController.pushScene('second', this.db);<br />}" +
            "</pre><p>I pass the name of the scene I want to 'push' and then the parameters. On the second scene I will grab these parameters and use them. Almost as the first scene, but now I have the database open already so I pass it to the second scene.</p><pre class=\"brush:js\">" +
            @"function SecondAssistant(origdb) {<br />    this.db = origdb;<br />}</pre>" +
            "<p>Okay, displaying the results and formatting them are outside of the scope of this brief post. Also, I copied the code from one of the samples on developer.palm.com, the application is called Data and the code can be found under Data\\app\\assistants\\storage\\sqlite and Data\\app\\views\\storage\\sqlite. Here is how the scenes look on the emulator</p><p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/30072009_WebOs.png\" alt=\"WebOs\" /></div>" +
            "</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_30072009_k = "WebOS Development";
        public const string content_30072009_d = "About First steps in WebOS Development";

        //"WebOS Development"
        public const string content_29072009_b = "<p>Quite a while ago (around 8 years) I was doing some development for PalmOS devices. With the new Palm Pre soon to be released in Australia and the Mojo SDK now freely available to everyone I decided to have a look at WebOS development. There is no need to have a Palm Pre device to begin development, I only had to download and install the SDK, Java and the VirtualBox to run the emulator. There is also a possibility of using Eclipse with the WebOS plugin but since I'm just starting and not doing anything complex, I'm happy to use Notepad++ as the IDE.</p>";
        public const string content_29072009_r = "<p><a href=\"http://developer.palm.com/index.php?option=com_content&view=article&id=1661\">Installing the Palm® Mojo™ SDK on Windows</a></p><p>The Hello World page gives an understanding of the steps required to create, compile and install a WebOS application. The \"SDLC\" is simple and takes a few seconds - generate the application, add some functionality, package, install on the emulator, have a look, improve/fix functionality, package, install on the emulator, have a look ...</p><p><a href=\"http://developer.palm.com/index.php?option=com_content&view=article&id=1758\">Hello, World!</a></p><p>There are also sample applications available and in the first few hours that I spent I learned a few things about how the applications function - scenes, assistants etc. - and also a few basic things about how the databases are used.</p><p><a href=\"http://developer.palm.com/index.php?option=com_content&view=article&id=1688\">Samples</a></p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_29072009_k = "WebOS Development";
        public const string content_29072009_d = "About WebOS Development";

        //"Mifare 1K Memory Structure"
        public const string content_22072009_b = "<p>To complete the task I'm working on and read/write to smart cards, I had to understand the memory structure of the Mifare Standard 1K card. This was no easy task for a weak and small brain of mine! I figured it out finally and here is a very short summary of my understanding:</p>";
        public const string content_22072009_r = "<p>The total memory of 1024 bytes is divided into 16 sectors of 64 bytes, each of the sectors is divided into 4 blocks of 16 bytes. Blocks 0, 1 and 2 of each sector can store data and block 3 is used to store keys and access bits (the exception is the ‘Manufacturer Block’ which can not store data).</p><p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/22072009_MiFare_Memory_Structure.png\" alt=\"MiFare Memory Structure\" /></div>" +
            "</p><p>The data in any sector can be protected by either Key A or both Key A and Key B security keys. I do not need to use Key B, and in this case the bytes in the trailer can be used for data. If the sector is protected by the security key, this key has to be loaded before data can be accessed by using a special command.</p><p>Access bits define the way the data in the sector trailer and the data blocks can be accessed. Access bits are stored twice – inverted and non-inverted in the sector trailer as shown in the images.</p><p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/22072009_MiFare_Block_Structure.png\" alt=\"MiFare Block Structure\" /></div>" +
            "</p><p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/22072009_MiFare_Access_Bits.png\" alt=\"MiFare Access Bits\" /></div>" +
            "</p><p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/22072009_MiFare_Access_Bits_Access_Condition.png\" alt=\"MiFare Access Bits Condition\" /></div>" +
            "</p><p>Some examples:<br /><br />Data stored in the sector trailer:<br />01 02 03 04 05 06 FF 07 80 69 11 12 13 14 15 16<br />01 02 03 04 05 06 – Key A<br />FF 07 80 69 – Access bits<br />11 12 13 14 15 16 – Key B (or data if Key B is not used)<br /><br />Bytes 6, 7, 8 are access data<br />FF 07 80<br /><br />Binary representation:<br /><strong>1</strong>111<strong>1</strong>111 = FF<br /><strong><u>0</u></strong>000<strong>0</strong>111 = 07<br /><strong><u>1</u></strong>000<strong><u>0</u></strong>000 = 80</p><p>The bits that are bolded and underscored are the ones that define access to keys (C13, C23, C33 in the image above) and they form the 001 sequence. The bits that are bolded and not underscored are the same bits inverted. They form, as expected, the sequence 110.</p><p>From the table above I can see that 001 means that Key A can not be read, but can be written and Key B may be read. This is the \"transport configuration\" and was read from the card that was never used.</p><p>A bit more on Mifare 1K commands next time.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_22072009_k = "Mifare 1K Memory Structure C# SmartCard software development";
        public const string content_22072009_d = "Mifare 1K Memory Structure";

        //"Smart Cards Do Not Hurt Anymore"
        public const string content_19072009_b = "<p>Ah, the great mystery of talking to the smart card is solved. The tool that helped me to do it is called CHIPDRIVE Smartcard Commander. It is a free tool and can easily be found, downloaded and installed.</p><p>When I positioned the card in the reader and started the Smartcard Commander, I could immediately see that it knows a lot of stuff about the card.</p>";
        public const string content_19072009_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/19072009_Chipdrive_SmartCard_Commander.png\" alt=\"Chipdrive SmartCard Commander\" /></div>" +
            "<p>But what's more important, it has some sample scripts that can be loaded when I select \"CPU Card\" from the System tree and use the Load Script button. The sample script shows me how to construct commands that can be send to the card, I can also run them immediately and see the results.</p><p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/19072009_Chipdrive_SmartCard_Commander_Script.png\" alt=\"Chipdrive SmartCard Commander Script\" /></div>" +
            "</p><p>I only need to send the proper commands now...</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_19072009_k = "Software development C# SmartCard CHIPDRIVE Commander debugging";
        public const string content_19072009_d = "CHIPDRIVE SmartCard Commander is a very useful tool in debugging smart card applications.";

        public const string content_17072009_b = "<p>Resolved the problem with the Smart Card reader. After everything else failed, I tried installing the pritner and reader on the clean PC. Surprisingly, it worked. Then I tried uninstalling all drivers from my PC, restarting and reinstalling again. Unsurprisingly, it did not work (I tried doing this before).</p>";
        public const string content_17072009_r = "<p>Next thing, I decided to compare the driver versions between my PC and clean PC. And here it was - my driver said \"SCM Microsystems 4.35.00.01\" and the one on the clean PC said \"SCM Microsystems 5.15\". And, of course, the S331DICL.sys files had different dates. So, I copied the S331DICL.sys and installed the drivers again. That did not quite help though, the driver version was now the proper one, but the device version itself was not.</p><p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/17072009_Driver_File_Details.png\" alt=\"Driver File Details\" /></div>" +
            "</p><p>Why are the versions different? Only when I searched for S331DICL.sys on the whole computer I could figure out what was the most likely reason for my problem - looks like the old the driver version was installed by the 3M scanner installer. I found the old S331DICL.sys in one of the subfolders under its Program Files folder. Now, when I was installing the driver, it remembered the location and used the old file that came with the 3M scanner. So I uninstalled the 3M application, made sure that the S331DICL.sys file is deleted completely from my computer, copied over the new version and pointed to the new version of S331DICL.sys file when installing the smart card drivers. Now it finally worked.</p><p>Next thing is to actually implement communication to the smart card ...</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_17072009_k = "SCM Microsystems Smart Card Reader software development driver versions";
        public const string content_17072009_d = "Programming the Smart Card Reader, resolving driver issues";

        //"Unit Testing - First Attempts"
        public const string content_15072009_b = "<p>I had some time to use while I was investigating the smart card issues, so I decided to do the right thing. Something I have never done before. To learn how to write and use unit tests. Since I had a very small application that was testing the capabilities of the printer, it looked like a perfect guinea pig for my experiment. It turned out that writing tests is not so hard as I expected it to be. Well, I can not promise that I did it right, of course, because no one writes them here anyway and there is no one to mentor me or point to my mistakes.</p>";
        public const string content_15072009_r = "<p>So first of all I downloaded and installed NUnit framework</p><a href=\"http://www.nunit.org/index.php?p=download\">NUnit framework</a><p>Then I added a project of type class library to my solution and a single class called UnitTest to this solution. Here is the full code of the UnitTest class:</p><pre class=\"brush:csharp\">" +
            @"using System;<br />using NUnit.Framework;<br />using SmartCardTest;<br />using DataCardCP40;<br /><br />[TestFixture]<br />public class UnitTest<br />{<br />    public DataCardPrinter printer;<br />    ICE_API.DOCINFO di;<br /><br />    [Test]<br />    public void CreateObjects()<br />    {<br />        printer = new DataCardPrinter();<br />        di = DataCardPrinter.InitializeDI();<br />        printer.CreateHDC();<br />        Assert.AreNotEqual(printer.Hdc, 0);<br />        Assert.Greater(di.cbSize, 0);<br />    }<br /><br />    [Test]<br />    public void SetInteractiveMode()<br />    {<br />        int res = ICE_API.SetInteractiveMode(printer.Hdc, true);<br />        Assert.Greater(res, 0);<br />    }<br /><br />    [Test]<br />    public void StartDoc()<br />    {<br />        int res = ICE_API.StartDoc(printer.Hdc, ref di);<br />        Assert.Greater(res, 0);<br />    }<br /><br />    [Test]<br />    public void StartPage()<br />    {<br />        int res = ICE_API.StartPage(printer.Hdc);<br />        Assert.Greater(res, 0);<br />    }<br /><br />    [Test]<br />    public void RotateCardSide()<br />    {<br />        int res = ICE_API.RotateCardSide(printer.Hdc, 1);<br />        Assert.Greater(res, 0);<br />    }<br /><br />    [Test]<br />    public void FeedCard()<br />    {<br />        int res = ICE_API.FeedCard(printer.Hdc, ICE_API.ICE_SMARTCARD_FRONT + ICE_API.ICE_GRAPHICS_FRONT);<br />        Assert.Greater(res, 0);<br />    }<br /><br />    [Test]<br />    public void SmartCardContinue()<br />    {<br />        int res = ICE_API.SmartCardContinue(printer.Hdc, ICE_API.ICE_SMART_CARD_GOOD);<br />        Assert.Greater(res, 0);<br />    }<br /><br />    [Test]<br />    public void EndPage()<br />    {<br />        int res = ICE_API.EndPage(printer.Hdc);<br />        Assert.Greater(res, 0);<br />    }<br /><br />    [Test]<br />    public void EndDoc()<br />    {<br />        int res = ICE_API.EndDoc(printer.Hdc);<br />        Assert.Greater(res, 0);<br />    }<br />}</pre>" +
            "<p>There's not much to explain. First I create the objects required and verify that the device context was created and the DOCINFO struct was initialized. All the other tests just check the return codes of the printer functions. The error code is 0, so the check is for return value being greater than zero.</p><p>After compiling and fixing errors I realized that I have no way to set the sequence of the execution. Supposedly, as the theory teaches us, each test should be able to run alone and independent of whether the rest were passed, failed or run at all. Well, does not work so well in my case - if I want to test that the card can be ejected from the printer, I need to somehow insert it first! I found out, however, that the tests are executed in the alphabetic order of their names. Okay, that'll do for now. So I just renamed my tests like this A_CreateObjects(), B_SetInteractiveMode() etc. Then I compiled the solution, creating the \"DataCardTest.dll\". Next step is to run NUnit and open the dll. Wow! The smart thing can see all my tests now. When ready, just select Test->Run all from the menu and enjoy ...</p><p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/15072009_Tests_Pass.png\" alt=\"Tests Pass\" /></div>" +
            "</p><p>It does not alway end that well, however - it might be like this (see how it tells what was the line where the error happened and how the expected test result was different from the actual).</p><p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/15072009_Tests_Fail.png\" alt=\"Tests Fail\" /></div>" +
            "</p><p>What happened here? Took me some time to figure out ... the default printer was not set to my card printer.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_15072009_k = "Software Development C# Unit Testing";
        public const string content_15072009_d = "Staring to learn the art of unit testing";

        //"Smart Cards Hurt - 2"
        public const string content_10072009_b = "<p>Now, the slightly harder part is communicating with the Smart Card reader. Most, if not all, of the functionality resides within the winscard.dll. For functions reference, this MSDN page could be a start.</p><p><a href=\"http://msdn.microsoft.com/en-us/library/aa924246.aspx\">Smart Card Functions</a></p>";
        public const string content_10072009_r = "<p>I also found a nice example using google code search which resides here</p><p><a href=\"http://chenguo.googlecode.com/svn/trunk/DriverTest/DriverTest/ACR120Driver.cs\">ACR120Driver.cs</a></p><p>and using this code as a template, I used the following code to test the functionality of my SCM reader.</p><pre class=\"brush:csharp\">" +
            @"long retCode;<br />int hContext = 0;<br />int ReaderCount = 0;<br />int Protocol = 0;<br />int hCard = 0;<br />string defaultReader = null;<br />int SendLen, RecvLen;<br /><br />byte[] SendBuff = new byte[262];<br />byte[] RecvBuff = new byte[262];<br /><br />ModWinsCard.SCARD_IO_REQUEST ioRequest;<br /><br />retCode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, ref hContext);<br />if (retCode != ModWinsCard.SCARD_S_SUCCESS)<br />{<br /> System.Diagnostics.Debug.WriteLine(ModWinsCard.GetScardErrMsg(retCode));<br />}<br /><br />retCode = ModWinsCard.SCardListReaders(hContext, null, null, ref ReaderCount);<br /><br />if (retCode != ModWinsCard.SCARD_S_SUCCESS)<br />{<br /> System.Diagnostics.Debug.WriteLine(ModWinsCard.GetScardErrMsg(retCode));<br />}<br /><br />byte[] retData = new byte[ReaderCount];<br />byte[] sReaderGroup = new byte[0];<br /><br />//Get the list of reader present again but this time add sReaderGroup, retData as 2rd & 3rd parameter respectively.<br />retCode = ModWinsCard.SCardListReaders(hContext, sReaderGroup, retData, ref ReaderCount);<br /><br />if (retCode != ModWinsCard.SCARD_S_SUCCESS)<br />{<br /> System.Diagnostics.Debug.WriteLine(ModWinsCard.GetScardErrMsg(retCode));<br />}<br /><br />//Convert retData(Hexadecimal) value to String <br />string readerStr = System.Text.ASCIIEncoding.ASCII.GetString(retData);<br />string[] rList = readerStr.Split('\0');<br /><br />foreach (string readerName in rList)<br />{<br /> if (readerName != null && readerName.Length > 1)<br /> {<br />  defaultReader = readerName;<br />  break;<br /> }<br />}<br /><br />if (defaultReader != null)<br />{<br /> retCode = ModWinsCard.SCardConnect(hContext, defaultReader, ModWinsCard.SCARD_SHARE_DIRECT,<br />       ModWinsCard.SCARD_PROTOCOL_UNDEFINED, ref hCard, ref Protocol);<br /> //Check if it connects successfully<br /> if (retCode != ModWinsCard.SCARD_S_SUCCESS)<br /> {<br />  string error = ModWinsCard.GetScardErrMsg(retCode);<br /> }<br /> else<br /> {<br />  int pcchReaderLen = 256;<br />  int state = 0;<br />  byte atr = 0;<br />  int atrLen = 255;<br /><br />  //get card status<br />  retCode = ModWinsCard.SCardStatus(hCard, defaultReader, ref pcchReaderLen, ref state, ref Protocol, ref atr, ref atrLen);<br /><br />  if (retCode != ModWinsCard.SCARD_S_SUCCESS)<br />  {<br />   return;<br />  }<br /><br />  //read/write data etc.<br />  <br />  .....<br /> }<br />}" +
            "</pre><p>ModWinsCard.cs is, again, a wrapper for the winscard.dll functions, data structures, and declares all required constants.</p><p>Anyway, this code actually worked fine, except one little detail - the state variable that gets returned by the SCardStatus returned the value of 2. And the possible values are explained here:</p><p><a href=\"http://msdn.microsoft.com/en-us/library/aa924671.aspx\">SCardStatus</a></p><p>\"2\" is SCARD_PRESENT, which means \"A card is present in the card reader, but it is not in position for use\". A better result would be something like SCARD_NEGOTIABLE which is \"The card has been reset and is waiting for protocol negotiation\".</p><p>Also, using SCardConnect with preferred protocol set to T0 or T1 returned SCARD_W_UNRESPONSIVE_CARD error.</p><p>Now this is the point where I had to consult with the printer manufacturer because there's a number of possible reasons for the errors - hardware, firmware, drivers or incompatible card. Work still in progress.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_10072009_k = "winscard.dll smart card reader c# development";
        public const string content_10072009_d = "Programming the Smart Card Reader";

        //"Smart Cards Hurt - 1"
        public const string content_07072009_b = "<p>So here's the new toy I've got to play with - the DataCard CP40 Plus card printer with the SCM SCR331-DI Smart Card reader.</p>";
        public const string content_07072009_r = "<p><a href=\"http://www.datacard.com/id-card-printer-support-and-drivers/cp40-plus-card-printer\">Datacard CP40 Plus</a></p><p>Developing the application for the printer consists mostly of two parts - communicating with the printer and communicating with the smart card reader. You tell the printer to pick up the card, you tell the printer to position the card for smart card processing, you tell the smart card reader to write data to the smart card, you tell the printer to encode the magstripe and print something on the card, you tell the printer to finish with the print job.</p><p>It does not look so easy when you read the manual. This is how the programming flow looks like:</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/07072009_Programming_for_Smart_Card.png\" alt=\"Programming for Smart Card\" /></div>" +
            "<p>In reality, though, the whole printer communication is mostly done by the following code:</p><pre class=\"brush:csharp\">" +
            @"printer.Hdc = PrintDoc.PrinterSettings.CreateMeasurementGraphics().GetHdc().ToInt32();<br /><br />/* Set Interactive mode */<br />if (ICE_API.SetInteractiveMode(printer.Hdc, true) > 0)<br />{<br /> ICE_API.DOCINFO di = new ICE_API.DOCINFO();<br /> /* Initialize DOCINFO */<br /> di.cbSize = 16;<br /> di.lpszDocName = ""Card Printer SDK Test"";<br /> di.lpszDataType = string.Empty;<br /> di.lpszOutput = string.Empty;<br /><br /> /* Start document and page */<br /> if (ICE_API.StartDoc(printer.Hdc, ref di) > 0)<br /> {<br />  if (ICE_API.StartPage(printer.Hdc) > 0)<br />  {<br />   /* Set card rotation on */<br />   ICE_API.RotateCardSide(printer.Hdc, 1);<br />   /* Feed the card into the smart card reader */<br />   if (ICE_API.FeedCard(printer.Hdc, ICE_API.ICE_SMARTCARD_FRONT + ICE_API.ICE_GRAPHICS_FRONT) > 0)<br />   {<br />    /* Put any SmartCard processing/communication here */<br />    TalkToSmartCard();<br />   }<br />   /* Remove the card from the reader and continue printing */<br />   ICE_API.SmartCardContinue(printer.Hdc, ICE_API.ICE_SMART_CARD_GOOD);<br />   /* End the page */<br />   ICE_API.EndPage(printer.Hdc);<br />  }<br />  /* End job */<br />  ICE_API.EndDoc(printer.Hdc);<br /> }<br />}" +
            "</pre><p>The ICE_API mostly contains wrappings for the functions from the ICE_API.dll which comes with the printer and defines some constants and data structures, like this</p><pre class=\"brush:csharp\">" +
            @"[StructLayout(LayoutKind.Sequential)]<br />public struct DOCINFO<br />{ <br /> public int cbSize;<br /> public string lpszDocName;<br /> public string lpszOutput;<br /> public string lpszDataType;<br />}<br /><br />........<br /><br />[DllImport(""ICE_API.dll"")]<br />public static extern int FeedCard(int hdc, int dwCardData);<br /><br />[DllImport(""ICE_API.dll"")]<br />public static extern int GetCardId(int hdc, CARDIDTYPE pCardId);<br /><br />[DllImport(""ICE_API.dll"")]<br />public static extern int SmartCardContinue(int hdc, int dwCommand);<br /><br />.........<br /><br />public const int ICE_SMARTCARD_FRONT = 0x10;<br />public const int ICE_GRAPHICS_FRONT = 0x1;<br /><br />public const int ICE_SMART_CARD_GOOD = 0;<br />public const int ICE_SMART_CARD_ABORT = 1;</pre>" +
            "<p>Now that was the easy part.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_07072009_k = "winscard.dll smart card reader c# development";
        public const string content_07072009_d = "Programming the Smart Card Reader";

        //"Embedded Technology Workshop"
        public const string content_27062009_b = "<p>Some members of our team, including myself, have attended a small, half-day workshop on Microsoft Embedded Technologies. Here's how the agenda looked like:</p>";
        public const string content_27062009_r = "<table border=1><tr><td width=\"20%\">TIME</td><td>TOPIC</td></tr><br /><tr><td>12:30</td><td>Registration and light lunch</td></tr><br /><tr><td>13:00 – 13:05</td><td>Welcome speech</td></tr><br /><tr><td>13:10 – 13:30</td><td>Introduction: Why use Embedded? What are the benefits?</td></tr><br /><tr><td>13:30 to 15:00</td><td>Module 1: Windows Embedded Standard – Development Suite, Tools and Utilities.<br /><br />Module 2: Embedded Enabling Features.</td></tr><br /><tr><td>15:00</td><td>Tea/Coffee Break </td></tr><br /><tr><td>14:30 to 16:00</td><td>Module 3: Demo:<br />- Building an image using File Based Write Filter<br />Module 4: Componentization of 3rd Party Drivers.<br /><br />Module 5: Demo:<br />- Creating Custom Components in your image.</td></tr><br /><tr><td>16:00 - 16:30</td><td>Q & A </td></tr><br /><tr><td>16:30</td><td>Closing and thank you </td></tr><br /></table><p>It was held at the local Microsoft office (not Microsoft Office, but the actual place where like, people work). The office was pretty boring by the way - no huge Bill Gates portaits, no sacrifices etc ... maybe they clean up when they know strangers will be present.</p><p>Anyway, the topic was mostly about how to assemble your own embedded OS from parts of dismembered Windows XP or Windows Embedded Standard etc. Basically, if I know exactly what peripherial devices will my hardware use, I can only include drivers for these devices, hugely reducing the size of the OS. Also, I may choose to cut out other elements of the OS - I may get rid of the whole explorer shell altogether. They mentioned that the smallest OS they have actually seen used by one of the clients was about 8MB in size. Quite impressive compared to the standard XP footprint of about 1.9GB.</p><p>As they said, the goal of the workshop was to show the participants that the process of assembling your own OS is not as complicated as people usually think. Can't say they succeeded - looked fairly complex to me so far ...</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_27062009_k = "Embedded Technology Workshop";
        public const string content_27062009_d = "Embedded Technology Workshop";

        //"VSS => TFS converter application update"
        public const string content_23062009_b = "<p>After a bit of thought, I decided what would be the easiest and the most convinient way to run my small application that helps to convert projects from VSS to TFS.</p><p>I will start the command line tool passing them together with parameters to cmd.exe application, and run cmd.exe with the -k parameter to prevent the command window from closing after the tool exits. I will keep the ID of the process that runs cmd.exe. Next time I run the cmd.exe, I will check if there is ID present, and if yes, I will kill the process, and then start a new one. This way the user's computer will not be littered with command windows.</p>";
        public const string content_23062009_r = "<p>So, the small class that would take care of process management looks like this</p><pre class=\"brush:csharp\">" +
            "public class ProcessFactory<br />{<br /> private static int _currentProcessID = -1;<br /><br /> private static Process _cmdProcess;<br /><br /> private static ProcessStartInfo _startInfo;<br /><br /> public static ProcessStartInfo StartInfo<br /> {<br />  get<br />  {<br />   if (_startInfo == null)<br />   {<br />    _startInfo = new ProcessStartInfo();<br />   }<br />   return _startInfo;<br />  }<br /> }<br /><br /> public static void RunProcess(string filename, string args, string workingdir)<br /> {<br />  try<br />  {<br />   if (_currentProcessID > 0)<br />   {<br />    Process processToClose = Process.GetProcessById(_currentProcessID);<br />    if (processToClose != null)<br />    {<br />     processToClose.Kill();<br />    }<br />    _currentProcessID = -1;<br />   }<br /><br />   StartInfo.FileName = filename;<br />   StartInfo.Arguments = args;<br />   StartInfo.WorkingDirectory = workingdir;<br /><br />   _cmdProcess = Process.Start(StartInfo);<br /><br />   if (_cmdProcess != null)<br />   {<br />    _currentProcessID = _cmdProcess.Id;<br />   }<br />  }<br />  catch (Exception ex)<br />  {<br />   Logger.LogInfo(ex);<br />  }<br /> }<br />}" +
            "</pre><p>And then I just call the RunProcess as many times as I want, but the user will not be bothered with \"leftover\" command windows</p><pre class=\"brush:csharp\">" +
            @"string args = ""/k ssarc.exe -d- -i -y"" + SettingsManager.GetSetting(Constants.VSSLOGIN)<br /> + "","" +<br /> SettingsManager.GetSetting(Constants.VSSPASSWORD) + "" -s"" +<br /> SettingsManager.GetSetting(Constants.VSSDBFOLDER) + "" "" +<br /> SettingsManager.GetSetting(Constants.VSSARCHIVEFILENAME)<br /> + "".ssa"" + "" \"""" + SettingsManager.GetSetting(Constants.VSSPROJECTNAME) + ""\"""";<br /><br />ProcessFactory.RunProcess(""cmd.exe"", args, SettingsManager.GetSetting<br />(Constants.VSSINSTFOLDER));<br /><br />.....<br /><br />args = ""/k ssrestor.exe \""-p"" + SettingsManager.GetSetting(Constants.VSSPROJECTNAME)<br /> + ""\"""" + "" -s"" + SettingsManager.GetSetting(Constants.VSSARCHIVEFOLDER) +<br /> "" -y"" + SettingsManager.GetSetting(Constants.VSSLOGIN) + "","" +<br /> SettingsManager.GetSetting(Constants.VSSPASSWORD) + "" "" +<br /> SettingsManager.GetSetting(Constants.VSSARCHIVEFILENAME) + "".ssa"" +<br /> "" \"""" + SettingsManager.GetSetting(Constants.VSSPROJECTNAME) + ""\"""";<br /><br />ProcessFactory.RunProcess(""cmd.exe"", args, SettingsManager.GetSetting<br />(Constants.VSSINSTFOLDER));" + "</pre><p>etc., until finished.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_23062009_k = "VSS TFS migration";
        public const string content_23062009_d = "Automating the VSS to TFS conversion process";

        //"Using the Process class"
        public const string content_16062009_b = "<p>I was not too loaded with work recently so I decided to write a small application that would help to automate the process of converting existing Visual SourceSafe projects to Team Foundation Server. The idea is to get some information from the user first, and then spare him from some manual tasks - running tools like ssarc, ssrestor or VSSConverter, manually creating and editing XML files etc.</p><p>When the application starts, the user needs to provide (or just check) the following information:</p>";
        public const string content_16062009_r = "<ul><li>A folder where Visual SourceSafe is installed</li><li>A folder where Visual SourceSafe database is located</li><li>Visual SourceSafe database administrator login credentials</li><li>The name of the Visual SourceSafe project to be converted</li><li>A folder that will be used during conversion to restore VSS database, keep XML files etc.</li><li>SQL Server that will be used by the converter</li><li>A name of the TFS and the port number</li><li>A name of the project on the TFS where the converted files will go</li></ul><p>A significant chunk of the application functionality is just wrapping the calls to command line tools so that the user does not have to bother with manually locating them, typing the correct parameters etc.</p><p>For that purpose, the .NET class Process is quite handy.<br />Here is the example:<br />To archive the VSS project MyProject which is in the VSS database located on MyServer into the archive file called MyArchive.ssa I need to run the following from the command line:</p><p>>\"C:\\Program Files\\Microsoft Visual SourceSafe\\ssarc.exe\" \"-d- -i -yadmin,password -s\\MyServer\\ MyArchive.ssa \\$/MyProject\\\"</p><p>To run this command from the C# code I can use the following code:</p><pre class=\"brush:csharp\">" +
            @"ProcessStartInfo startInfo = new ProcessStartInfo();<br />startInfo.FileName = ""ssarc.exe"";<br />startInfo.Arguments = @""-d- -i -yadmin,password -s\\MyServer\ MyArchive.ssa \$/MyProject\"";<br />startInfo.WorkingDirectory = @""C:\Program Files\Microsoft Visual SourceSafe"";<br />Process process = Process.Start(startInfo);" + "</pre><p>This is quite self-explanatory.</p><p>There are a couple of things that I had trouble with however. First thing is logging. It would be nice to log the errors and messages that the process generates. This is possible, according to the MSDN article.</p><p><a href=\"http://msdn.microsoft.com/en-us/library/system.diagnostics.processstartinfo.aspx\">ProcessStartInfo Class</a></p><blockquote>Standard input is usually the keyboard, and standard output and standard error are usually the monitor screen. However, you can use the RedirectStandardInput, RedirectStandardOutput, and RedirectStandardError properties to cause the process to get input from or return output to a file or other device. If you use the StandardInput, StandardOutput, or StandardError properties on the Process component, you must first set the corresponding value on the ProcessStartInfo property. Otherwise, the system throws an exception when you read or write to the stream.</blockquote><p>However, if I redirect standard output to the text file, for example, the user is unable to see it. And some of the tools used required interaction with the user. So it looks like I either interact with the user, or log the messages somewhere.</p><p>Also, when the process completes, it closes the window where it was running. So, if there is a message shown by the process when it exits, the user does not have time to read it. It might be frustrating when the process exits with an error message and the user does not know what exactly the error was. And it can not be logged because the output can not be redirected somewhere - the user needs to see it on the screen.</p><p>I will still be looking for the 'elegant' solution for this, but so far I found a workaround: rather than starting the process itself, I can start the command line using the \"cmd.exe\" and pass the whole tool together with the parameters as a parameter to cmd.exe.</p><p><a href=\"http://www.microsoft.com/resources/documentation/windows/xp/all/proddocs/en-us/cmd.mspx?mfr=true\">CMD</a></p><p>The trick is that specifying the /k parameter prevents the command window from closing after the process exits. Here is how the previous code will look like when changed according to my workaround:</p><pre class=\"brush:csharp\">" +
            @"ProcessStartInfo startInfo = new ProcessStartInfo();<br />startInfo.FileName = ""cmd.exe"";<br />startInfo.Arguments = @""/k ""C:\Program Files\Microsoft Visual SourceSafe\ssarc.exe"" ""-d- -i -yadmin,password -s\\MyServer\ MyArchive.ssa \$/MyProject\"""";<br />Process process = Process.Start(startInfo);" + "</pre><p>I will be looking for a better solution when I have time to improve this application.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_16062009_k = "VSS TFS migration";
        public const string content_16062009_d = "Automating the VSS to TFS conversion process";

        //"Small Things Refreshed Today"
        public const string content_09062009_b = "<p>I had to write a small Windows Forms application today. It just gets some user input, creates an XML file, sends it to the webservice, gets the response, parces it and shows the results to the user. Good thing is that I had to remind myself how to use two simple things.</p><h3>1. Saving and retrieving values using the app.config file.</h3>";
        public const string content_09062009_r = "<p>If I want to get some values from the app.config file, I can keep them in the appSetting section and the whole app.config file for the small application can be as simple as that</p><pre class=\"brush:xml\">" +
            @"<?xml version=""1.0"" encoding=""utf-8""?><br /><configuration><br /> <appSettings><br />  <add key=""MYKEY1"" value=""myValue1"" /><br />  <add key=""MYKEY2"" value=""myValue2"" /><br /> </appSettings><br /></configuration><br />" +
            "</pre><p>To read the values I need to do just the following (after I add a reference to System.configuration to the project):</p><pre class=\"brush:csharp\">" +
            @"string myFirstValue = ConfigurationManager.AppSettings.Get(""MYKEY1"");" +
            "</pre><p>To update the values I need to put a little bit more effort</p><pre class=\"brush:csharp\">" +
            @"Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);<br />AppSettingsSection appSettings = config.AppSettings;<br /><br />appSettings.Settings[""MYKEY1""].Value = myNewValue1;<br />appSettings.Settings[""MYKEY2""].Value = myNewValue2;<br /><br />config.Save(ConfigurationSaveMode.Modified);<br />ConfigurationManager.RefreshSection(""appSettings"");" +
            "</pre><p>It is useful to know that this would not work at debug time, though - it will not throw an exception, but the values would not be updated too. I spent a few minutes trying to find out why it does not work before I understood that this behaviour is expected.</p><h3>2. Creating the XML document.</h3><p>Of course, for the purposes of my application, where the whole XML is maybe 10 to 15 elements, I could go with the following:</p><pre class=\"brush:csharp\">" +
            @"string myXML = ""<?xml version=\""1.0\"" encoding=\""UTF-8\""?><message><header SchemaVersion=""2.0"">"";<br />myXML += ""<someID>"" + someID + ""</someID>"";<br />...<br />myXML += ""</message>"";<br />return myXML;" +
            "</pre><p>The code would actually be shorter than the \"proper\" XML handling, take less time to write and maybe even will work faster (especially if I use a StringBuilder to concatenate strings). I did it the \"proper\" way, however - for practice.</p><p>To create a document</p><pre class=\"brush:csharp\">" +
            @"XmlDocument xmlDoc = new XmlDocument();" + "</pre><p>To create a declaration</p><pre class=\"brush:csharp\">" +
            @"XmlDeclaration xDec = xmlDoc.CreateXmlDeclaration(""1.0"", ""UTF-8"", null);" + "</pre><p>To create an element in a format of <pre class=\"brush:xml\">" +
            @"<MYKEY1>myValue1</MYKEY1>" + "</pre> I created a small helper function</p><pre class=\"brush:csharp\">" +
            @"private XmlElement CreateElementFromNameValue(string name, string value)<br />{<br /> XmlElement element = xmlDoc.CreateElement(name);<br /> element.InnerText = value;<br /> return element;<br />}" + "</pre><p>To create an attribute to the element</p><pre class=\"brush:csharp\">" +
            @"XmlElement xmlHeader = xmlDoc.CreateElement(""header"");<br />XmlAttribute schema = xmlDoc.CreateAttribute(""SchemaVersion"");<br />schema.Value = ""2.0"";<br />xmlHeader.SetAttributeNode(schema);" + "</pre><p>To bring it all together</p><pre class=\"brush:csharp\">" +
            @"XmlDocument xmlDoc = new XmlDocument();<br />XmlDeclaration xDec = xmlDoc.CreateXmlDeclaration(""1.0"", ""UTF-8"", null);<br /><br />XmlElement request = xmlDoc.CreateElement(""request"");<br />XmlAttribute schema = xmlDoc.CreateAttribute(""SchemaVersion"");<br />schema.Value = ""2.0"";<br />request.SetAttributeNode(schema);<br /><br />request.AppendChild(CreateElementFromNameValue(""MYKEY1"", ""myValue1""));<br />request.AppendChild(CreateElementFromNameValue(""MYKEY2"", ""myValue2""));<br /><br />xmlDoc.AppendChild(xDec);<br />xmlDoc.AppendChild(request);" +
            "</pre><p>Expected InnerXml of the xmlDoc</p><pre class=\"brush:xml\">" +
            @"<?xml version=""1.0"" encoding=""UTF-8""?><br /><request SchemaVersion=""2.0""><br /> <MYKEY1>myValue1</MYKEY1><br /> <MYKEY2>myValue2</MYKEY2><br /></request>" +
            "</pre><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_09062009_k = "app.config XML C# software development";
        public const string content_09062009_d = "Saving and retrieving values from app.config file and working with XML files";

        //"VSS => TFS Migration"
        public const string content_25052009_b = "<p>Now that I have the TFS installed to play with, my next task is to come up with the process to transfer existing projects from Visual Source Save. Since the current VSS database is fairly huge, and we do not want to transfer the whole thing at once, I came up with the following process:</p>";
        public const string content_25052009_r = "<ul><br /><li>Select project(s) to be transferred from VSS database</li><br /><li>Back up project(s) and restore them to the new VSS database</li><br /><li>Fix the issues in the new VSS database with the Analyze tool</li><br /><li>Run the VSSConverter tool in analyse mode</li><br /><li>Get the TFS ready for migration</li><br /><li>Prepare the migration settings file</li><br /><li>Run the VSSConverter tool in migration mode</li><br /><li>Verify the results of the migration</li></ul><p>This may look rather lengthy and complex, but it makes sure that the current VSS database remains untouched, which is quite important for obvious reasons.</p><p>Here is how I migrated a small project from VSS to TFS in a few more detail:</p><p><h3>Select project(s) to be transferred from VSS database</h3></p><p>Let's say we want to transfer MySmallProject which is located in $/MyMiscProjects/MySmallProject in a large VSS database.</p><p><h3>Back up project(s) and restore them to the new VSS database</h3></p><p>Microsoft has two utilities for backing up and restoring VSS projects, SSARC and SSRESTOR. Their parameters are described in detail here:</p><p><a href=\"http://msdn.microsoft.com/en-us/library/t9d14fh1(VS.80).aspx\">SSARC</a><a href=\"http://msdn.microsoft.com/en-us/library/b4ch74ts(VS.80).aspx\">SSRESTOR</a></p><p>They usually can be found in the SourceSafe folder (i.e. C:\\Program Files\\Microsoft Visual SourceSafe)</p><p>First, I create a new VSS database (VSSTransfer) where I'm the admin. Next, I need to have admin rights in the initial VSS database and, of course, to know where it is located. Then I can run the SSARC command like that:</p><p><i><u>ssarc -d- -i -yadmin,password -s\\PathToVSSDB\\MyHugeVSSDB CodeProject.ssa \"$/MyMiscProjects/MySmallProject\"</u></i></p><p>This backs up the MySmallProject with default parameters, without deleting files from the old database \"MyHugeVSSDB\", into the CodeProject.ssa archive file.</p><p>Next, I restore the project into the new empty database I created.</p><p><i><u>ssrestor \"-p$/MySmallProject\" -sC:\\VSSTransfer -yadmin,password CodeProject.ssa \"$/MyMiscProjects/MySmallProject\"</u></i></p><p><h3>Fix the issues in the new VSS database with the Analyze tool</h3></p><p>This is just running the Analyze tool with the -F parameter to fix possible issues in the VSS.</p><p><h3>Run the VSSConverter tool in analyse mode</h3></p><p>The VSSConverter is a Microsoft tool that comes with the TFS and allows migrating data from VSS database into the TFS database. More info here:</p><p><a href=\"http://msdn.microsoft.com/en-us/library/ms253090(VS.80).aspx\">VSSConverter Command-Line Utility for Source Control Migration</a></p><p>To run the VSSConverter, a settings file has to be prepared first. Here is the sample:</p><pre class=\"brush:xml\"><?xml version=\"1.0\" encoding=\"utf-8\"?><br /><SourceControlConverter><br />      <ConverterSpecificSetting><br />            <Source name=\"VSS\"><br />                  <VSSDatabase name=\"C:\\VSSTransfer\"></VSSDatabase><br />            </Source><br />            <ProjectMap><br />            <Project Source=\"$/MySmallProject\"></Project><br />            </ProjectMap><br />      </ConverterSpecificSetting><br />      <Settings><br />       <Output file=\"Analysis.xml\"></Output><br />     </Settings><br /></SourceControlConverter></pre><p>(if we need to transfer multiple projects, there can be multiple 'Project' elements under 'ProjectMap')</p><p>Now I save the file as settings.xml and run the VSSConverter tool (which is located in drive:\\Program Files\\Microsoft Visual Studio 9.0\\Common7\\IDE ):</p><p><i><u>VSSConverter analyze settings.xml</u></i></p><p>(An important note, though - the VSSConverter should be the one that comes with TFS SP1. I tried to use the tool from the original TFS install and got troubles with history - it was not migrated at all).</p><p>Two files will be created, Analysis.xml and UserMap.xml</p><p><h3>Get the TFS ready for migration</h3></p><p>First of all, create the target project, i.e. MyTFSSmallProject. Then, look at the UserMap.xml. It lists all VSS users who performed action in the database. It looks like this:</p><pre class=\"brush:xml\"><?xml version=\"1.0\" encoding=\"utf-8\"?><br /><UserMappings xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"<br /> xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><br />  <UserMap From=\"Admin\" To=\"\" /><br />  <UserMap From=\"Bob\" To=\"\" /><br />  <UserMap From=\"Fred\" To=\"\" /><br />  <UserMap From=\"Jack\" To=\"\" /><br /></UserMappings></pre><p>To map users properly, we need to add them to TFS. If the user no longer exists, he can be mapped to any user - TFS admin or his team leader, for example. So the UserMap.xml will end up looking like this:</p><pre class=\"brush:xml\">" +
            @"<?xml version=""1.0"" encoding=""utf-8""?><br /><UserMappings <br />xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" <br />xmlns:xsd=""http://www.w3.org/2001/XMLSchema""><br />  <UserMap From=""Admin"" To=""MYDOMAIN\TFSAdmin"" /><br />  <UserMap From=""Bob"" To=""MYDOMAIN\Bob"" /><br />  <UserMap From=""Fred"" To=""MYDOMAIN\Fred"" /><br />  <UserMap From=""Jack"" To=""MYDOMAIN\TFSAdmin"" /><br /></UserMappings>" +
            "</pre><p><h3>Prepare the migration settings file</h3></p><p>Modify the settings.xml file to specify the SQL Server that is going to be used for the migration process and Team Foundation Server, the users map file and the destination project on the TFS as follows and save it as migration_settings.xml. The SQL server does not have to be the one where TFS database are located, and the user performing the migration need to have CREATE DATABASE permission in the SQL Server.</p><pre class=\"brush:xml\">" +
            @"<?xml version=""1.0"" encoding=""utf-8""?><br /><SourceControlConverter><br />      <ConverterSpecificSetting><br />            <Source name=""VSS""><br />              <VSSDatabase name=""C:\VSSTransfer""></VSSDatabase><br />  <UserMap name=""C:\VSSTransfer\Usermap.xml""></UserMap><br />  <SQL Server=""MYLOCALSQL""></SQL><br />            </Source><br />            <ProjectMap><br />              <Project Source=""$/MySmallProject"" Destination=""$/MyTFSSmallProject""><br /></Project><br />            </ProjectMap><br />      </ConverterSpecificSetting><br />      <Settings><br /> <TeamFoundationServer name=""MYTFSSERVER"" port=""8080"" protocol=""http""><br /></TeamFoundationServer><br />     </Settings><br /></SourceControlConverter>" +
            "</pre><p><h3>Run the VSSConverter tool in migration mode</h3></p><p>Run the VSSConverter tool in migration mode as follows:</p><p><i><u>VSSConverter Migrate migration_setting.xml</u></i></p><p>A report file called VSSMigrationReport.xml will be created if the migration process runs successfully. A log file called VSSConverter.log will contain information messages about the migration process.</p><p><h3>Verify the results of the migration</h3></p><p>Log in to TFS, go to the project's Source Control Explorer, check the files, history etc. Get the latest version, build it. Have fun.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_25052009_k = "VSS TFS migration";
        public const string content_25052009_d = "Migrating source code from VSS to TFS";

        //"TFS Disaster Resolved"
        public const string content_15052009_b = "<p>Okay, so today the TFS was finally installed. Unfortunately, I cannot tell for sure what the exact thing that fixed it was, because we changed more than one thing. Firstly, the reporting services were uninstalled completely from the data tier. Secondly, we found some information on slipstreaming the SP1 for TFS 2008 and applied it to the installation package.</p>";
        public const string content_15052009_r = "<a href=\"http://www.woodwardweb.com/vsts/creating_a_tfs.html\">Creating a TFS 2008 with SP1 Slipstreamed ISO image</a><p>And lastly, we ran the installation from the beginning ... again. I personally think that removing the reporting services from the data tier did it. We'll need to have reporting services on the data tier later, we'll see if installing them back will break TFS or not. But for now, this weight is off my shoulders.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_15052009_k = "TFS 2008 and IIS error troubleshooting";
        public const string content_15052009_d = "Work on resolving TFS 2008 issues and IIS issues complete.";

        //"TFS/IIS Disaster Update"
        public const string content_14052009_b = "<p>I got a response from the Microsoft support person, who have told me earlier that he was able to reporoduce our error.</p><p>So, according to what he told me, he looked up some internal documentation and found out that the particular configuration that we are trying to use (Windows Server 2003 on application tier, Windows Server 2008 on data tier) may not have been properly tested. So far, the recommendation is to install reporting services on the application tier. (Later, he said, we will be able to move them to the data tier).</p>";
        public const string content_14052009_r = "<p>There were a few issues because of the Sharepoint that was not completely removed from the application tier before we started the reinstallation of the TFS, but the most interesting was the \"Error 29000.The Team Foundation databases could not be installed. For more information, see the Microsoft Windows Installer (MSI) log.\"</p><p>That was a bit tricky, because both admin and service account for the TFS had all possible permissions on the data tier. Some log file reading and some searching, and I found out that this is the issue with analysis services permissions.</p><a href=\"http://social.msdn.microsoft.com/Forums/en-US/tfssetup/thread/d8f5f035-680d-475e-9243-45457216362d\">Problem in doing TFS2008 SP1 upgrade</a><p>The account should be a member of the “Server Role is used to grant server-wide security privileges to a user” under analysis services->properties->security option should be the TFS Service account. (There is no need to add the TFS Setup account or the TFS Report account here).</p><p>Now, the question was ... what error would come up next?</p><p>Next one was the Error 28805.The setup program cannot complete the request to the server that is running SQL Server Reporting Services. Verify that SQL Server Reporting Services is installed and running on the Team Foundation app tier and that you have sufficient permissions to access it. For more information, see the setup log.</p><p>OK, that was our mistake. The reporting services were removed from the data tier and installed on the app tier, but the databases were never created. Even the SQL Server was not installed yet on the application tier.</p><p>With that fixed, we moved forward just to finally hit the wall.</p><p>\"Error 29112.Team Foundation Report Server Configuration: Either SQL Reporting Services is not properly configured, or the Reporting Services Web site could not be reached.  Use the Reporting Services Configuration tool to confirm that SQL Reporting Services is configured properly and that the Reporting Service Web site can be reached, and then run the installation again. For more information, see the Team Foundation Installation Guide.\"</p><p>And what happens here remains a mystery for me so far.</p><p>This is what I see in the installation log:</p><blockquote>\"Setting database connection...<br /><br />Verifying the configuration of SQL Server Reporting Services...<br />SQL Server Reporting Services status Name: ReportServerVirtualDirectory        Status: Set     Severity: 1     Description:  A virtual directory is specified for this instance of report server.            <br />SQL Server Reporting Services status Name: ReportManagerVirtualDirectory       Status: Set     Severity: 1     Description:  A virtual directory is specified for this instance of report manager.           <br />SQL Server Reporting Services status Name: WindowsServiceIdentityStatus        Status: Set     Severity: 1     Description:  A Windows service identity is specified for this instance of report server.     <br />SQL Server Reporting Services status Name: WebServiceIdentityStatus            Status: Set     Severity: 1     Description:  A web service identity is specified for this instance of report server.         <br />SQL Server Reporting Services status Name: DatabaseConnection                  Status: Set     Severity: 1     Description:  A report server database is specified for this report server.                   <br />SQL Server Reporting Services status Name: EmailConfiguration                  Status: NotSet  Severity: 2     Description:  E-mail delivery settings are not specified for the report server. E-mail delivery is disabled until these settings are specified.<br />SQL Server Reporting Services status Name: ReportManagerIdentityStatus         Status: Set     Severity: 1     Description:  A report manager identity must be specified.                                    <br />SQL Server Reporting Services status Name: SharePointIntegratedStatus          Status: NotSet  Severity: 2     Description:  The report server instance supports SharePoint integration, but it currently runs in native mode. If you want to integrate this report server with a SharePoint product or technology, open the Database Setup page and create or select report server database that can be used with a SharePoint Web application.<br />SQL Server Reporting Services status Name: IsInitialized                       Status: OutOfSync Severity: 3     Description:  The report server is not initialized.                                           <br /><br />Verifying SQL Server Reporting Services configuration status failed.<br /><br />Error: ErrorCheckingReportServerStatus.<br /><br />Configuring SQL Server Reporting Services failed.\"</blockquote><p>But why? I have no idea. Here is what happens:</p><p>I open Reporting Service Configuration Manager, go to \"Database Setup\" and notice that the \"Server Name\" is pointing to the data tier, and the \"Initialization\" shows a grayed cross against it. So I point it to correct server, Press \"Connect\", \"OK\", \"Apply\" and enjoy a lot of green ticks in the \"Task Status\" and a grayed cross being changed to grayed tick.</p><p>Voila! Reporting Services configured properly and initialized. I can go to http://localhost/reports and see the proper \"SQL Reporting Services Home Page\".</p><p>So I switch back to my error and press \"Retry\". The installer thinks for a while, but then the error is displayed again.</p><p>So I start the Reporting Service Configuration Manager again, go to \"Database Setup\" and notice that the \"Server Name\" is pointing to the data tier, and the \"Initialization\" shows a grayed cross against it!</p><p>Why does the installed does it to me? I have no idea so far and could not find any good information.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_14052009_k = "TFS 2008 and IIS error troubleshooting";
        public const string content_14052009_d = "Work on resolving TFS 2008 issues and IIS issues contintues ...";

        //"Small Thing Learned Today"
        public const string content_10052009_b = "<p>(No, I didn't forget my blog. It's just that not much exciting have been happening to me in regards to development)</p><p>Visual Studio 2008 does not show Solution view when there is only one project in the Solution by default. I personally find this feature very annoying. Today I was going through one example from a book, and it involved creating a solution and adding a couple different projects to it. So I created the solution, added a new project to it, did whatever was required and came to the step where I had to add a second project to the solution. And here I am, completely puzzled - not only I don't see the solution in the Solution Explorer, but there is no menu which would 'intuitively' point me to the way to add a second project.</p>";
        public const string content_10052009_r = "<p>The fix was easy to find, but still - what's the point of having 'solutions' if you're hiding them from the users by default?</p><p><a href=\"http://blogs.msdn.com/sayanghosh/archive/2008/02/09/visual-studio-2008-does-not-show-solution-view-when-there-is-only-one-project-in-the-solution-by-default.aspx\">Visual Studio 2008 does not show Solution view when there is only one project in the Solution (by default)</a></p><p>The solution is to go to Tools>Options>Projects and Solutions and check \"Always Show Solution\" (which is unchecked by default).</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_10052009_k = "Visual Studio tips tricks view solution";
        public const string content_10052009_d = "Show the solution view in Visual Studio when there is only one project";

        //"IIS Disaster Update"
        public const string content_23042009_b = "Microsoft has been able to reproduce our issue on their testing machines. I guess that places the ball into their court now.";
        public const string content_23042009_r = "Makes me feel a little bit less dumb, I was quite sure that we're missing some important security configuration setting or anything like that. Also, for the company, I guess, that means that we do not have to pay for support hours spent on the issue by Microsoft. Let's see what they will come up with ...<br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_23042009_k = "TFS 2008 and IIS error troubleshooting";
        public const string content_23042009_d = "Work on resolving TFS 2008 issues and IIS issues contintues ...";

        //"IIS Disaster Update"
        public const string content_21042009_b = "<p>I got a response from Microsoft, which is actually more of an information request. They wanted to know if I can connect to the IIS on the data tier using the 'Connect As' checkbox on the 'Connect to Computer' dialog, like this:</p>";
        public const string content_21042009_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/21042009_Connect_To_Computer.png\" alt=\"Connect To Computer\" /></div>" +
            "<p>Apparently, I can not. This did not come as a surprise. However, I decided to do an experiment and use the service account credentials in the 'Connect As' dialog box. Strangely enough, that worked. Very strange - both account are administrators on both machines, but only one of them can connect to IIS on data tier remotely. I started looking for a possible reason and noticed that the service account was a member of the IIS_WPG on the app tier, and the TFS admin account was not. Aha! So, I added the admin account to the group.</p><p>Now, the really strange thing is happening. I logon to the app tier as the TFS admin account, start IIS Manager, right-click 'Internted Information Services' and click 'Connect'. From here, I try 2 different approaches:</p><p>1. Connect without providing credentials. Which, I assume, is connecting as a current user - the TFS admin user.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/21042009_Connect_To_Computer_2.png\" alt=\"Connect To Computer 2\" /></div>" +
            "<p>and this is what I get for my efforts.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/21042009_You_Have_Been_Denied_Access_To_This_Machine.png\" alt=\"You Have Been Denied Access To This Machine\" /></div>" +
            "<p>2. Connect specifying the credentials explicitly. Which are, of course, the credentials of the TFS admin user.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/21042009_Connect_To_Computer_3.png\" alt=\"Connect To Computer 3\" /></div><p>and voila</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/21042009_IIS_Manager.png\" alt=\"IIS Manager\" /></div>" +
            "<p>Suddenly I have all the access I need. Unfortunately, that does not help much because the TFS installation still fails - I assume it tries to login to the data tier using the first approach.</p><p>Which obviously means ... which means ... ugh, I have no idea what that means. I do not have enough knowledge on the subject. Somehow the remote (data tier) IIS treats these logins differently, even though it is the same domain account that tries to login. Something should be configured in a different way somewhere. I tried to play with authentication settings on both servers, but did not succeed yet. I forwarded my new findings to Microsoft support. Stay tuned ...</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_21042009_k = "TFS 2008 and IIS error troubleshooting";
        public const string content_21042009_d = "Work on resolving TFS 2008 issues and IIS issues contintues ...";

        //"TFS Disaster Update"
        public const string content_17042009_b = "<p>I had a support session with a Microsoft technical support representative yesterday. They use a program which is called <a href=\"http://support.microsoft.com/easyassist\">Easy Assist</a> which needs to be installed on the client's computer and then you can share your desktop with the support person in the remote location and give him control etc. Nothing special, except that it works pretty fast.</p>";
        public const string content_17042009_r = "<p>Anyway, the issue was identified during the session. Basically, there are two computers, the application tier and data tier. There is a domain account that is a member of the Administrators group on both tiers. When logged on locally, this account can perform any administrative tasks. However, when the account is logged on the application tier and tries to connect to the IIS on the data tier, the access is denied and the following message is shown.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/17042009_You_Have_Been_Denied_Access_To_This_Machine.png\" alt=\"You Have Been Denied Access To This Machine\" /></div>" +
            "<br /><p>When the account tries to configure the Reporting Services on the data tier, the following message is displayed:</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/17042009_Reporting_Services_Configuration_Manager_Error.png\" alt=\"Reporting Services Configuration Manager Error\" /></div>" +
            "<br /><p>This seems to be the reason why the TFS cannot be installed - the account does not have proper permissions on the data tier to configure the Reporting Services.</p><p>So, the TFS Disaster can now be officially renamed to the IIS disaster. Microsoft promised to get their IIS team on this issue. I checked the possible solutions myself, but it appears that all suggestions are already configured properly on both computers. And so the saga unfolds ...</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_17042009_k = "TFS 2008 error troubleshooting";
        public const string content_17042009_d = "Work on resolving TFS 2008 issues contintues ...";

        //"TFS Disaster Update"
        public const string content_15042009_b = "<p>Got a responce from Microsoft today.</p><p>They suggest that the Reporting Services should be installed on the application tier, quoting the Installation Guide for the TFS:</p>";
        public const string content_15042009_r = "<blockquote><p><strong>Application Tier</strong></p><p>The Team Foundation application tier is composed of Web-based, front-end applications that are integrated with Internet Information Services (IIS). These applications include SQL Server Reporting Services, Team Foundation Core Services, and SharePoint Products and Technologies. In addition, the application tier hosts Team Foundation Windows services.</p></blockquote><p>To which I reply, quoting the Installation Guide for the TFS:</p><blockquote><p><strong>How to Deploy Team Foundation Server with SQL Server Reporting Services on a Remote Server</strong></p><p>You can deploy SQL Server Reporting Services on a remote server, which is a server other than the application-tier server for Team Foundation. In this scenario, you can deploy Team Foundation Server and run SQL Server Reporting Services on any of the following types of servers: <p><p>The data-tier server for Team Foundation. <br />The same server on which SharePoint Products and Technologies is running. <br />A remote server anywhere on the network.<p></blockquote><p>Stay tuned as the epic saga unfolds!</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_15042009_k = "TFS 2008 error troubleshooting";
        public const string content_15042009_d = "Work on resolving TFS 2008 issues contintues ...";

        //"Copy Constructor Update"
        public const string content_14042009_b = "<p>Stumbled upon a problem today and understood that I was not implementing the copy constructor properly.<br />Actually, in my post \"Copy Constructor\" I wrote:</p>";
        public const string content_14042009_r = "<p>For the List type, for example, the following approach would work:</p><pre class=\"brush:csharp\">" +
            @"class Customer  <br />{  <br />    private List<STRING> names;  <br />  <br />    // Copy constructor.  <br />    public Customer(Customer previousCustomer)  <br />    {  <br />        names = new List<STRING>(previousCustomer.names.ToArray());  <br />    }  <br />  <br />    ...  <br />}" +
            "</pre><p>This is not true for the list of reference type objects.</p><p>The workaround I use now is to implement a copy constructor in the reference type and create a copy of the list in the following manner:</p><pre class=\"brush:csharp\">" +
            @"class Customer  <br />{  <br />    private List<ID> customerIDs;  <br />  <br />    // Copy constructor.  <br />    public Customer(Customer previousCustomer)  <br />    {  <br />       customerIDs = new List<ID>();<br />       foreach(ID id in previousCustomer.customerIDs)<br />       {<br />         customerIDs.Add(new ID(id));<br />       }<br />    }  <br />  <br />    ...  <br />}" +
            "</pre><p>The syntax could be a lot more elegant in 3.5, but this application uses the 2.0 framework.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_14042009_k = "C# programming copy constructor";
        public const string content_14042009_d = "More encounters with C# Copy Constructor";

        //"TFS Disaster Update"
        public const string content_10042009_b = "<p>The decision was made to log an issue with Microsoft with a low priority as the TFS is not in our production environment yet. The first response we received just in about 2 hours after logging the issue.<br />They were interested in the following information:</p>";
        public const string content_10042009_r = "<p>1.     The installation log file;</p><p>2.     Accounts used for the installation: the setup /TFS service/reports /SQL Service account. Please describe briefly about them and make sure they meet the requirements listed in the installation guide;</p><p>3.     Did you follow the “prerequisites for Team Foundation Server” section to prepare for the installation? The \"install SQL Server Reporting Service\" section documents how to prepare reporting service. Per this doc one should not configure reporting service before TFS installation. Do you confirm this is consistent with your installation?</p><br /><p>After that they promised to get back to us ASAP and still did not, a full business day after I provided all the required information. It's the Thursday before the Easter Friday though, so I did not hold my breath.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_10042009_k = "TFS 2008 error troubleshooting";
        public const string content_10042009_d = "Work on resolving TFS 2008 issues contintues ...";

        //"TFS Disaster"
        public const string content_08042009_b = "<p>Today I was performing a 'pre-production' install of the TFS, which is different from the install I've done before. The first install had application and data tiers on the same computer, this one was supposed to have two tiers on different computers. Should be nice and easy since I have some TFS installation experience and a competent database admin to work together with.</p>";
        public const string content_08042009_r = "<p>And the first issue that I came across was that .Net Framework 2.0 is required by the TFS, but it was not installed. But how could I not list it under the prerequisites when I was writing the installation procedure document? Well, the first installation had both the SQL Server and TFS on the same computer. And the .Net Framework is a prerequisite for the SQL Server, so by the time TFS had to be installed it was present.</p><p>That was the easy part.</p><p>To be able to place Reporting Services on the same machine as the data tier, we used the advice from this article</p><p><a href=\"http://blogs.msdn.com/sudhir/archive/2007/09/10/reporting-services-flexibility-orcas-rtm-only.aspx\">Reporting Services Flexibility (Orcas RTM Only)</a></p><p>and edited the msiproperty.ini according to that advice.</p><p>Next problem we came across happened during the system health check. The following message was generated:</p><p>\"The System Health Check has detected a problem that may cause Setup to fail. <br /><br />Description<br />SQL Server Analysis Services is not installed.\"</p><p>We used the workaround and happily proceeded further. That was the easy part too.</p><p>\"Workaround / Remedy<br />SQL Server Analysis Services is a prerequisite for Visual Studio Team Foundation Server 2008. Install a supported version of SQL Server Analysis Services. For more information about supported versions of SQL Server and Team Foundation Server prerequisites, download the most recent version of the Team Foundation Installation Guide, which is available from the Microsoft Web site. <br /><br />More information<br />For additional information and help please refer to: http://go.microsoft.com/fwlink/?LinkId=79226\"</p><p>The next error came up during the actual Team Foundation Server installation process.</p><p><br />---------------------------<br />Microsoft Visual Studio 2008 Team Foundation Server Setup<br />---------------------------<br />Error 29109.Team Foundation Report Server Configuration: SQL Reporting Services configuration encountered an unknown error. Verify that you have sufficient permissions to configure SQL Reporting Services, and try again.<br />---------------------------<br />Retry   Cancel   <br />---------------------------</p><p>And the entry in the installation log was the following</p><p><br />TFRSConfig - Team Foundation Server Reporting Services Configuration Tool<br />Copyright (c) Microsoft Corporation.  All rights reserved.<br /><br />Connecting to SQL Server Reporting Services. Please wait...<br />Invalid namespace <br />Querying the following Windows Management Instrumentation (WMI) path: IIS://DATASERVERNAME/W3SVC.<br />System.Runtime.InteropServices.COMException (0x80070005): Access is denied.<br /><br />   at System.DirectoryServices.DirectoryEntry.Bind(Boolean throwIfFail)<br />   at System.DirectoryServices.DirectoryEntry.Bind()<br />   at System.DirectoryServices.DirectoryEntry.get_IsContainer()<br />   at System.DirectoryServices.DirectoryEntries.ChildEnumerator..ctor(DirectoryEntry container)<br />   at System.DirectoryServices.DirectoryEntries.GetEnumerator()<br />   at Microsoft.TeamFoundation.Admin.ReportingServices.WebSiteFinder.FindBestMatch(Uri searchUri)<br />   at Microsoft.TeamFoundation.Admin.ReportingServices.InputArgs.EnsureIISSettings()<br />   at Microsoft.TeamFoundation.Admin.ReportingServices.ReportingServicesConfigurator.Run()<br />   at Microsoft.TeamFoundation.Admin.ReportingServices.Program.Main(String args)<br /><br />Configuring SQL Server Reporting Services failed.<br /><br />04/07/09 16:56:06 DDSet_Status: Process returned 2519<br />04/07/09 16:56:06 DDSet_Status: Found the matching error code  for return value '2519' and it is: '29109'<br />04/07/09 16:56:06 DDSet_Error:  2519<br />MSI (s) (A4!B8) [08:44:23:998]: Product: Microsoft Visual Studio 2008 Team Foundation Server - ENU -- Error 29109.Team Foundation Report Server Configuration: SQL Reporting Services configuration encountered an unknown error. Verify that you have sufficient permissions to configure SQL Reporting Services, and try again.<br /><br />04/08/09 08:44:23 DDSet_Status: Commandline: \"d:\\Program Files\\Microsoft Visual Studio 2008 Team Foundation Server\\Tools\\TFRSConfig.exe\" /setup /install /s \"DATASERVERNAME\" /u \"NT Authority\\NetworkService\" /buildInIdentity /l \"1033\" /verify /ignoreExistingIISArtifacts /instancename \"MSSQLSERVER\" /appPoolName \"Classic .NET AppPool\" /reportServerUri \"http://DATASERVERNAME/ReportServer\" /reportManagerUri \"http://DATASERVERNAME/Reports\" /h \"DATASERVERNAME\"</p><p>And this is where we got stuck. We tried pretty much every solution we could find. Every possible account was given every possible permission on the data tier machine, the reporting services were perfectly accessible from the application tier machine, I could log on to the data tier machine from every account used in the TFS installation and configure Reporting Services if I wished to, but the error still did not go away.</p><p>I have to say that we are now stuck at this point.<br />There's couple options I can see - first, completely rebuild the application tier machine. Make sure everything is configured before we even start the installation, everything has access and permissions. Then try installing again. Second option could be to try and install the reporting services on the application tier. This would probably slow it down a bit. And another option is to try and make Microsoft fix the problem for us. We'll make a decision tomorrow.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_08042009_k = "TFS 2008 error troubleshooting";
        public const string content_08042009_d = "First encounters with TFS 2008";

        //"Full process of scanning the document and extracting data"
        public const string content_07042009_b = "<p>Now the full process of scanning the document and extracting data from it is mostly finished. It does not look too complicated now, unlike it was just a few weeks earlier.</p><p>First, I create the instances of a class that will keep the extracted data (AIT3MDocument) and the class that will communicate with the scanner (AIT3MDocumentScanner) through a ReaderClass object. I explained it in some more detail in one of the earlier blog entries.</p>";
        public const string content_07042009_r = "<pre class=\"brush:csharp\">" +
            @"AIT3MDocument document = new AIT3MDocument();<br />AIT3MDocumentScanner docScanner = new AIT3MDocumentScanner();<br />document = docScanner.ScanDocument(_Filepath, useOCRCheckBox.Checked);<br /><br />documentReader = new ReaderClass();<br />documentItem = new DocumentItemClass();" +
            "</pre><p>Next, I will extract some data that lets me identify what kind of document is in the scanner.</p><pre class=\"brush:csharp\">" +
            @"string documentID = documentReader.RetrieveTextItem(DOCUMENT_ID);<br />string documentSpecific = documentReader.RetrieveTextItem(DOCUMENT_SPECIFIC);<br />string mrzDocumentType = documentReader.RetrieveTextItem(MRZ_DOCUMENT_TYPE);" + "</pre><p><br />" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/14042009_Document_ID.png\" alt=\"Document ID\" /></div>" +
            "</p><p>I can extract data such as the english spelling of the person's name through the ReaderClass object.</p><pre class=\"brush:csharp\">" +
            @"string name = documentReader.RetrieveTextItem(NAME);" + "</pre><p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/14042009_Document_Name.png\" alt=\"Document Name\" /></div>" +
            "</p><p>If, for example, the document was identified as a Hong Kong ID, I know that it has a special sequence of digits on it which can be extracted too.</p><pre class=\"brush:csharp\">" +
            @"string nameAsNum = documentReader.RetrieveTextItem(MRZ_NAME_AS_NUMBER_1);" + "</pre><p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/14042009_Retrieve_Text_Item.png\" alt=\"Retrieve Text Item\" /></div>" +
            "</p><p>This sequence of digits encodes the chinese spelling of the person's name. To be able to extract that spelling was the only reason I spent all that time and effort writing my glorious wrapper for the unmanaged dll.</p><p>So, after I found out the type of the document and the sequence of digits that encodes the name, I can call the proper function:</p><pre class=\"brush:csharp\">" +
            @"if (!String.IsNullOrEmpty(nameAsNum))<br />{<br /> TSSLWrapper.RECO_DATA recoData = new TSSLWrapper.RECO_DATA();<br /> IntPtr ptr = TSSLWrapper.SDKCreate();<br /> bool res = TSSLWrapper.CcnOCRsdk_HKID(ptr, nameAsNum, out recoData);<br /><br /> if (res)<br /> {<br />  document.FirstNameExtended = recoData.FirstName;<br />  document.LastNameExtended = recoData.Surname;<br /> }<br /> TSSLWrapper.SDKDelete(ptr);<br />}" + "</pre><p>and observe the result.</p><p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/14042009_Extended_Names.png\" alt=\"Extended Names\" /></div>" +
            "</p><p>Now I just populate the names into the proper fields and my job is done.</p><p>This is the simplest example. Some other types of documents I work with do not contain a sequence of digits, but rather operate with the image directly. In this case a path to the saved image has to be provided, but the output is more or less similar anyway, so there is not much difference from the development point of view.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_07042009_k = "C# development 3M document scanner AIT3MDocumentScanner";
        public const string content_07042009_d = "Using 3M Document Reader programmatically to scan the document and extract data";

        //"Calling an unmanaged C++ dll from C# managed code"
        public const string content_01042009_b = "<p>I finished the task of calling the unmanaged dll functions from the C# application today. After I found out that my solution described before does not work, I spent almost two days on the task. Looking at what I've done, I can see that the whole solution takes maybe 2-3 dozen lines of code. And while I understand that lines of code is not the best way to measure productivity, I could have done it in half a day if I always chose the right way to do things. Why did it take me that long? Well, there are some reasons.</p>";
        public const string content_01042009_r = "<ul><li>I have not touched C++ in the last 8 years, so every little complaint from the linker or compiler was a challenge.</li><li>I could not find a complete solution for my case, so I had to compile a few together: learn how to write a dll in C++, learn how to write a wrapper class, learn how to call the wrapper class from the managed code etc.</li><li>There are numerous solutions on the internet, but choosing the right one is the problem. Some of the approaches I tried led me nowhere but took some time.</li></ul><p>Now, when I'm done with this bucket of excuses, here is how I solved my particular case:</p><p>First of all, I had to understand that the straightforward DllImport solution does not work. I came across a comment that suggested that when a class member has to be called from a dll, a managed wrapper dll has to be written to allow access to the class members. From the C++ sample code I had I could see that yes, a class member is called.</p><pre class=\"brush:csharp\">" +
            @"CcnOCRsdk *ocr;<br />CString code;<br />RECO_DATA data;<br />GetDlgItemText(IDC_CODE,code);<br />char _code[200];<br />WideCharToMultiByte(CP_UTF8, 0, code, -1, (char *)_code, 200, NULL, NULL);<br />ocr->convertHKID_Name(_code,&data)" +
            "</pre><p>So, I looked at this example of creating a C++ dll</p><p><a href=\"http://msdn.microsoft.com/en-us/library/ms235636(VS.80).aspx\">Walkthrough: Creating and Using a Dynamic Link Library</a></p><p>and at this example of writing a wrapper class</p><p><a href=\"http://www.codeproject.com/KB/mcpp/unmanaged_to_managed.aspx\">Calling Managed Code from Unmanaged Code and vice-versa</a></p><p>and made my first attempt at writing a wrapper. The wrapper in my initial solution exported the member function of a wrapped class. It worked to the point where the function was being called, and then threw the 'AccessViolationException'. Here I got stuck again.</p><p>Next thing to understand was that I have to export the whole class, including the constructor. To get access to the member of the class, I would have to return a pointer to the instance of the unmanaged class, and then pass this pointer to the function, that exports the member of the unmanaged class (I hope I'm describing it properly, but I'm not completely sure). The answers to this question pointed me to the right direction.</p><p><a href=\"http://stackoverflow.com/questions/315051/using-a-class-defined-in-a-c-dll-in-c-code\">using a class defined in a c++ dll in c# code</a></p><p>After that, it was really simple, because I only had to apply my DllImport skills. So here we go:</p><p><u>First step, create a C++ DLL project in Visual Studio 2005.</u></p><ul><li>Start Visual Studio</li><li>From the File menu, select New and then Project….</li><li>From the Project types pane, under Visual C++, select Win32.</li><li>From the Templates pane, select Win32 Console Application.</li><li>Choose a name for the project and enter it in the Name field. Choose a name for the solution, and enter it in the Solution Name field.</li><li>Press OK to start the Win32 application wizard. From the Overview page of the Win32 Application Wizard dialog, press Next.</li><li>From the Application Settings page of the Win32 Application Wizard, under Application type, select DLL if it is available or Console application if DLL is not available.</li><li>From the Application Settings page of the Win32 Application Wizard, under Additional options, select Empty project.</li><li>Press Finish to create the project.</li></ul><p>There were some setting I had to change for my project in Project->Properties:</p><ul><li>Under General->Project Defaults, set Use of MFC to 'Use MFC in a Shared DLL' as the unmanaged code was using MFC</li><li>Under General->Project Defaults set Common Language Runtime support to 'Common Language Runtime Supportj(/clr)'</li><li>Under Linker->Input->Additional Dependencies enter the name of the lib file for the unmanaged dll.</li></ul><p><u>Second step, write a wrapper class for the unmanaged dll.</u></p><p>This is how the unmanaged class looks like:</p><pre class=\"brush:csharp\">" +
            @"class CNOCRSDK_API CcnOCRsdk {<br />public:<br /> CcnOCRsdk(void);<br /> bool convertHKID_Name(char *code,RECO_DATA *o_data); //hkid <br /> //more member functions<br /> ~CcnOCRsdk();<br />private:<br /> RECT *regionList;<br /> RECT *chRect,*enRect;<br /> //more member functions<br />};" +
            "</pre><p>This is the wrapper class I wrote, that exports the class constructor and one of the member functions:</p><pre class=\"brush:csharp\">" +
            @"include ""stdafx.h""<br />#using <mscorlib.dll><br />#include ""cnOCRsdk.h""<br /><br />using namespace System::Runtime::InteropServices;<br />using namespace System;<br /><br />namespace TSSL<br />{<br /> public class __declspec(dllexport) Wrapper<br /> {<br /> public:<br />  CcnOCRsdk* SDKCreate()<br />  {<br />   return new CcnOCRsdk();<br />  }<br /><br />  bool CcnOCRsdk_HKID(CcnOCRsdk* pSDK, char *code, RECO_DATA *o_data)<br />  {<br />   return pSDK->convertHKID_Name(code, o_data);<br />  }<br /><br />  void SDKDelete(CcnOCRsdk* pSDK)<br />  {<br />   delete pSDK; <br />  }<br /> };<br />}</pre><p>The __declspec(dllexport) at the class level exports all public class member in the dll. If it was applied on the member level, it would only export the member it was applied to.</p><p><u>Third step, run the Dumpbin utility and find out what are the 'mangled' names of the functions exported by the dll.</u></p><pre>          1    0 00001240 ??4Wrapper@TSSL@@QAEAAV01@ABV01@@Z = __t2m@??4Wrapper@TSSL@@QAEAAV01@ABV01@@Z ([T2M] public: class TSSL::Wrapper & __thiscall TSSL::Wrapper::operator=(class TSSL::Wrapper const &))<br />          2    1 00001220 ?CcnOCRsdk_HKID@Wrapper@TSSL@@QAE_NPAVCcnOCRsdk@@PADPAURECO_DATA@@@Z = __t2m@?CcnOCRsdk_HKID@Wrapper@TSSL@@QAE_NPAVCcnOCRsdk@@PADPAURECO_DATA@@@Z ([T2M] public: bool __thiscall TSSL::Wrapper::CcnOCRsdk_HKID(class CcnOCRsdk *,char *,struct RECO_DATA *))<br />          3    2 00001200 ?SDKCreate@Wrapper@TSSL@@QAEPAVCcnOCRsdk@@XZ = ?SDKCreate@Wrapper@TSSL@@QAEPAVCcnOCRsdk@@XZ (public: class CcnOCRsdk * __thiscall TSSL::Wrapper::SDKCreate(void))<br />          4    3 00001410 ?SDKDelete@Wrapper@TSSL@@QAEXPAVCcnOCRsdk@@@Z = ?SDKDelete@Wrapper@TSSL@@QAEXPAVCcnOCRsdk@@@Z (public: void __thiscall TSSL::Wrapper::SDKDelete(class CcnOCRsdk *))" + "</pre><p><u>Fourth step, write another wrapper, now in C#, which will define the DllImports for the unmanaged functions and the structures required to call them</u></p><pre class=\"brush:csharp\">" +
            @"public class TSSLWrapper<br />    {<br />        [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]<br />        public struct RECO_DATA<br />        {<br />            /// wchar_t[200]    <br />            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 200)]<br />            public string FirstName;<br />            /// wchar_t[200]    <br />            [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 200)]<br />            public string Surname;<br />        }<br /><br />        [DllImport(@""TSSL.dll"", EntryPoint = ""?SDKCreate@Wrapper@TSSL@@QAEPAVCcnOCRsdk@@XZ"")]<br />        public static extern IntPtr SDKCreate();<br />        [DllImport(@""TSSL.dll"", EntryPoint = ""?CcnOCRsdk_HKID@Wrapper@TSSL@@QAE_NPAVCcnOCRsdk@@PADPAURECO_DATA@@@Z"")]<br />        public static extern bool CcnOCRsdk_HKID(IntPtr ptr, string num, out RECO_DATA o_data);<br />    }" +
            "</pre><p>RECO_DATA is the structure I have to send from C# to C++. Note how the SDKCreate returns the IntPtr which is, in my understanding, a pointer to the instance of the unmanaged class. To call a member function of this class, I pass this pointer to the function.</p><p><u>And fifth and last step, is to call the C# wrapper class from the C# application and enjoy the results.</u></p><pre class=\"brush:csharp\">" +
            @"TSSLWrapper.RECO_DATA recoData = new TSSLWrapper.RECO_DATA();<br />string num = ""262125355174"";<br />IntPtr ptr = TSSLWrapper.SDKCreate();<br />bool res = TSSLWrapper.CcnOCRsdk_HKID(ptr, num, out recoData);" +
            "</pre><p>(To make the solution complete, I should also wrap the destructor and call it after I don't need the class any more, I'll do it soon)</p><p>Here we go, my longest post ever has arrived.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_01042009_k = "C++ C# dll development managed unmanaged";
        public const string content_01042009_d = "Calling an unmanaged C++ dll from C# managed code";

        //"Copy Constructor"
        public const string content_22032009_b = "<p>It's strange, but I did not have to deal with copy constructors in C# until today. Here is the problem: in the application I'm working on, a user can retrieve customer's details from the database, change some values on several separate tabs, and then choose to save or discard changes. Now, discarding did not work properly till today. The problem is that the old values were not saved anywhere, so no way to access them was implemented.</p>";
        public const string content_22032009_r = "<p>Two approaches came to my mind: First, to reload data from the database, and second, to keep a 'backup' copy of the customer. Since an extra trip to the database takes several seconds, the 'backup' approach was chosen.</p><p>Now, when the customer details are retrieved from the database, they are immediately copied into the 'backup' customer object. Obviously, if you try to create a copy like this</p><pre class=\"brush:csharp\">" +
            @"Customer cust = SelectCustomerFromDB(id);<br />Customer cust2 = cust;<br />" +
            "</pre><p>You will end up with two references to the same object. Now, if you change anything in 'cust' object, your 'cust2' is useless, cause it references the same object and will reflect the changes. This MSDN page gives a brief idea of the proper way to create a copy of the object</p><p><a href=\"http://msdn.microsoft.com/en-us/library/ms173116(VS.80).aspx\">How to: Write a Copy Constructor (C# Programming Guide)</a></p><p>The above sample would now look more like this</p><pre class=\"brush:csharp\">" +
            @"class Customer<br />{<br />    private string name;<br /><br />    // Copy constructor.<br />    public Customer(Customer previousCustomer)<br />    {<br />        name = previousCustomer.name;<br />    }<br /><br />    ...<br />}<br /><br />...<br /><br />Customer cust = SelectCustomerFromDB(id);<br />Customer cust2 = new Customer(cust);" +
            "</pre><p>What if your customer has a list of names though?</p><pre class=\"brush:csharp\">" +
            @"class Customer<br />{<br />    private List<string> names;<br /><br />    // Copy constructor.<br />    public Customer(Customer previousCustomer)<br />    {<br />        names = previousCustomer.names;<br />    }<br /><br />    ...<br />}<br /><br />...<br /><br />Customer cust = SelectCustomerFromDB(id);<br />Customer cust2 = new Customer(cust);" +
            "</pre><p>Now try changing something in cust.names. Ooops, the cust2.names reflected the changes. Unfortunately, the article linked above only shows the approach to copy the object with members of value types. Now, if you have members of reference types in your class, you would have to copy each one properly.</p><p>For the List type, for example, the following approach would work:</p><pre class=\"brush:csharp\">" +
            @"class Customer<br />{<br />    private List<string> names;<br /><br />    // Copy constructor.<br />    public Customer(Customer previousCustomer)<br />    {<br />        names = new List<string>(previousCustomer.names.ToArray());<br />    }<br /><br />    ...<br />}" +
            "</pre><p>I came across a few smart ways to implement the copy constructor so that you would not have to specify each class field separately, like in this exapmle</p><p><a href=\"http://www.johnsadventures.com/archives/2006/07/an_intelligent_copy_constructor_in_c_usi/\">An Intelligent Copy Constructor In C# Using Reflection</a></p><pre class=\"brush:csharp\">" +
            @"public MyClass( MyClass rhs )<br />{<br /> // get all the fields in the class<br /> FieldInfo[] fields_of_class = this.GetType().GetFields( <br />   BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance );<br /><br /> // copy each value over to 'this'<br /> foreach( FieldInfo fi in fields_of_class )<br /> {<br />  fi.SetValue( this, fi.GetValue( rhs ) );<br /> }<br />}" +
            "</pre><p>This is great, because if you add a new field to the class and forget to add it to a copy constructor, your code will compile without warnings but you will get troubles sooner or later.</p><p>Unfortunately, this approach also works for value type fields only so I could not benefit from it. I could not quickly find a way to use this approach to reference type class members so I'll leave it for myself as a TODO task.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_22032009_k = "C# Programming Copy Constructor";
        public const string content_22032009_d = "Using C# Copy Constructor";

        //"DllImport Adventures"
        public const string content_19032009_b = "<p>More fun with the scanner. Now we want to get the first name and last name in chinese from the certain types of passports. There is a dll written in C++ which supposedly provides this functionality. The function in this dll takes a path to an image as a parameter and returns a structure that contains first name and last name.</p>";
        public const string content_19032009_r = "<p>Here is the C++ signature of the function</p><pre class=\"brush:csharp\">" +
            @"<br />bool recoCHN_P_Name(char *imgPath,RECO_DATA *o_data);" + "</pre><p>Here is the RECO_DATA struct</p><pre class=\"brush:csharp\">" +
            @"struct RECO_DATA{<br /> wchar_t FirstName[200];<br /> wchar_t Surname[200];<br />};" +
            "</pre><p>Now I have to call the C++ method from my C# code. The 'DllImport' thingy comes to mind. There are heaps of tutorials on that, for example</p><p><a href=\"http://www.csharphelp.com/archives/archive52.html\">Call Unmanaged Code. Part 1 - Simple DLLImport</a></p><p>So I quickly come up with the first version:</p><p>This is the 'wrapper' class</p><pre class=\"brush:csharp\">" +
            @"public class cnOCRsdk<br />{<br /> [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]<br /> public struct RECO_DATA{<br />  [MarshalAs(UnmanagedType.ByValTStr, SizeConst=200)]<br />  public string FirstName;<br />  [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 200)]<br />  public string Surname;<br />  }<br /><br /> [DllImport(@""cnOCRsdk.dll"", EntryPoint=""recoCHN_P_Name"")]<br /> public static extern bool recoCHN_P_Name(byte[] imgPath, RECO_DATA o_data);<br />}" +
            "</pre> <p>This is the call to the function in the wrapper class</p><pre class=\"brush:csharp\">" +
            @"cnOCRsdk.RECO_DATA recoData = new cnOCRsdk.RECO_DATA();<br /><br />string path = @""C:\WINDOWS\twain_32\twainrgb.bmp"";<br /><br />System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();<br />byte[] bytes = encoding.GetBytes(path);<br /><br />bool res = cnOCRsdk.recoCHN_P_Name(bytes, recoData);" +
            "</pre><p>Unfortunately, this gives me a \"Unable to find an entry point named 'recoCHN_P_Name' in DLL 'cnOCRsdk.dll'.\" error. My first guess was that I am converting types from C++ to C# incorrectly. I asked for some help</p><p><a href=\"http://stackoverflow.com/questions/653178/unable-to-find-an-entry-point-named-function-in-dll-c-to-c-type-conversion\">Unable to find an entry point named [function] in dll</a></p><p>I got a suggestion to use the 'dumpbin' tool</p><p><a href=\"http://support.microsoft.com/kb/177429\">DUMPBIN</a></p><p>and also a link to the page that explained C++ name mangling</p><p><a href=\"http://www.kegel.com/mangle.html\">C++ Name Mangling/Demangling</a></p><p>my function had a mangled name of '?recoCHN_P_Name@CcnOCRsdk@@QAE_NPADPAURECO_DATA@@@Z', but how did that help me?</p><p>Anyway, I came across another article on calling C++ from C#</p><p><a href=\"http://blogs.msdn.com/vcblog/archive/2008/12/08/inheriting-from-a-native-c-class-in-c.aspx\">Inheriting From a Native C++ Class in C#</a></p><p>and noticed an interesting thing in it:</p><pre class=\"brush:csharp\">" +
            @"[DllImport(""cppexp.dll"", EntryPoint = ""?M1@CSimpleClass@@QAEXXZ"", CallingConvention = CallingConvention.ThisCall)]<br />    private static extern void _M1(__CSimpleClass* ths);" +
            "</pre><p>this guy is using the mangled function name as an entry point! Why shouldn't I try this approach?</p><p>Long story short, the 'unable to find entry point' error went away and after a few more tweaks I made this work. Unfortunately, the function always returns me false so far, which is probably because all the images I try are 'bad', but at least it does not break anymore.</p><pre class=\"brush:csharp\">" +
            @"public class cnOCRsdk<br />{<br /> [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]<br /> public struct RECO_DATA<br /> {<br />  [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 200)]<br />  public string FirstName;<br />  [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 200)]<br />  public string Surname;<br /> }<br /><br /> [DllImport(@""cnOCRsdk.dll"", EntryPoint=""?recoCHN_P_Name@CcnOCRsdk@@QAE_NPADPAURECO_DATA@@@Z"")]<br /> public static extern bool recoCHN_P_Name(ref string imgPath, ref RECO_DATA o_data);<br />}" +
            "</pre><pre class=\"brush:csharp\">" +
            @"cnOCRsdk.RECO_DATA recoData = new cnOCRsdk.RECO_DATA();<br />recoData.FirstName = new string(new char[200]);<br />recoData.Surname = new string(new char[200]);<br /><br />string path = @""C:\WINDOWS\twain_32\twainrgb.bmp"";<br /><br />bool res = cnOCRsdk.recoCHN_P_Name(ref path, ref recoData);" + "</pre><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_19032009_k = "C# Development DllImport";
        public const string content_19032009_d = "Calling a C++ dll from C# code using platform invoke";

        //"Image file handle adventures"
        public const string content_15032009_b = "<p>Today I had an issue with not being able to delete a file programmatically.<br />While looking for the reason the file was 'locked' (the actual error message was \"The process cannot access the file 'file.jpg' because it is being used by another process\") I discovered a tool which can help finding out the process which is locking the file.</p>";
        public const string content_15032009_r = "<p><a href=\"http://technet.microsoft.com/en-us/sysinternals/bb896653.aspx\">Process Explorer</a></p><p>Anyway, here is how the image was processed by the application:</p><pre class=\"brush:csharp\">" +
            @"Bitmap b = (Bitmap)Image.FromFile(_Filepath);<br />pictureBox1.Image = b;<br /><br />// later in the code<br /><br />System.IO.MemoryStream ms = new System.IO.MemoryStream();<br />pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);<br /><br />// even later in the code<br /><br />File.Delete(_Filepath);" +
            "</pre><p>At the point where File.Delete() was called, the handle to the file existed (under some circumstances).</p><p>Here is how I implemented the fix initially:</p><pre class=\"brush:csharp\">" +
            @"public Bitmap getBitmapFromFile(string filename)<br />{<br /> Image i = null;<br /> using (Stream s = new FileStream(filename, FileMode.Open))<br /> {<br />  i = Image.FromStream(s);<br />  s.Close();<br /> }<br /> return (Bitmap)i;<br />}<br /><br />pictureBox1.Image = getBitmapFromFile(_Filepath);<br /><br />// later in the code<br /><br />System.IO.MemoryStream ms = new System.IO.MemoryStream();<br />pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);<br /><br />// even later in the code<br /><br />File.Delete(_Filepath);" +
            "</pre><p>The approach did not work, however, this time returning me the error \"A generic error occurred in GDI+\" at the line where I tried to save the image.</p><p>Here is the reason why this happened:</p><p><a href=\"http://support.microsoft.com/?id=814675\">Bitmap and Image constructor dependencies</a></p><p>In my case, obviously, \"Additionally, if the stream was destroyed during the life of the Bitmap object, you cannot successfully access an image that was based on a stream\" - see how I tried to close a stream, trying to release a handle to the file this way?</p><p>The possible solutions can be found here:</p><p><a href=\"http://www.kerrywong.com/2007/11/15/understanding-a-generic-error-occurred-in-gdi-error/\">Understanding \"A generic error occurred in GDI+.\" Error</a></p><p>That's how I fixed my problem eventually:</p><pre class=\"brush:csharp\">" +
            @"public Bitmap getBitmapFromFile(string filePath)<br />{<br /> Image img = Image.FromFile(filePath);<br /> Bitmap bmp = img as Bitmap;<br /> Graphics g = Graphics.FromImage(bmp);<br /> Bitmap bmpNew = new Bitmap(bmp);<br /> g.DrawImage(bmpNew, new Point(0, 0));<br /> g.Dispose();<br /> bmp.Dispose();<br /> img.Dispose();<br /> return bmpNew;<br />}<br /><br />pictureBox1.Image = Syco.Common.Util.getBitmapFromFile(_Filepath);<br /><br />// later in the code<br /><br />System.IO.MemoryStream ms = new System.IO.MemoryStream();<br />pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);<br /><br />// even later in the code<br /><br />File.Delete(_Filepath);" +
            "</pre><p>Now the image is saved properly and no handle is held against the file, so it is deleted properly too.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_15032009_k = "C# programming image file handle";
        public const string content_15032009_d = "Deleting an image file programmatically";

        //"Moving Ahead of Technology"
        public const string content_14032009_b = "<p>Remember that Windows Service I wrote not long ago? Well, today it was time to install it into actual testing environment. That did not work exactly as expected. The first thing I get when trying to run the setup project was this error message</p>";
        public const string content_14032009_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/14032009_Some_Dll_Failed_To_Register.png\" alt=\"Some Dll Failed To Register\" /></div>" +
            "<br /><p>Did not take long to find out that the service was developed in VS.NET 2008 and required .NET Framework 3.5 to run. Now, what do you think would take longer in a large company, to rebuild the application in the previous version of VS.NET or obtaining the permission to install .NET Framework on a server? That's what I did ...</p><p>Therefore, moving fast and using the 'latest tools' might actually get you into some trouble...</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_14032009_k = "C# programming .NET 3.5";
        public const string content_14032009_d = "Moving Ahead of Technology";

        //"A Twisted Code Snippet"
        public const string content_06032009_b = "<p>The application is able to save images for an ID. When the changes are being saved, the comma-separated list of file names has to be inserted into the XML file. Easy. This small snippet have been sitting in the production code for about two years, until someone had to attach more than 2 images to a single ID.</p>";
        public const string content_06032009_r = "<p>(Easy to see that the logic works for one or two files, forming a string of '1.jpg,2.jpg'. If more files are to be saved, the string will look like 1.jpg,2.jpg3.jpg4.jpg' and all images except the first one will be lost).</p><pre class=\"brush:csharp\">" +
            @"id += ""<Filenames>"";<br /><br />bool firstImage = true;<br />foreach (IDImage image in idType.IDImages)<br />{<br /> id += image.ImageFilename;<br /> if (firstImage)<br /> {<br />  firstImage = false;<br />  id += "","";<br /> }<br />}<br />id += ""</Filenames>"";" +
            "</pre><p>I like bugs that are easy to fix and it looks like magic to a unsuspecting observer that a bug is fixed in a with a few keystrokes. How did the original developer come up with the idea, though, and why did it pass testing ...</p><pre class=\"brush:csharp\">" + @"id += ""<Filenames>"";<br /><br />foreach (IDImage image in idType.IDImages)<br />{<br /> id += image.ImageFilename;<br /><br /> //if not last image, add ','<br /> if (idType.IDImages.IndexOf(image) < idType.IDImages.Count - 1)<br /> {<br />  id += "","";<br /> }<br />}<br />id += ""</Filenames>"";" +
            "</pre><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_06032009_k = "Error fix";
        public const string content_06032009_d = "Fixing errors";

        //"C++ Runtime Libraries Adventures"
        public const string content_02032009_b = "<p>Almost no day passes without one of those \"WHAT is happening?\" moments. Yesterday there were 3 that happened to me. One required to delete temporary Internet Explorer files. Of course, I was stupid enough to require a colleague's advice on that. Another one, ironically, was caused by that same colleague adding a line of code in the wrong place. This one I fixed myself.</p>";
        public const string content_02032009_r = "<p>Now the third one was a bit more interesting and looks like a classic \"worked on my machine!\" situatuion.<br />Remember the application that uses the sophisticated scanner? Well, now it also uses a webcam. I was preparing the new version to ship. I build the installation package and installed the application on the test computer. When I try to use the webcam, I get our 'generic' error message, that can be caused by almost anything. But it worked on my machine, I swear!</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/02032009_ApplicationFailed.PNG\" alt=\"Application Failed To Start\" /></div><p>Anyway, there is a way to find out what really happened - Event Log.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/02032009_ErrorMessage.PNG\" alt=\"Error Message\" /></div><p>So, something is wrong with the webcam library QuickCamLib.dll. First thing that comes to mind - somehow not registered during installation process? Possible or not? Go go, regsvr32</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/02032009_EventLog1.PNG\" alt=\"Event Log Contents\" /></div>" +
            "<p>Oh well, this is not my application problem. But what is wrong? The webcam drivers were installed and I can actually make webcam work (outside of the application).<br />Fortunately, I'm not the first one to have this problem.<br /><a href=\"http://social.msdn.microsoft.com/Forums/en-US/vcgeneral/thread/36971526-95f3-4a9f-a601-1843c86332c1/#page:1\">This application has failed to start because the application configuration is incorrect</a></p><p>The thread suggests to look in the System part of the Event Log for a 'side by side' error - and there it is!</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/02032009_EventLog2.PNG\" alt=\"Event Log Contents\" /></div>" +
            "<p>The thread also contains the solution, which is somewhat complicated but precise, except in my case I have '90' instead of '80'.</p><p>\"You need to copy CRT DLL into your application local folder together with the manifest. Please take a look on this post on my blog, http://blogs.msdn.com/nikolad/archive/2005/03/18/398720.aspx. Basically go to windows\\winsxs folder. Find a folder like x86_Microsoft.VC80.CRT and copy DLL from there to your application local folder. From what I see in your code you need msvcrt80.dll and msvcp80.dll (perhaps msvcrt80d.dll and msvcp80d.dll if this is Debug mode application). Then go to windows\\winsxs\\manifests folder and copy x86_*_Microsoft.VC80.CRT*.manifest to Microsoft.VC80.CRT.manifest to your application local folder.\"</p><p>I copy the 4 files msvcm90.dll, msvcp90.dll, msvcr90.dll and Microsoft.VC90.CRT.manifest into my application folder on the test computer, and it works like a charm. I add these files to the installation package, reinstall the application and it works again. All is well that ends well I guess.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_02032009_k = "C++ Runtime Library development";
        public const string content_02032009_d = "C++ Runtime Libraries Adventures";

        //"Bootstrapper: scrap the bootstrapper"
        public const string content_23022009_b = "<p>Although the \"bootstrapper\" solution worked fine, the decision was made to scrap it completely and do things in a different way.</p>";
        public const string content_23022009_r = "<p>There are a few valid reasons for that:<br />- The application comes together with a few other device driver packages<br />- Currently, these packages are bundled into the .msi package and installed on the client computer, and from there they have to be installed manually<br />- This brings the current .msi package size to over 100MB in size<br />- Not every client computer would use all of the device drivers<br />- If the application will need to support other hardware in the future, using the same approach will keep the .msi package bloating more and more.</p><p>So, the task was changed. The requirement now is to have an application in a separate package, and all the device drivers in separate packages, but the installation process will let users select which device drivers they want to install.</p><p>After some investigation I came to the conclusion that this can not be done using the Visual Studio Setup and Deployment project. The main limiting issue here is the fact that an .msi installer can not be started from withing another .msi installer. Therefore, I can not launch my application installer, show the dialog with options, and proceed to installing these optional drivers.</p><p>A simple solution I came up with was to write a small 'wrapper' Windows Forms application. The application would present the user with multiple checkboxes - one for each optional component.</p><p>After the user makes the choices and presses the 'Install' button, the application would first read the xml file which lists all available components</p><pre class=\"brush:xml\"><br /><?xml version='1.0'?><br /><setuppackages><br /> <package name=\"DriverPackage1\" path=\"\\DP1\\DriverPackage1.exe\"></package><br /> <package name=\"DriverPackage2\" path=\"\\DP2\\DriverPackage2.exe\"></package><br /> ...<br /> <package name=\"MainAppPackage\" path=\"\\App\\MainSetup.exe\"></package><br /></setuppackages><br /></pre><p>The name/path pairs would be added to the </p><pre class=\"brush:csharp\">" +
            @"Dictionary<string, string> packages;" +
            "</pre><p>DriverPackage1, ..., MainAppPackage will be set up as tags for the checkboxes at design time to simplify the functionality.</p><p>The application will then loop through all checkboxes and, if the checkbox is checked, will add the setup file path to the list.</p><pre class=\"brush:csharp\">" +
            "<br />string startupPath = Application.StartupPath;<br /><br />string path;<br />List<string> components = new List<string>();<br /><br />foreach (Control control in this.Controls)<br />{<br /> CheckBox checkBox = control as CheckBox;<br /><br /> if (checkBox != null && checkBox.Checked)<br /> {<br />  if (packages.TryGetValue(checkBox.Tag.ToString(), out path))<br />  {<br />   components.Add(path);<br />  }<br /> }<br />}<br />" +
            "</pre><p>Finally, the application will loop through the list of setup files, executing the installation process for each file and waiting for it to finish before launching next one.</p><pre class=\"brush:csharp\">" +
            @"<br />foreach (string componentPath in components)<br />{<br /> InstallComponent(startupPath + componentPath);<br />}<br /><br />// ..........<br /><br />private void InstallComponent(string filePath)<br />{<br /> System.Diagnostics.Process installerProcess;<br /><br /> installerProcess = System.Diagnostics.Process.Start(filePath);<br /><br /> while (installerProcess.HasExited == false)<br /> {<br />  //indicate progress to user<br />  Application.DoEvents();<br />  System.Threading.Thread.Sleep(250);<br /> }<br />}" +
            "</pre><p>Other small details include a progress bar, success messages etc., but the main idea should be clear.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_23022009_k = "C# Development Setup Deployment Bootstrapper";
        public const string content_23022009_d = "Using a bootstrapper in the setup and deployment project";

        //"Bootstrapper adventures"
        public const string content_22022009_b = "<p>I'm now at the stage of creating a setup package for the application that is going to use that magnificent 3M scanner. It has a setup and deployment project already, so I just rebuild it and try installing on the test desktop. However, during the setup process I end up with an error message.</p>";
        public const string content_22022009_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/22022009_InstallerError.PNG\" alt=\"Installer Error\" /></div>" +
            "<p>After some research, I find out that there is some driver package that needs to be installed before my application install. If I install it manually first, the installation runs smoothly. So I have to include it in the installation somehow. How hard can that be? I have no experience with installation packages, but I get a hint that a custom action can help.</p><p>I do some research and soon enough I find out that</p><a href=\"http://msdn.microsoft.com/en-us/library/bbd7cck3.aspx\">Custom Actions Management in Deployment</a><br /><p>Actions can only be run at the end of an installation.</p><p>That is not what I need, the drivers absolutely have to be installed prior to the application installation. I research more and find a couple of links, which teach me how to use Orca and how to execute my Custom Actions whenever I like.</p><a href=\"http://www.codeproject.com/KB/install/msicustomaction.aspx?df=100&forumid=3159&exp=0&select=1757791\">MSI Custom Action DLL</a><a href=\"http://www.appdeploy.com/messageboards/tm.asp?m=4012&mpage=1&key=&\">Custom Action Run EXE</a><p>Great! Problem solved. I edit my  place my Custom Action before the installation process starts. This time, however, I encounter a '2731' error. I'm not the first one to ever get this error, of course.</p><a href=\"http://n2.nabble.com/Problem-when-trying-to-install-.NET-framwork-2.0-during-MSI-install-,-plz-any-idea-td708807.html\">Problem when trying to install .NET framwork 2.0 during MSI install</a><p>\"It is probably failing because you are trying to invoke an installer when an installer is already running.  You need to install separate installers sequentially, not from within one another.  You would need a bootstrapper to do that. \"</p><p>Well, that's what I should have known in the very beginning. OK then, now to create a bootstrapper. (And what is the bootstrapper, by the way?)</p><a href=\"http://msdn.microsoft.com/en-us/magazine/cc163899.aspx\">Use the Visual Studio 2005 Bootstrapper to Kick-Start Your Installation</a><a href=\"http://www.clariusconsulting.net/blogs/pga/comments/42831.aspx\">Creating a bootstrapper for a VS Shell application</a><p>These 2 pages give me some ideas. I locate my C:\\Program Files\\Microsoft Visual Studio 8\\SDK\\v2.0\\BootStrapper\\Packages folder, create product.xml and package.xml to be as simple as possible, and now I can choose my package from MySetupProject->Properties->Prerequisites.</p><p>The application now can be installed smoothly, but there is still one thing I am not happy about. The package consists of setup.exe, the .msi package, and a subfolder with my driver package. I do not want the subfolder, that might be confusing for the user or the subfolder may get 'lost' somewhere in the process of application distribution. I'm looking for the soluton:</p><a href=\"http://stackoverflow.com/questions/567592/bootstrapper-how-to-compile-the-application-and-prerequisite-in-single-msi-pack\">Bootstrapper: How to compile the application and prerequisite in single .msi package?</a><br/><a href=\"http://babek.info/libertybasicfiles/lbnews/nl134/iexpress.htm\">IExpress Installer</a><p>And the IExpress seems to work fine for me. I create the single-file installation package, copy it to the test desktop and run ... just to be presented with another error. After examining the installation log, I realise that the IExpress did not extract my driver to the subfolder, but the installer expected to find it in the subfolder. Apparently, IExpress does not support the subfolders. I need another trick. A google search returns me to the page I have seen already and I read it again, carefully ... to the end.</p><a href=\"http://www.clariusconsulting.net/blogs/pga/comments/42831.aspx\">Creating a bootstrapper for a VS Shell application</a><p>There it is, my solution:</p><blockquote>Unfortunately, the MSBuild task doesn't provide the option to have the configuration resource use prerequisite installers found in the target directory, so you must manually update the appropriate resource file to remove the hard-coded path that looks for prerequisites in a sub-directory of the same name. <br /><br />- Open the Setup.exe program in Visual Studio's resource editor <br />- Double-click the resource named, SETUPCFG in the 41 folder <br />- Search for the \"Vs Shell\\\" string and delete the two occurrences that appear <br />- Save the resource file and the Setup.exe executable will be updated automatically <br />- Run iexpress <br />- Create a new package by following the IExpress wizard's steps and make sure to include the following files ...</blockquote><p>Some careful setup.exe editing follows (first attempt was unsuccessful, I spoiled the .exe and had to rebuild my project again) and I have the complete solution - my single-file installation package, that has a prerequisite that is installed before the installation of the main application.</p><p>However, that was not the end ...</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_22022009_k = "C# Development Setup Deployment Bootstrapper";
        public const string content_22022009_d = "Using a bootstrapper in the setup and deployment project";

        //"Mysterious validation function"
        public const string content_18022009_b = "<p>Making changes to some application and having some problems with validation, I came across this validation function:</p>";
        public const string content_18022009_r = "<pre class=\"brush:csharp\">" +
            @"public new bool Validate(bool someParameter)<br />{<br /> bool blnResult = true;<br /><br /> if (name != null)<br /> {<br />  if (!name.Validate())<br />   blnResult = false;<br /> }<br /><br /> if (address != null)<br /> {<br />  if (!address.Validate(someParameter))<br />   blnResult = false;<br /> }<br /><br /> if (somethingElse != null)<br /> {<br />  if (!somethingElse.Validate())<br />   blnResult = false;<br /> }<br /> <br /> if (someMore != null)<br /> {<br />  if (!someMore.Validate())<br />   blnResult = false;<br /> }<br /><br /> return blnResult;<br />}" +
            "</pre><p>So I asked myself, why would this function go through all validations each time even if it knows after the very first one that the blnResult is false and that will not change?</p><p>After some thought and investigation, the most likely answer is that the application was growing little by little. So, whenever its functionality was extended, say, from just keeping names to keeping names and addresses, the person currently in charge of the application would just copy and paste this bit</p><pre class=\"brush:csharp\">" +
            @"if (name != null)<br />{<br /> if (!name.Validate())<br />  return false;<br />}" +
            "</pre><p>replace name with address and move on.</p><p>After I made some small change, the function does not look much different, but I have much less troubles with validation now.</p><pre class=\"brush:csharp\">" +
            @"public new bool Validate(bool someParameter)<br />{<br /> if (name != null)<br /> {<br />  if (!name.Validate())<br />   return false;<br /> }<br /><br /> if (address != null)<br /> {<br />  if (!address.Validate(someParameter))<br />   return false;<br /> }<br /><br /> if (somethingElse != null)<br /> {<br />  if (!somethingElse.Validate())<br />   return false;<br /> }<br /> <br /> if (someMore != null)<br /> {<br />  if (!someMore.Validate())<br />   return false;<br /> }<br /><br /> return true;<br />}" +
            "</pre><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_18022009_k = "Random thoughts";
        public const string content_18022009_d = "Mysterious validation function";

        //"A small functionality change"
        public const string content_16022009_b = "<p>Here's what can happen when a small functionality change is required in a fairly complex application. In this case, there are a few TabPages that are generated dynamically. Some of the TabPages have a 'Save Details' button.</p><p>The change: one of the TabPages now should NOT have a 'Save Details' button if the object is a 'new' object (not yet saved in the database). Easy!</p>";
        public const string content_16022009_r = "<p>Solution:</p><pre class=\"brush:csharp\">" +
            @"this.tabControl.Controls.Add(this.mySomethingTabPage);<br /><br />HideOrShowSaveButton(this.mySomethingTabPage); // solution<br /><br />...<br /><br />public void HideOrShowSaveButton(TabPage tabPage)<br />{<br /> //'save details' should not appear on the 'MySomething' tab page if <br /> //the object is a new object<br /><br /> //find 'save details' button<br /><br /> ToolStripButton mySmallSaveButton = null;<br /><br /> foreach (Control c in this.mySomethingTabPage.Controls)<br /> {<br />  SomeDetails.SomeDetailsHeadingView pdhv = c as SomeDetails.SomeDetailsHeadingView;<br /><br />  if (pdhv != null)<br />  {<br />   foreach (Control c1 in pdhv.Controls)<br />   {<br />    ToolStrip headingToolStrip = c1 as ToolStrip;<br /><br />    if (headingToolStrip != null)<br />    {<br />     foreach (ToolStripItem item in headingToolStrip.Items)<br />     {<br />      if (item.Name == ""SaveButton"")<br />      {<br />       mySmallSaveButton = item as ToolStripButton;<br /><br />       //hide button on the 'someDetails' tab if new object<br />       if (mySmallSaveButton != null)<br />       {<br />        if (Global.currentObject.IsNewObject)<br />        {<br />         mySmallSaveButton.Visible = false;<br />         mySmallSaveButton.Enabled = false;<br />        }<br />        else<br />        {<br />         mySmallSaveButton.Visible = true;<br />         mySmallSaveButton.Enabled = true;<br />        }<br />        return;<br />       }<br />      }<br />     }<br />    }<br />   }<br />  }<br /> }<br />}" +
            "</pre><p>Had to hardcode the 'SaveButton' name which I would really want to avoid, but did not want to spend more time on that.</p><p>Thoughts: this application could have been designed a little better to make future changes not so painful.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_16022009_k = "C# development hardcoding bad";
        public const string content_16022009_d = "A small functionality change";

        //"Asynchronous calls to a WebService"
        public const string content_12022009_b = "<p>Figured out how to call a WebService asynchronously using a Callback in .NET. The process is fairly easy and straightforward.</p><p>This article <a href=\"http://www.stardeveloper.com/articles/display.html?article=2001121901&page=1\">Professional ASP.NET Web Services : Asynchronous Programming</a> provided me with most of the information that I needed, and even had a few solutions discussed, of which I chose the one which suits me best</p>";
        public const string content_12022009_r = "<p>First of all, of course, I need a WebService</p><pre class=\"brush:csharp\">" +
            @"namespace AsyncService<br />{<br />    /// <summary><br />    /// Summary description for Service1<br />    /// </summary><br />    [WebService(Namespace = ""http://tempuri.org/"")]<br />    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]<br />    [ToolboxItem(false)]<br />    // To allow this Web Service to be called from script, using ASP.NET AJAX,<br />    // uncomment the following line. <br />    // [System.Web.Script.Services.ScriptService]<br />    public class AsyncService : System.Web.Services.WebService<br />    {<br /><br />        [WebMethod(Description=""Return a random value under 1000"")]<br />        public int[] GetRandomValue(int id, int delay)<br />        {<br />            Random random = new Random();<br />            int randomValue = random.Next(1000);<br />            int[] returnValue = new int[] { id, randomValue };<br />            Thread.Sleep(delay);<br />            return returnValue;<br />        }<br />    }<br />}" +
            "</pre><p>This service only returns a random number. It also returns the number after some delay to imitate that it is actually does something useful.</p><p>A Web Service proxy class provides a wrapper that lets me communicate to a WebService.</p><pre class=\"brush:csharp\">" +
            @"namespace AsyncCaller<br />{<br />    [WebServiceBindingAttribute(Name = ""AsyncRequestSoap"", Namespace<br />        = ""http://tempuri.org/"")]<br />    public class AsyncCallerProxy : SoapHttpClientProtocol<br />    {<br />        public AsyncCallerProxy()<br />        {<br />            this.Url = ""http://localhost/MyAsyncService/AsyncService.asmx"";<br />        }<br /><br />        [SoapDocumentMethodAttribute(""http://tempuri.org/GetRandomValue"",<br />         Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]<br />        public int[] GetRandomValue(int id, int delay)<br />        {<br />            object[] results = this.Invoke(""GetRandomValue"", new object[]<br />               { id, delay });<br />            return ((int[])results[0]);<br />        }<br /><br />        public IAsyncResult BeginGetRandomValue(int id, int delay,<br />            AsyncCallback callback, object asyncState)<br />        {<br />            return this.BeginInvoke(""GetRandomValue"", new object[] {<br />            id, delay}, callback, asyncState);<br />        }<br /><br />        public int[] EndGetRandomValue(IAsyncResult asyncResult)<br />        {<br />            object[] results = this.EndInvoke(asyncResult);<br />            return ((int[])(results[0]));<br />        }<br />    }<br />}" +
            "</pre><p>Now I want to make a small demonstration of asynchronous communication to the WebService.</p><p>Before that I would need a very simple helper class to make it easy.</p><pre class=\"brush:csharp\">" +
            @"public class AsyncHelper<br />    {<br />        public AsyncHelper(int id)<br />        {<br />            this.HelperID = id;<br />            Random random = new Random();<br />            this.RandomDelay = random.Next(10000);<br />        }<br /><br />        int _helperID = 0;<br /><br />        public int HelperID<br />        {<br />            get { return _helperID; }<br />            set { _helperID = value; }<br />        }<br /><br />        int _randomDelay = 0;<br /><br />        public int RandomDelay<br />        {<br />            get { return _randomDelay; }<br />            set { _randomDelay = value; }<br />        }<br /><br />        int _randomResult = 0;<br /><br />        public int RandomResult<br />        {<br />            get { return _randomResult; }<br />            set { _randomResult = value; }<br />        }<br />    }" +
            @"</pre><p>When an instatnce of the class is created, it is assigned a random delay value. I will pass it to the WebService and will get a response after a number of milliseconds defined by RandomDelay value.</p><p>On my demo application form I have a button and two DataGridViews.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/12022009_AsynchronousCall.JPG\" alt=\"Asynchronous Call To The WebService Demo\" /></div><p>When a button is pressed, an instance of the AsyncHelper class is created and added to the list of currently running requests, which is bound to the first DataGridView. The ID of the class instance and the delay value are passed to the WebService.</p><p>After the delay, the WebService returns the ID and the random ‘Result’. An instance of the AsyncHelper is found by ID, the result is assigned and the instance is removed from the current requests list and added to the processed requests list. If the button is pressed multiple times, a user can see multiple requests being added to the list and being returned by the WebService after the delays specified.</p><pre class=\"brush:csharp\">" +
            @"public partial class Form1 : Form<br />    {<br />        AsyncCallerProxy objWebService = new AsyncCallerProxy();<br /><br />        //counter for number of requests sent<br />        private int _requestCounter = 0;<br />        private List<AsyncHelper> _asyncRequests = new List<AsyncHelper>();<br />        private List<AsyncHelper> _asyncRequestsProcessed = <br />             new List<AsyncHelper>();<br /><br />        public Form1()<br />        {<br />            InitializeComponent();<br />            bindingSourceRequests.DataSource = _asyncRequests;<br />            dataGridViewRequests.DataSource = bindingSourceRequests;<br /><br />            bindingSourceRequestsProcessed.DataSource = _asyncRequestsProcessed;<br />            dataGridViewRequestsProcessed.DataSource =<br />                bindingSourceRequestsProcessed;<br />            <br />            dataGridViewRequests.Columns[0].DataPropertyName = ""HelperID"";<br />            dataGridViewRequests.Columns[1].DataPropertyName = ""RandomDelay"";<br /><br />            dataGridViewRequestsProcessed.Columns[0].DataPropertyName =<br />              ""HelperID"";<br />            dataGridViewRequestsProcessed.Columns[1].DataPropertyName = <br />              ""RandomDelay"";<br />            dataGridViewRequestsProcessed.Columns[2].DataPropertyName = <br />              ""RandomResult"";<br />        }<br /><br />        private void buttonRequest_Click(object sender, EventArgs e)<br />        {<br />            //create a new request and add it to request queue<br />            AsyncHelper newRequest = new AsyncHelper(_requestCounter);<br />            _requestCounter++;<br /><br />            _asyncRequests.Add(newRequest);<br /><br />            AsyncCallback asyncCallback = new AsyncCallback(MyCallBack);<br /><br />            IAsyncResult asyncResult;<br /><br />            asyncResult = objWebService.BeginGetRandomValue<br />              (newRequest.HelperID, newRequest.RandomDelay, asyncCallback, null);<br /><br />            UpdateRequestQueueDisplay();<br />        }<br /><br />        private void MyCallBack(IAsyncResult asyncResult)<br />        {<br />            int[] returnValue = objWebService.EndGetRandomValue(asyncResult);<br />            int id = returnValue[0];<br />            int result = returnValue[1];<br /><br />            AsyncHelper currentRequest = <br />                _asyncRequests.Find(delegate(AsyncHelper testRequest)<br />                {return testRequest.HelperID == id;});<br /><br />            //request is processed, remove it from the queue and add to processed<br />            //requests list<br />            if (currentRequest != null)<br />            {<br />                currentRequest.RandomResult = result;<br />                _asyncRequestsProcessed.Add(currentRequest);<br />                _asyncRequests.Remove(currentRequest);<br /><br />                UpdateRequestQueueDisplay();<br />            }<br />        }<br /><br />        private void UpdateRequestQueueDisplay()<br />        {<br />            //fixes the cross-thread issue while accessing the form controls<br />            this.BeginInvoke(new MethodInvoker(delegate()<br />            {<br />                this.bindingSourceRequests.ResetBindings(false);<br />                this.bindingSourceRequestsProcessed.ResetBindings(false);<br />            }));<br /><br />        }<br />    }" +
            "</pre><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_12022009_k = "WebService C# development asynchronous";
        public const string content_12022009_d = "Calling a WebService asynchronously";

        //"Top-level exception handling"
        public const string content_11022009_b = "<p>Found a good article on exception handling. <a href=\"http://richnewman.wordpress.com/2007/04/08/top-level-exception-handling-in-windows-forms-applications-part-1/\">Top-level Exception Handling in Windows Forms Applications</a> and followed some advice from it.</p><p>What it meant for me, basically, is that I changed the Program.cs file of my application from this</p>";
        public const string content_11022009_r = "<pre class=\"brush:csharp\">" +
            @"using System;<br />using System.Collections.Generic;<br />using System.Linq;<br />using System.Windows.Forms;<br /><br />namespace MyNameSpace<br />{<br />    static class Program<br />    {<br />        /// <summary><br />        /// The main entry point for the application.<br />        /// </summary><br />        [STAThread]<br />        static void Main()<br />        {<br />            Application.EnableVisualStyles();<br />            Application.SetCompatibleTextRenderingDefault(false);<br />            Application.Run(new FormUpdater());<br />        }<br />    }<br />}" +
            "</pre><p>to this</p><pre class=\"brush:csharp\">" +
            @"using System;<br />using System.Collections.Generic;<br />using System.Linq;<br />using System.Windows.Forms;<br />using System.Threading;<br /><br />namespace MyNameSpace<br />{<br />    static class Program<br />    {<br />        /// <summary><br />        /// The main entry point for the application.<br />        /// </summary><br />        [STAThread]<br />        static void Main()<br />        {<br />            Application.ThreadException += <br />                new ThreadExceptionEventHandler(new <br />                  ThreadExceptionHandler().ApplicationThreadException);<br /><br />            Application.EnableVisualStyles();<br />            Application.SetCompatibleTextRenderingDefault(false);<br />            Application.Run(new FormUpdater());<br />        }<br /><br />        public class ThreadExceptionHandler<br />        {<br />            public void ApplicationThreadException<br />                  (object sender, ThreadExceptionEventArgs e)<br />            {<br />                MessageBox.Show(e.Exception.Message, ""Error"", <br />                      MessageBoxButtons.OK, MessageBoxIcon.Error);<br />            }<br />        }<br />    }<br />}" +
            "</pre><p>and that allowed me to get rid of a couple of dozens try/catch blocks in the application code without losing any exception handling functionality. Quite handy.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_11022009_k = "Windows development top level exception handling";
        public const string content_11022009_d = "Practicing top level exception handling";

        //"Scanner Update"
        public const string content_10022009_b = "<p>I tried the solution suggested in this thread. <a href=\"http://social.microsoft.com/Forums/en-US/netfxbcl/thread/4b350704-d9b3-4920-a68a-802f5f4e8a6a\">HOW TO RETRIEVE INSTALLED SCANNER</a></p>";
        public const string content_10022009_r = "<p>I did the following:</p><p><li>Downloaded wiaaut.dll</li> <br /><li>Copied it to system32</li> <br /><li>Registered it with \"regsvr32 wiaaut.dll\" (successfully)</li><br /><li>Added a reference to wiaaut.dll to my project in Visual Studio.NET</li><br /><li>Checked that the Windows Image Acquisition (WIA) service is running</li></p><p>Next, I added and debugged the following code:</p><pre class=\"brush:csharp\">" +
            @"WIA.DeviceManager manager = new WIA.DeviceManagerClass();<br />WIA.DeviceManagerClass managerClass = new WIA.DeviceManagerClass();<br /><br />string wdeviceName = """";<br />foreach (WIA.DeviceInfo info in manager.DeviceInfos)<br />{<br /> if (info.Type == WIA.WiaDeviceType.ScannerDeviceType)<br /> {<br />  foreach (WIA.Property p in info.Properties)<br />  {<br />   if (p.Name == ""Name"")<br />   {<br />    wdeviceName = ((WIA.IProperty)p).get_Value().ToString();<br />    Console.WriteLine(wdeviceName);<br />   }<br />  }<br /> }<br />}" +
            "</pre><p>However, the manager.DeviceInfos is always empty. I have 2 scanners attached, one of them shows in Control Panel->Scanners and Cameras, one doesn't, and both show under \"Imaging Devices\" in Device manager.</p><p>At this point, the only idea I have is that the scanner drivers just do not support WIA. As long as I have no way to check if this is true or not, I'll have to stick to the yesterday's solution.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_10022009_k = "WMI Windows instrumentation C# development scanner";
        public const string content_10022009_d = "I continue working with scanners";

        //"Finding a scanner"
        public const string content_09022009_b = "<p>I need to find out what scanners are attached to the computer. I also need to give the user of my application an option to select the default scanner, and to change this selection at any time. Fortunately, the scanner can only be one of a few models. Therefore, this is the solution I came up with so far:</p>";
        public const string content_09022009_r = "<pre class=\"brush:csharp\">" + @"ArrayList scanners = new ArrayList();<br />            <br />ManagementObjectSearcher search = new System.Management.ManagementObjectSearcher<br />(""SELECT * From Win32_PnPEntity"");<br /><br />ManagementObjectCollection deviceCollection = search.Get();<br /><br />foreach (ManagementObject info in deviceCollection)<br />{<br />    string deviceName = Convert.ToString(info[""Caption""]);<br /><br />    if (/* check deviceName for certain substrings */)<br />    {<br />        scanners.Add(deviceName);<br />    }<br />}" +
            "</pre><p>However, there are at least two things that can be improved, though I don't know if they're possible.</p><p>First, I would like to get only those devices that are under \"Imaging devices\" in Device Manager. That would be a huge improvement as I currently have almost 200 entries in the deviceCollection, and only 2 of them are under \"Imaging Devices\".</p><p>Also, I would like to find a way to check if the device is a scanner. That would help to provide a \"general\" solution where the scanner attached may be of any model.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_09022009_k = "WMI Windows instrumentation C# development scanner";
        public const string content_09022009_d = "Using Windows Instrumentation to find a scanner attached to a PC programmatically";

        //"Wrote a Windows Service"
        public const string content_05022009_b = "<p>Wrote my first windows service today. Was not hard at all, I mostly followed the guide</p><a href=\"http://www.codeproject.com/KB/dotnet/simplewindowsservice.aspx\"> Simple Windows Service Sample</a>";
        public const string content_05022009_r = "<p>However, the way the service in the sample is logging events was not suitable for me. I needed to log the events into the Application log of the Event Viewer. I also needed to catch exceptions and log them as errors in the Application log. So I used the powers of Enterprise Library event logging and exception handling. Firstly, I added references to EnterpriseLibrary and the corresponding 'usings' to the windows service</p><pre class=\"brush:csharp\">" +
            @"<br />using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;<br />using Microsoft.Practices.EnterpriseLibrary.Logging;<br />" +
            "</pre><p>Then, I added the loggingConfiguration and exceptionHandling sections to the appConfig file, that looked like this:</p><pre class=\"brush:xml\">" +
            @"<br /> &lt;configSections&gt;<br />  &lt;section name=""loggingConfiguration"" <br />type=""Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.LoggingSettings,<br />Microsoft.Practices.EnterpriseLibrary.Logging, Version=2.0.0.0, Culture=neutral,<br />PublicKeyToken=null"" /&gt;<br />  &lt;section name=""exceptionHandling"" type=<br />""Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Configuration.<br />ExceptionHandlingSettings,<br />Microsoft.Practices.EnterpriseLibrary.ExceptionHandling,<br />Version=2.0.0.0, Culture=neutral, PublicKeyToken=null"" /&gt;<br /> &lt;/configSections&gt;<br /> <br /> ...<br /> <br />  &lt;loggingConfiguration name=""Logging Application Block"" tracingEnabled=""true""<br />  defaultCategory="""" logWarningsWhenNoCategoriesMatch=""true""&gt;<br />  &lt;listeners&gt;<br />   &lt;add source="" "" formatter=""Text Formatter"" log=""Application""<br />     machineName="""" listenerDataType=<br />""Microsoft.Practices.EnterpriseLibrary.Logging.Configuration.<br />FormattedEventLogTraceListenerData,<br />Microsoft.Practices.EnterpriseLibrary.Logging, Version=2.0.0.0, Culture=neutral,<br />PublicKeyToken=null""<br />     traceOutputOptions=""None"" type=""Microsoft.Practices.EnterpriseLibrary.Logging.<br />TraceListeners.FormattedEventLogTraceListener,<br />Microsoft.Practices.EnterpriseLibrary.Logging, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null""<br />     name=""MyService EventLog TraceListener"" /&gt;<br />  &lt;/listeners&gt;<br />  &lt;formatters&gt;<br />   &lt;add template=""Timestamp: {timestamp}&#xD;&#xA;Message: {message}&#xD;&#xA;Category: {category}&#xD;""<br />     type=""Microsoft.Practices.EnterpriseLibrary.Logging.Formatters.TextFormatter,<br />Microsoft.Practices.EnterpriseLibrary.Logging, Version=2.0.0.0, Culture=neutral,<br />PublicKeyToken=null"" name=""Text Formatter"" /&gt;<br />  &lt;/formatters&gt;<br />  &lt;categorySources&gt;<br />   &lt;add switchValue=""All"" name=""MyService""&gt;<br />    &lt;listeners&gt;<br />     &lt;add name=""MyService EventLog TraceListener"" /&gt;<br />    &lt;/listeners&gt;<br />   &lt;/add&gt;<br />  &lt;/categorySources&gt;<br />  &lt;specialSources&gt;<br />   &lt;allEvents switchValue=""All"" name=""All Events"" /&gt;<br />   &lt;notProcessed switchValue=""All"" name=""Unprocessed Category"" /&gt;<br />   &lt;errors switchValue=""All"" name=""Logging Errors &amp; Warnings""&gt;<br />    &lt;listeners&gt;<br />     &lt;add name=""MyService EventLog TraceListener"" /&gt;<br />    &lt;/listeners&gt;<br />   &lt;/errors&gt;<br />  &lt;/specialSources&gt;<br /> &lt;/loggingConfiguration&gt;<br /><br /> &lt;exceptionHandling&gt;<br />  &lt;exceptionPolicies&gt;<br />   &lt;add name=""PagingPolicy""&gt;<br />    &lt;exceptionTypes&gt;<br />     &lt;add type=""System.Exception, mscorlib, Version=2.0.0.0, Culture=neutral,<br />PublicKeyToken=b77a5c561934e089""<br />       postHandlingAction=""None"" name=""Exception""&gt;<br />      &lt;exceptionHandlers&gt;<br />       &lt;add logCategory=""MyService"" eventId=""100"" severity=""Error""<br />         title=""MyService Exception Handling"" formatterType=<br />""Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.TextExceptionFormatter,<br />Microsoft.Practices.EnterpriseLibrary.ExceptionHandling, Version=2.0.0.0, Culture=neutral,<br />PublicKeyToken=null"" priority=""0"" type=<br />""Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Logging.LoggingExceptionHandler, <br />Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.Logging,<br />Version=2.0.0.0, Culture=neutral, <br />PublicKeyToken=null""<br />         name=""MyService Logging Handler"" /&gt;<br />      &lt;/exceptionHandlers&gt;<br />     &lt;/add&gt;<br />    &lt;/exceptionTypes&gt;<br />   &lt;/add&gt;<br />  &lt;/exceptionPolicies&gt;<br /> &lt;/exceptionHandling&gt;<br />" +
            "</pre><p>I'll be honest, I would not be able to explain every single line in this XML snippet. I just know that it works that way, and when I try removing some parts of it which seem to be unnecessary to me, the whole application usually starts failing.</p><p>Next, I put the following into the OnStart method of the service:</p><pre class=\"brush:csharp\">" +
            @"<br />            try<br />            {<br />                Logger.Write(""MyService Process Started: "" + getNow(), ""MyService"");<br />                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);<br />                timer.Interval = 10000;<br />                timer.Enabled = true;<br />            }<br />            catch (Exception ex)<br />            {<br />                ExceptionPolicy.HandleException(ex, ""MyService"");<br />                errorCount++;<br />            }<br />   " +
            "</pre><p>and into the OnStop method</p><pre class=\"brush:csharp\">" +
            @"<br />            Logger.Write(""MyService Process Stopped: "" + getNow(), ""MyService"");<br />            timer.Enabled = false;<br />   " +
            "</pre><p>and into the timer_Elapsed method</p><pre class=\"brush:csharp\">" +
            @"<br />   Logger.Write(""running MyService process at "" + getNow(), ""MyService"");<br />   RunMainFunction();<br />   " +
            "</pre><p>(getNow() simply returns DateTime stamp in the required format)</p><p>and I started getting events written into the Application log.</p><p>Actually, logging events into the event log seems to be faster and easier than trying to debug the service. At least with my little service, where the whole cycle of stopping service -> uninstallation -> building a service + installation package -> installation -> starting a new version of a service can be done in under one minute.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_05022009_k = "C# programming Windows service";
        public const string content_05022009_d = "I wrote my first windows service";

        //"Team Foundation Server 2008 Adventures"
        public const string content_04022009_b = "<p>I’m playing with the Team Foundation Server 2008 these days. After I mastered the installation process, time has come to play around with security settings, group memberships and things like that.The first real issue I came across was having a problem with adding a Windows Group to TFS Licensed Users group. This is done through Team->Team Foundation Server Settings -> Group Memberships. The error message I got was very uninspiring and unhelpful.</p>";
        public const string content_04022009_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/04022009_TFSError1.JPG\" alt=\"Team Foundation Server Error\" /></div><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2009/04022009_TFSError2.JPG\" alt=\"Team Foundation Server Error\" /></div><p>At the same time I could add single users from the same group without any problems. So my next step was to try to add every user from the group individually one by one. This way, after adding five users, I got the more helpful error message</p><p>Aha! There is an error code this time. Now I can find out why. Turns out that the 5 user limitation is specific to the TFS – Workgroup edition. Ok, I do not even know for sure what is the edition I have installed and it does not say on the Help->About.</p><p>This page comes very handy</p><a href=\"http://blogs.msdn.com/robcaron/archive/2006/08/15/701843.aspx\">Which Version of Team Foundation Server Do I Have?</a><p>So, I check the value of the registry key under <strong>HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\VisualStudio\\9.0\\TeamFoundation\\Registration\\Edition_CT</strong> And it is ‘Workgroup’ indeed. Confirmed!</p><p>Now I need to upgrade to the Standard Edition. I have the key, but when I go to Add and Remove programs, run Change/Remove and select the option to Upgrade, the boxes where I’m supposed to enter the license key are grayed out! I’m stuck!</p><p>Luckily, I find some help.<p><a href=\"http://social.msdn.microsoft.com/Forums/en-US/tfssetup/thread/2a13ec88-e199-48e6-8312-de9602cf7577/\">Upgrading TFS 2008 Workgroup to Std. Edition</a><p>Here is what the wise guys from Microsoft advise:</p><p>1. Find a Setup.sdb file at the folder</p><p><ProgramPath>\\Microsoft Visual Studio 2008 Team Foundation Server\\Microsoft Visual Studio 2008 Team Foundation Server - ENU\\</p><p>2. Open the Setup.sdb file with text editor; you can back up the file before editing it.</p><p>3. Remove the “[Product Key]” line and the PID line after it in the file.</p><p>Try to upgrade again.</p><p>And this works. I re-check the registry key value and, indeed, it has changed to “Full” which means Standard Edition. Hooray! However, when I try to add a 6th user to the TFS Licensed users, I still come up with the same error. Now I consider reinstalling from scratch! Next, I come across this discussion.</p><a href=\"http://social.msdn.microsoft.com/Forums/en-US/tfssetup/thread/21eb519c-7871-4e4b-ade8-3614b3a1d2a3/\">Upgrading from limited version</a><p>And the last bit of advice seems to be my case</p><p>“If you are using the full RTM version (not the workgroup version) do not use the Licensed Users Group.  It is not used by the full version but is still limited to 5 users.  Just add the users to the project groups.”</p><p>And it works too. I can add Windows Users and Groups to project groups.</p><p>Finally, a few words about deleting projects.<br />A simple way to delete the project forever is explained here</p><a href=\"http://aspadvice.com/blogs/ssmith/archive/2006/03/10/Team-System-Delete-Project.aspx\">Team System Delete Project</a><p>“If you need to delete a Team System Project you need to do it through a command line utility that is installed with Team Explorer.  There is no way to delete a project from Team System except through the command line tool, TFSDeleteProject.exe.  This utility is in the c:\\program files\\Microsoft Visual Studio 8\\Common7\\IDE\\ folder by default.  To delete a project, use the following syntax:</p><p>TFSDeleteProject /server:ServerName ProjectName”</p><p>However, if you are getting the TF30063 error, it might be worth looking at this post, it worked like a charm in my situation:</p><a href=\"http://vsts-fu.blogspot.com/2008/10/tf30063-you-are-not-authorized-to.html\">TF30063: You are not authorized to access...</a><p>“Ever try to delete a team project and get the above message? Are you in the Team Foundation Administrators group and scratching your head?”</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_04022009_k = "Team Foundation Server 2008 limitations";
        public const string content_04022009_d = "First encounter with Team Foundation Server 2008. First troubles. First limitations";

        //"Another one!"
        public const string content_26012009_b = "<p>I started getting a very unexpected error today in the application I’m working on. I did a quick search on google and resorted to asking a question on the StackOverflow website</p>";
        public const string content_26012009_r = "<a href=\"http://stackoverflow.com/questions/427007/the-key-userid-does-not-exist-in-the-appsettings-configuration-section\">The key ‘UserID’ does not exist in the appSettings configuration section.</a><p>Basically, I started getting this error while trying to open 2 of some 10+ forms in my Window Forms application in designer.</p><pre class=\"brush:js\">" +
            @"To prevent possible data loss before loading the designer, the following errors must be <br />resolved: <br />The key 'UserID' does not exist in the appSettings configuration section." + "</pre><p>While waiting for a kind soul to reply to my question, I continued searching through Call Stacks, trying to find the cause of a problem. And I found it eventually. What the VS designer was really complaining about, was the fact that I was calling a stored procedure from the user control’s InitializeComponent.</p><p>Well, I have a couple of comboboxes on the user control which a populated from the database, so I decided to choose the simplest way to make sure they were populated by the time the user control was shown. Looks like it was not such a good idea. I ended up moving the calls into a separate function, and calling that function from a hosting form after the user control is shown in the hosting form. This appears to work for me. Took me a whopping 58 minutes to fix an error I introduced by being careless and ignorant, hope it’s not project-breaking thing.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_26012009_k = "Stored Procedure User Control InitializeComponent C# development";
        public const string content_26012009_d = "Calling a stored procedure from a User Control's InitializeComponent is not a good idea";

        //"Changed appearance"
        public const string content_20012009_b = "<p>Inspired by this blog entry I found</p><p><a href=\"http://developertips.blogspot.com:80/2007/08/syntaxhighlighter-on-blogger.html\">Using SyntaxHighlighter on BLOGGER</a></p>";
        public const string content_20012009_r = "<p>I spent some time updating my blog and posts to use the SyntaxHighlighter. I think it looks a little better now, so I'll keep it this way.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_20012009_k = "Blogger SyntaxHighlighter";
        public const string content_20012009_d = "I start using SyntaxHighlighter with Blogger";

        //"My Visual Studio Error Puzzle"
        public const string content_16122008_b = "<p>OK, here’s my small puzzle I came across today.<br /><br />Start Visual Studio 2008. Select New Project -> Visual C# -> Windows -> Windows Forms Application. WindowsFormsApplication1 seems to be a good enough name. Hit OK.</p>";
        public const string content_16122008_r = "Now, go to Solution Explorer, right-click WindowsFormsApplication1 and select ‘add Class’. Class1 seems to be a good enough name. Hit OK.<br /><br />Now, make this class inherit from SoapHttpClientProtocol. For this purpose, add a reference to ‘System.Web.Services’ to References.<br /><br />Now, decorate this new Class1 with the WebServiceBindingAttribute.<br />You’ll end up with code as simple as this:<br /><pre class=\"brush:csharp\">" +
            @"<br />using System;<br />using System.Collections.Generic;<br />using System.Linq;<br />using System.Text;<br />using System.Web.Services.Protocols;<br />using System.Web.Services;<br /><br />namespace WindowsFormsApplication1<br />{<br />    [WebServiceBindingAttribute(Name = ""AsyncRequestSoap"", <br />        Namespace = ""http://tempuri.org/"")]<br />    class Class1 : SoapHttpClientProtocol<br />    {<br />    }<br />}<br />" +
            "</pre><br />The project builds without errors and, if actual code is added to Class1, it will execute without errors. However, if I double-click 'Class1.cs' in Solution Explorer, I would be presented with an error screen like this one:<br /><br />" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2008/16122008_WebServiceBindingAttribute.png\" alt=\"WebServiceBindingAttribute\" /></div>" + "<br /><br />This seems to be a bug in Visual Studio since it’s obvious that WebServiceBindingAttribute attribute is added to the class and the application will compile and execute regardless of the error screen. Seems to be safe to ignore. However, I did not find clarification on that yet.<br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_16122008_k = "WebServiceBindingAttribute Visual Studio bug";
        public const string content_16122008_d = "Potential bug in Visual Studio? ";

        //"I'm a scanning developer"
        public const string content_15122008_b = "<p>I spent a couple of days investigating what this device can do:</p>";
        public const string content_15122008_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2008/10122008_3M_Document_Reader.png\" alt=\"3M Document Reader\" /></div><p>This smart device is supposed to be able to scan all sorts of passports and other identification documents and retrieve values like names, date of birth, document expiry dates and such. The documentation is not very straightforward and the code samples are in C++ only so it took me a bit of time to figure out how to operate this device programmatically.</p><p>First of all, a DocAuth.dll has to be referenced. (There is a bunch of development resources on the CD that comes with the device, and the dll’s are there too). After that, an object of a ReaderClass can be created.</p><pre class=\"brush:csharp\">" +
            @"using DOCAUTHLib;<br />...<br />r = new ReaderClass();" +
            "</pre><p>then the connection to the device has to be established</p><pre class=\"brush:csharp\">" +
            @"r.Connect(""D72086"", ""Scanner"", ""ProcessingDone"");" +
            "</pre><p>\"ProcessingDone\" is the comma-separated string that specifies which events will be captured. Other events are “NewDocument ”, “DocumentIdentified ”, etc. After the connection is established, you can subscribe to the scanner events.</p><pre class=\"brush:csharp\">" +
            @"r._IReaderEvents_Event_ReaderEvent += new _IReaderEvents_ReaderEventEventHandler(r__IReaderEvents_Event_ReaderEvent);<br />void r__IReaderEvents_Event_ReaderEvent(string @event){}" +
            "</pre><p>In this case, for example, if subscription is made to “ProcessingDone” event only, then whenever a new document is placed into the scanner, it will be automatically scaneed and after that the event will be fired and captured by the function. This might not always be very handy, so I chose to run scans manually. This is done by simply calling</p><pre class=\"brush:csharp\">" +
            @"r.DoScan();" + "</pre><p>To use the device as a simple image scanner, you can instruct it to process the image as a generic image.</p><pre class=\"brush:csharp\">" +
            @"r.ForceDocumentProcessing(""Generic Image Capture"", """");" +
            "</pre><p>The image, for some reason, is returned as a string.</p><pre class=\"brush:csharp\">" +
            @"string s = r.RetrieveImageItem(""Visible"", “BMP”); // or JPG, etc." +
            "</pre><p>Alternatively, the image can be saved to disk straight away.</p><pre class=\"brush:csharp\">" +
            @"r.RetrieveImageItemAndSave(""Visible"", “JPG”, @""C:\scan.jpg"");" +
            "</pre><p>Now, when you want to use this device for automatic retrieval of data from various documents, things become more interesting. Apparently, the list of documents that can be recognised, is stored on the device somewhere. Here is how this can be accessed:</p><pre class=\"brush:csharp\">" +
            @"r.RetrieveDatabaseList("""");<br />string dv = string.Empty;<br />for (int i = 0; i < r.DatabaseListCount; i++)<br />{<br /> dv = r.get_DatabaseListValue(i);  <br />}" +
            "</pre><p>The “database list” is the list of types of documents that are available for recognition. It contains values like “2Line44” or “3Line30”. After the type has been selected, you can access the list of more particular documents belonging to this type in a similar way:</p><pre class=\"brush:csharp\">" +
            @"r.RetrieveDocumentItemList(“2Line44”);<br />string lv = string.Empty;<br />for (int i = 0; i < r.DocumentItemListCount; i++)<br />{    <br /> lv = r.get_DocumentItemListValue(i);<br />}" +
            "</pre><p>The ‘document item list’ contains values like “Hungarian_Passport”, “Netherlands_Old_Passport” etc. – a lot of them.</p><p>Now, after a particular document that we are going to try to read is chosen, we can go and access all the information that the scanner is trying to extract from it.</p><pre class=\"brush:csharp\">" +
            @"r.Connect(""D72086"", ""Scanner"", ""ProcessingDone"");<br />r.ForceDocumentProcessing(""2Line44"", ""Australia_Passport"");<br />r.DoScan();<br />string fName = r.RetrieveTextItem(""@mrz_surname"");<br />string lName = r.RetrieveTextItem(""@mrz_givname"");" +
            "</pre><p>If we’re lucky, the strings will contain proper name of the passport owner.</p><p>Now I can go and write a small application that can be used, for example, to add clients to a company database. It will scan the client’s passport and automatically populate fields like name, sex, DOB and a few others.</p><p>Overall, it's a fun device to work with. :-) </p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_15122008_k = "3M Document Reader C# C++ ReaderClass";
        public const string content_15122008_d = "Investigating capabilities of 3M Document Reader";

        //"I'm a stylish developer"
        public const string content_10122008_b = "<p>Only today I realized that it's easy to add my own stylesheet to the blog's template so I don't have to format each post which contains code by adding styles to it.</p><p>Oh well, I guess you should refer to the blog title, that explains it. Just in case you're a bit like me, though:</p>";
        public const string content_10122008_r = "<li>Simply copy the CSS you want to use to the clipboard</li><li>In Blogger, go to Template: Edit HTML. In the large textbox in the middle of the page, scroll down to the beginning of the CSS area (it starts with body { ). Just before that line, paste in the CSS you copied</li><li>Click Save Template</li><p>(courtesty of <a href=\"http://forkd.com/help/blogging_styling\">How do style my recipe on my blog?</a>)</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_10122008_k = "Blogger blog template style css";
        public const string content_10122008_d = "Modifying template styles in my blog";

        //"Small Thing Learned Today"
        public const string content_13112008_b = "<p>Let's say I have some data regarding the buildings and people who built them - one building obviously can have multiple builders.</p>";
        public const string content_13112008_r = "<pre class=\"brush:sql\">" +
            @"BuildingID  BuilderName<br />----------- ----------------<br />1           Paul<br />2           John<br />3           Bob<br />1           George<br />2           Sam<br />3           Fred<br />1           Joe<br />2           Phil" +
            "</pre><p>What I need here, is a report in the following format:</p><pre class=\"brush:sql\">" +
            @"BuildingID  builder_list<br />----------- --------------------------<br />1           George, Joe, Paul<br />2           John, Phil, Sam<br />3           Bob, Fred" +
            "</pre><p>I found a very good guide on the problem:</p><a href=\"http://www.projectdmx.com/tsql/rowconcatenate.aspx\">Concatenating row values in Transact-SQL</a><p>This is the solution I chose from the suggested ones:</p><pre class=\"brush:sql\">" +
            @"WITH CTE (BuildingID, builder_list, builder_name, length)<br /> AS(SELECT BuildingID, CAST( '' AS VARCHAR(8000) ), CAST( '' AS VARCHAR(8000) ), 0<br />    FROM tblBuildings<br />    GROUP BY BuildingID<br />       UNION ALL<br />    SELECT p.BuildingID, CAST(builder_list +<br />    CASE WHEN length = 0 THEN '' ELSE ', ' END + BuilderName AS VARCHAR(8000) ), <br />    CAST(BuilderName AS VARCHAR(8000)), length + 1<br />    FROM CTE c<br />    INNER JOIN tblBuilders p<br />    ON c.BuildingID = p.BuildingID<br />    WHERE p.BuilderName > c.builder_name)<br />  <br />SELECT BuildingID, builder_list<br />FROM (SELECT BuildingID, builder_list,<br /> RANK() OVER (PARTITION BY BuildingID ORDER BY length DESC)<br />FROM CTE) D (BuildingID, builder_list, rank)<br />WHERE rank = 1;" +
            "</pre><p>It looks a bit complex to understand, but does exactly what I need!</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_13112008_k = "SQL row value report concatenate";
        public const string content_13112008_d = "Concatenating row values in Transact-SQL";

        //"Small Thing Learned Today"
        public const string content_05112008_b = "<p>You can actually assign default values to stored procedure parameters. I suspect I knew that sometime, but totally forgotter. All I need to do is declare them like this</p>";
        public const string content_05112008_r = "<pre class=\"brush:sql\">" +
            @"CREATE PROCEDURE [dbo].[prSP_MyStoredProcWithDefaultParameters]<br />(@someID int,@someParam1 int = -1,@someParam2 int = -1,@someParam3 int = -1,<br />@someParam4 int = -1,//...@someParam999 int = -1)" +
            "</pre><p>Now if I have less than 999 'someParameters', I can still call the stored procedure. I don't really care how many parameters are in the list, as long as there is no more than 999 of them.</p><pre class=\"brush:sql\">" +
            @"List<DbParameter> myParams = new List<DbParameter>();<br />List<int> paramList = new List<int>();<br />// add some values to the list<br />foreach (int myInt in paramList)<br />{ <br /> if (paramList.IndexOf(myInt) > 998) <br /> {<br />  break;<br /> }<br /> else<br /> {<br />  myParams.Add(DbManager.CreateInParameter(""someParam"" +<br /> (paramList.IndexOf(myInt) + 1).ToString(), DbType.Int32, myInt)); <br /> }<br />}" +
            "</pre><p>Well, I guess I have to take care to assign the really important parameters of course.</p><p>I can have default values in SQL Server functions too, but, unfortunately I have to specify the keyword 'default' while calling them.</p><p>So, if a function is defined as</p><pre class=\"brush:sql\">" +
            @"CREATE FUNCTION [dbo].[fn_MyFunctionWithDefaultParameters](@someID int,<br />@someParam1 int = -1,@someParam2 int = -1,<br />@someParam3 int = -1,@someParam4 int = -1,<br />//...<br />@someParam999 int = -1)" +
            "</pre><p>I need to call it this way:</p><pre class=\"brush:sql\">" +
            @"dbo.fn_MyFunctionWithDefaultParameters(1, 2, 3, default, default, <br />/* snip a few hundred more*/ <br />default)" +
            "</pre><p>Still quite useful, but I wish I could call it just like this</p><pre class=\"brush:sql\">" +
            @"dbo.fn_MyFunctionWithDefaultParameters(1, 2, 3)" + "</pre><p>or this</p><pre class=\"brush:sql\">" +
            @"dbo.fn_MyFunctionWithDefaultParameters(1, 2, 3, default, 5, 6)</pre><br/>" + "by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_05112008_k = "SQL Server stored procedure parameter default value";
        public const string content_05112008_d = "Assigning default values to stored procedure parameters";

        //"Offer Accepted"
        public const string content_28102008_b = "<p>Finally got an offer from that company I mentioned before - the one that everyone knows about.</p>";
        public const string content_28102008_r = "<p> And accepted, too. Looking forward to it. The company is huge, though it is not a software development or a consulting company. The development team is about 30 people and only develops software for company's internal needs. I start in 4 weeks.</p><p>Oh well, enough about it for now I guess.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_28102008_k = "C# Work";
        public const string content_28102008_d = "I'm chaning jobs";

        //"A Quick One Before I Forget"
        public const string content_25102008_b = "<p>Cause it's Friday.</p><p>I came across a very good list of tips and tricks that every developer who uses Visual Studio 2008 should know:</p>";
        public const string content_25102008_r = "<p><a href=\"http://weblogs.asp.net/stephenwalther/archive/2008/10/21/essential-visual-studio-tips-amp-tricks-that-every-developer-should-know.aspx\">Essential Visual Studio Tips & Tricks that Every Developer Should Know</A>.</p><p>Ones I did not know are: 1, 4, 7, 10.</p>";
        public const string content_25102008_k = "Visual studio 2008 tips tricks";
        public const string content_25102008_d = "A list of tips and tricks for a Visual Studio 2008 developer";

        //"Small Thing Learned Today"
        public const string content_23102008_b = "<p>I needed to hide some of the rows of a databound DataGridView at runtime. However, when I added a piece of code to do that,</p>";
        public const string content_23102008_r = "<pre class=\"brush:csharp\">" +
            @"foreach (DataGridViewRow row in myDataGridView.Rows)<br />{ <br />   if (someCondition) <br />   {<br />      row.Visible = false; <br />   }<br />}" + "</pre><p>I was getting an exception sometimes:</p><p>'Row associated with the currency manager's position cannot be made invisible.'</p><p>I found out that the exception was happening when the row I was trying to set invisible was selected. The solution to this little problem is to change the code the following way:</p><pre class=\"brush:csharp\">" +
            @"CurrencyManager currencyManager1 = (CurrencyManager)<br />BindingContext[myDataGridView.DataSource];<br />currencyManager1.SuspendBinding();<br />foreach (DataGridViewRow row in myDataGridView.Rows)<br />{ <br />   if (someCondition) <br />   {<br />      row.Visible = false; <br />   }<br />}<br />myBindingSource.ResumeBinding();  // this is myDataGridView's binding source" + "</pre><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_23102008_k = "DataGridViewRow visible";
        public const string content_23102008_d = "How to hide data table rows";

        //"Small Thing Learned Today"
        public const string content_20102008_b = "<p>So I needed to show a type name in the textbox today. Like 'DateTime', or 'Integer' etc. I decided to create a binding source and bind to System.Type.Name like this:</p>";
        public const string content_20102008_r = "<pre class=\"brush:csharp\">" +
            @"this.propertyTypeBindingSource.DataSource = typeof(System.Type);<br />/* snip */<br />this.nameTextBox.DataBindings.Add(new System.Windows.Forms.Binding<br />(""Text"", this.propertyTypeBindingSource, ""Name"", true));<br />/* snip */<br />Type PropertyType = typeof(DateTime);<br />this.propertyTypeBindingSource.DataSource = PropertyType;" +
            "</pre><p>However, when I try to run the application, I get an exception<p><p>\"Cannot bind to the property or column Name on the DataSource. Parameter name: dataMember\"</p><p>So, looks like I'm not allowed to bind directly to System.Type. Maybe I have to do some simple trick ... I create a class</p><pre class=\"brush:csharp\">" +
            @"public class StubPropertyType<br />{ <br /> public StubPropertyType(Type type) <br /> {<br />  this.StubPropertyTypeName = type.Name;<br /> }<br /> private string _stubPropertyTypeName = string.Empty;<br /> public string StubPropertyTypeName <br /> {<br />  get { return _stubPropertyTypeName; }  <br />  set { _stubPropertyTypeName = value; } <br /> }<br />}" +
            "</pre><p>and my binding now looks along these lines:</p><pre class=\"brush:csharp\">" +
            @"this.propertyStubBindingSource.DataSource = typeof(StubPropertyType);<br />/* snip */<br />this.nameTextBox.DataBindings.Add(new System.Windows.Forms.Binding<br />(""Text"", this.propertyStubBindingSource, ""StubPropertyTypeName"", true));<br />/* snip */<br />Type PropertyType = typeof(DateTime);<br />StubPropertyType stub = new StubPropertyType(PropertyType);<br />this.propertyStubBindingSource.DataSource = stub;" +
            "</pre><p>And it works like a charm!</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_20102008_k = "Bind directly system.type";
        public const string content_20102008_d = "Cannot bind to the property or column Name on the DataSource. Parameter name: dataMember";

        //"Small Thing Learned Today"
        public const string content_15102008_b = "<p>In SQL Server 2005, if I need to know what are the columns and data types of the certain table, I can run a simple query to know it:</p>";
        public const string content_15102008_r = "<pre class=\"brush:csharp\">" +
            @"select * from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME='MyTable'" + "</pre><p>What's especially good for me is that it not only works with tables, but with views too. My current task is to create an interface for users which would allow them to generate custom reports through a set of views. They need to be able to apply criteria to the reports so that if the column's data type is datetime, they can use 'Is Before' or 'Is After', but on integer columns they can use 'Larger Than' etc.<p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_15102008_k = "SQL Server column data type";
        public const string content_15102008_d = "How to find out SQL Server columns and datatypes";

        //"Very Busy This Week"
        public const string content_14102008_b = "<p>On Friday I will have to go for an 'online test'. That's it, no more details. I've been for a 'first round' interview, and after about a week or so I got a call and was invited to this testing. I have no idea what the testing will be about, but I studied everything relevant on <a href=\"http://www.techinterviews.com\"> Tech Interviews</a> and even registered with <a href=\"http://www.brainbench.com\">Brainbench</a> cause they have a free C# test there. It's a bit hard to prepare when you don't know what you're preparing for. Well it's a C# development position, so this gives me sort of an idea.</p>";
        public const string content_14102008_r = "<br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_14102008_k = "Random thought";
        public const string content_14102008_d = "Preparing for a test";

        //"Stack Overflow!"
        public const string content_09102008_b = "<p><a href=\"http://stackoverflow.com/\">This site</a></p><p>seems to be very useful for those seeking answers for their computer-related questions. I fould a link to it <a href=\"http://www.joelonsoftware.com/items/2008/09/15.html\">here</a>.</p>";
        public const string content_09102008_r = "<p>This is how it works:</p><blockquote><br />Every question in Stack Overflow is like the Wikipedia article for some extremely narrow, specific programming question. How do I enlarge a fizzbar without overwriting the user’s snibbit? This question should only appear once in the site. Duplicates should be cleaned up quickly and redirected to the original question. <br /><br />Some people propose answers. Others vote on those answers. If you see the right answer, vote it up. If an answer is obviously wrong (or inferior in some way), you vote it down. Very quickly, the best answers bubble to the top. The person who asked the question in the first place also has the ability to designate one answer as the “accepted” answer, but this isn’t required. The accepted answer floats above all the other answers.</blockquote><br /><p><a href=\"http://www.joelonsoftware.com/items/2008/09/15.html\">Read more</a>.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_09102008_k = "programming questions answers";
        public const string content_09102008_d = "Stack overflow launched";

        //"Browsing The Job Listings"
        public const string content_07102008_b = "<p>Is there even such thing as a 'Junior Project Manager'? The title caught my eye as I never seen anything like this before. And the salary on offer is even significantly less than I have as a developer at the moment ...</p>";
        public const string content_07102008_r = "<br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_07102008_k = "junior project manager";
        public const string content_07102008_d = "Random thought";

        //"Snippets"
        public const string content_06102008_b = "<p>You know how in Visual Studio you can type 'mbox', press Tab twice and Visual Studio will convert it into</p>";
        public const string content_06102008_r = "<pre class=\"brush:csharp\">" + @"MessageBox.Show(""Test"");" + "</pre><p>Or, you can type 'ctor', press Tab twice and the constructor for the class will be generated? </p><p>This can be customised. Have a look at the following file:</p><pre class=\"brush:csharp\">" +
            @"&lt;?xml version=""1.0"" encoding=""utf-8"" ?&gt;<br />&lt;CodeSnippets  xmlns=""http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet""&gt;<br /> &lt;CodeSnippet Format=""1.0.0""&gt;<br />  &lt;Header&gt;<br />  &lt;Title&gt;props&lt;/Title&gt;<br />  &lt;Shortcut&gt;props&lt;/Shortcut&gt;<br />  &lt;Description&gt;Code snippet that checks for a null property&lt;/Description&gt;<br />  &lt;SnippetTypes&gt;<br />   &lt;SnippetType&gt;Expansion&lt;/SnippetType&gt;<br />  &lt;/SnippetTypes&gt;<br />  &lt;/Header&gt;<br />  &lt;Snippet&gt;<br />   &lt;Declarations&gt;<br />    &lt;Literal&gt;<br />     &lt;ID&gt;field&lt;/ID&gt;<br />     &lt;ToolTip&gt;backing store&lt;/ToolTip&gt;<br />     &lt;Default&gt;mProp&lt;/Default&gt;<br />    &lt;/Literal&gt;    <br />    &lt;Literal&gt;<br />     &lt;ID&gt;type&lt;/ID&gt;<br />     &lt;ToolTip&gt;Property type&lt;/ToolTip&gt;<br />     &lt;Default&gt;int&lt;/Default&gt;<br />    &lt;/Literal&gt;<br />    &lt;Literal&gt;<br />     &lt;ID&gt;property&lt;/ID&gt;<br />     &lt;ToolTip&gt;Property name&lt;/ToolTip&gt;<br />     &lt;Default&gt;MyProperty&lt;/Default&gt;<br />    &lt;/Literal&gt;<br />   &lt;/Declarations&gt;<br />   &lt;Code Language=""csharp""&gt;<br />   &lt;![CDATA[private $type$ $field$;   <br />   public $type$ $property$    <br />   {     <br />    get<br />    {<br />     if (this.$field$ == null)     <br />     {      <br />      this.$field$ = new $type$();<br />     }     <br />     return this.$field$;    <br />    }<br />    set {this.$field$ = value;}    <br />   }<br />   $end$]]&gt;   <br />   &lt;/Code&gt;<br />  &lt;/Snippet&gt; <br /> &lt;/CodeSnippet&gt;<br />&lt;/CodeSnippets&gt;<br />" +
            "</pre><p>You can now save it with any name you like, and then press Ctrl-K, B (or choose Tools -> Code snippets manager), press 'Import', navigate to this file and select it.</p><p>Now, if you type 'props' and press Tab twice, the system will convert it into the following little pattern for getting a private member through a property, with checking if it is null:</p><pre class=\"brush:csharp\">" + @"private int mProp;<br />public int MyProperty <br />{<br /> get  <br /> {<br />  if (this.mProp == null)  <br />  {   <br />   this.mProp = new int();   <br />  }<br />  return this.mProp; <br /> }<br /> set {this.mProp = value;} <br />}" +
            "</pre><p>From here it's easy to understand how to make 'snippets' that do anything you want.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_06102008_k = "Code development Visual Studio snippets";
        public const string content_06102008_d = "About using snippets with Visual Studio";

        //"Interview Question Of The Day"
        public const string content_05102008_b = "<p>The easiest of those I did not answer today was 'What is the difference between a class and a struct?'.</p>";
        public const string content_05102008_r = "<p>Yes, everyone should know it. For explanation why I did not, refer to the title of this blog. Later I found quite a detailed answer here:</p><p><a href=\"http://www.dotnetspider.com/resources/740-Difference-between-class-struct-C.aspx\">Difference between class and struct in C#</a></p><p>I knew some of these, but did not realize there are that many. Some can be combined though, for example \"When you instantiate a class, it will be allocated on the heap.When you instantiate a struct, it gets created on the stack\" is obvious if you already know that \"Classes are reference types and structs are value types\".</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_05102008_k = "programming interview class struct difference";
        public const string content_05102008_d = "Small Story From The Interview";

        //"A Well-known Interview Tip"
        public const string content_04102008_b = "<p>Everyone knows this one. You should research the company you're going to for an interview - what it does, what are their goals, who their main competitors are etc.</p>";
        public const string content_04102008_r = "<p>Sometimes, however, you have a luxury of skipping this step. If a company is so huge or well-known that literally EVERYONE knows it, I think it's pretty safe to skip it.</p><p>I was at such company last week, maybe for the first time in my life. No, they didn't ask me 'so, what do you know about us?' :)</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_04102008_k = "Interview tip";
        public const string content_04102008_d = "Random thoughts";

        //"Exciting Day At Work"
        public const string content_02102008_b = "<p>Implementing permissions in the application.The database structure is there. There is \"Permissions\" table, there is \"Roles\" table, there is \"RolesPermissions\" table. There is an interface that allows to create additional roles and add different permissions to them. So, what is the exciting thing that's not yet implemented?</p>";
        public const string content_02102008_r = "<p>I have to go through the code and insert pieces of code that actually check user's permissions and look like this:</p><pre class=\"brush:csharp\">" +
            @"if (CurrentUser.IsAllowed(Permissions.MyPermissionToSeeSomeHighlySensitiveData))<br />{ <br /> //existing code remains here<br />}<br />else<br />{ <br /> MessageBox(""GoAway"", ""You have no permission to do that"");<br />}" + "</pre><p>There are about 130 separate permissions and about 190 places in the application where permissions are checked ... what an exciting way to spend Friday.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_02102008_k = "work";
        public const string content_02102008_d = "Stuff that happens at work";

        //"Now Reading"
        public const string content_01102008_b = " <p><a href=\"http://www.amazon.com/Peopleware-Productive-Projects-Teams-Second/dp/0932633439/ref=sr_1_1?ie=UTF8&s=books&qid=1232267243&sr=1-1\">\"Peopleware: Productive Projects and Teams (Second Edition)\"</a></p>";
        public const string content_01102008_r = "<p>Great book. I won't try writing a review since there's plenty of them on the Internet. It's probably the most interesting book on managing software projects since</p><p><a href=\"http://www.amazon.com/Mythical-Man-Month-Software-Engineering-Anniversary/dp/0201835959/ref=sr_1_1?ie=UTF8&s=books&qid=1232267258&sr=1-1\">\"The Mythical Man-Month: Essays on Software Engineering\"</a></p><p>(I don''t read much on the subject anyway, so I could have missed a few dozens of good books).</p><p>Well after doing some reading, I learned a couple of things: my workspace is designed in the most counter-productive way possible and the project I am currently working on has all chances of joining those 25% of software products that are never actually used.</p><p style=\"color:#666666;\">Mood: insanely optimistic.</p><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_01102008_k = "Books reading peopleware mythical man month";
        public const string content_01102008_d = "About some of the books I read";

        //"My Favourite Anonymous Delegate"
        public const string content_30092008_b = "<p>Just to test out the editing capabilities here, I'll display a bit of code I use often.<br />Let's say I have a business entity class to keep some data.</p>";
        public const string content_30092008_r = "<pre class=\"brush:csharp\">" + @"<br />public class MyBusinessEntity <br />{<br />     public string MyProperty = string.Empty; <br />}" +
            "</pre><p>I keep these business entities in a List</p><pre class=\"brush:csharp\">" +
            @"<br />List listToSearch = new List();<br />// fill the list with actual data" +
            "</pre><p>I need to select all business entities that satisfy to a certain criteria.</p><pre class=\"brush:csharp\">" +
            @"List listIFound = <br />listToSearch.FindAll(delegate(MyBusinessEntity entity)<br />{<br />     return (entity.MyProperty == 'myTestString'); <br />});" +
            "</pre><p>listIFound will now contain all instances of MyBusinessEntity from listToSearch where MyProperty equals 'myTestString'.</p>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_30092008_k = "C# anonymous delegate";
        public const string content_30092008_d = "A simple C# anonymous delegate";

        //"Good Deed For The Day"
        public const string content_29092008_b = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2008/29092008_tick.jpg\" alt=\"Green Tick\" /></div>";
        public const string content_29092008_r = "by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_29092008_k = "Test post";
        public const string content_29092008_d = "Fist, test post";

        //"WebGrid: AJAX Updates, Server Sorting"
        public const string content_26112012_b = "<p>This is a brief summary of the changes I did to implement the AJAX updates to the <strong>WebGrid</strong> and sorting behaviour. I plan to put more detailed notes and the source code on <a href=\"http://www.ynegve.info/Yahoo/Theory/\">my website</a></p>";
        public const string content_26112012_r = "<p>To use AJAX and update grid content, firstly the grid needs to be placed in the div which has an id. The <strong>ajaxUpdateContainerId</strong> has to be specified in the <strong>WebGrid</strong> declaration. To put it simple, in my main view I have a div</p><pre class=\"brush: xml\">" +
            @"&lt;div id='wbgrid' style='float:left; min-width:500px;'&gt;
             @Html.Partial('_WebGrid', Model)
            &lt;/div&gt;" +
            "</pre><p>And in the partial view the div name <strong>wbgrid</strong> is specified as <strong>ajaxUpdateContainerId</strong>.</p><pre class=\"brush: csharp\">" +
            @"@{ var grid = new WebGrid&lt;Recipes.Models.Yahoo.YahooData&gt;(null, rowsPerPage: 5, defaultSort: 'YahooSymbolName', ajaxUpdateContainerId: 'wbgrid');
            grid.Bind(Model.Datas, rowCount: Model.TotalRows, autoSortAndPage: false);
            }" + "</pre><p>The link on the <strong>WebGrid</strong> column has the following format <u>http://localhost/Yahoo/Index?sort=DateTime&sortdir=ASC</u></p><p>Therefore, the controller function can automatically receive those parameters with the following signature:</p><pre class=\"brush: csharp\">" +
            @"public ActionResult Index(int page = 1, string sort = 'YahooSymbolName', string sortDir = 'Ascending')" +
            "</pre><p>The parameters will be then passed over to the function that retrieves data</p><pre class=\"brush: csharp\">" +
            @"public List&lt;YahooData&gt; GetData(out int totalRecords, int pageSize, int pageIndex, string sort = 'YahooSymbolName', SortDirection sortOrder = SortDirection.Ascending )
            {
             IQueryable&lt;YahooData&gt; data = db.YahooData;
             totalRecords = data.Count();

             Func&lt;IQueryable&lt;YahooData&gt;, bool, IOrderedQueryable&lt;YahooData&gt;&gt; applyOrdering = _dataOrderings[sort];
             data = applyOrdering(data, sortOrder == SortDirection.Ascending);

             List&lt;YahooData&gt; result = data.ToList();
             if(pageSize &gt; 0 && pageIndex &gt;=0)
             {
              result = result.Skip(pageIndex*pageSize).Take(pageSize).ToList();
             }
             return result;
            }" + "</pre><p>A couple of helper functions are utilized by GetData: <strong>CreateOrderingFunc</strong> and <strong>_dataOrderings</strong></p><pre class=\"brush: csharp\">" +
            @"// helpers that take an IQueryable&lt;Product&gt; and a bool to indicate ascending/descending
            // and apply that ordering to the IQueryable and return the result
            private readonly IDictionary&lt;string, Func&lt;IQueryable&lt;YahooData&gt;, bool, IOrderedQueryable&lt;YahooData&gt;&gt;&gt;
             _dataOrderings = new Dictionary&lt;string, Func&lt;IQueryable&lt;YahooData&gt;, bool, IOrderedQueryable&lt;YahooData&gt;&gt;&gt;
                   {
                    {'YahooSymbolName', CreateOrderingFunc&lt;YahooData, string&gt;(p=&gt;p.DataName)},
                    {'Ask', CreateOrderingFunc&lt;YahooData, decimal?&gt;(p=&gt;p.Ask)},
                    {'Time', CreateOrderingFunc&lt;YahooData, DateTime&gt;(p=&gt;p.DateTime)}
                    // Add for more columns ...
                   };

            /// returns a Func that takes an IQueryable and a bool, and sorts the IQueryable (ascending or descending based on the bool).
            /// The sort is performed on the property identified by the key selector.
            private static Func&lt;IQueryable&lt;T&gt;, bool, IOrderedQueryable&lt;T&gt;&gt; CreateOrderingFunc&lt;T, TKey&gt;(Expression&lt;Func&lt;T, TKey&gt;&gt; keySelector)
            {
             return
              (source, ascending) =&gt; ascending ? source.OrderBy(keySelector) : source.OrderByDescending(keySelector);
            }" +
            "</pre><p>Finally, to complete the functional example, I added a <strong>jQuery</strong> dialog that displays the data that was retrieved from Yahoo. In the view, the <strong>RetrieveData</strong> function triggers the controller action <strong>AddDataToDB</strong> (which calls the Yahoo website and adds results to the database).</p><pre class=\"brush: js\">" +
            @"function RetrieveData() {
             $.post('@Url.Action('AddDataToDB','yahoo')',
             function (d) {
              ShowDialog(d.o);
             });
            }

            function ShowDialog(msg) {
             $('&lt;div/&gt;').dialog({ title: 'Retrieved the following data', width: 450, height: 250, close: function(){ location.reload(); }}).html(msg);
            }" +
            "</pre><p><strong>AddDataToDB</strong> returns a <strong>Json</strong> result, containing the html table.</p><pre class=\"brush: csharp\">" +
            @"public ActionResult AddDataToDB()
            {
             List&lt;YahooData&gt; datas = GetSingleSet();
             datas.ForEach(d =&gt; db.YahooData.Add(d));
             db.SaveChanges();

             string s = ""&lt;table&gt;&lt;thead&gt;&lt;tr class=\""webgrid-header\""&gt;&lt;th&gt;Company&lt;/th&gt;&lt;th&gt;Time&lt;/th&gt;&lt;th&gt;LTP&lt;/th&gt;&lt;th&gt;Volume&lt;/th&gt;&lt;th&gt;Ask&lt;/th&gt;&lt;th&gt;Bid&lt;/th&gt;&lt;th&gt;High&lt;/th&gt;&lt;th&gt;Low&lt;/th&gt;&lt;/tr&gt;&lt;/thead&gt;&lt;tbody&gt;"";

             foreach (var yahooData in datas)
             {
              s = s + ""&lt;tr class=\""webgrid-row-style\""&gt;"" + 
               ""&lt;td class=\""company\""&gt;"" + yahooData.DataName + ""&lt;/td&gt;"" +
               ""&lt;td class=\""time\""&gt;"" + yahooData.DateTime.ToString(""dd/MM/yyyy hh:mm"") + ""&lt;/td&gt;"" +
               ""&lt;td class=\""ask\""&gt;"" + yahooData.LTP + ""&lt;/td&gt;"" +
               ""&lt;td class=\""volume\""&gt;"" + yahooData.Volume + ""&lt;/td&gt;"" +
               ""&lt;td class=\""ask\""&gt;"" + yahooData.Ask + ""&lt;/td&gt;"" +
               ""&lt;td class=\""ask\""&gt;"" + yahooData.Bid + ""&lt;/td&gt;"" +
               ""&lt;td class=\""ask\""&gt;"" + yahooData.High + ""&lt;/td&gt;"" +
               ""&lt;td class=\""ask\""&gt;"" + yahooData.Low + ""&lt;/td&gt;"" +
               ""&lt;/tr&gt;"";
             }

             s = s + ""&lt;/tbody&gt;&lt;/table&gt;"";

             return Json(new { o = s });
            }" +
            "</pre><p>The result is then utilised by the <strong>ShowDialog</strong> function, that displays a <strong>jQuery</strong> dialog. When the user closes the dialog, the page is refreshed so that the <strong>WebGrid</strong> contents are updated with the latest data retrieved.</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/26112012_Complete_Example.png\" alt=\"Complete WebGrid Example\" /></div><p align=\"center\">Complete example</p><p><strong>References</strong></p><a href=\"http://msdn.microsoft.com/en-us/magazine/hh288075.aspx\">Get the Most out of WebGrid in ASP.NET MVC</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_26112012_k = "MVC C# WebGrid AJAX partial view sorting";
        public const string content_26112012_d = "More advanced usage of WebGrid control, now with AJAX";

        //"MVC and SEO basics: inject title, keywords and description"
        public const string content_28112012_b = "<p>Almost by accident, I came across Google's starter guide for SEO optimisation and decided that it is a good idea to make some improvements I've been neglecting. Here's what I did so far and how I applied it to the MVC framework.</p>";
        public const string content_28112012_r = "<p><strong>1. Create unique, accurate page titles</strong></p><p>One way to do it with the MVC framework is to create a placeholder on the master page and then override it on the view page.</p><p><u>Master:</u></p><pre class=\"brush: xml\">" +
            @"<asp:ContentPlaceHolder id=""init"" runat=""server""></asp:ContentPlaceHolder>
            <head runat=""server"">    
                <asp:ContentPlaceHolder ID=""title"" runat=""server"">
                    <title><%=this.Page.Title%></title>
                </asp:ContentPlaceHolder>
            </head>" + "</pre><p><u>View:</u></p><pre class=\"brush: xml\">" +
            @"<asp:Content ID=""Content1"" ContentPlaceHolderID=""title"" runat=""server"">
                   <title>Home Page</title>
            </asp:Content>" +
            "</pre><p>For now, I chose the easier approach to set the title in the <strong>_Layout.cshtml</strong></p><pre class=\"brush: xml\">" +
            @"<title>@ViewBag.Title</title>" + "</pre><p>And assign it in each view separately</p><pre class=\"brush: csharp\">" +
            @"@{
                ViewBag.Title = ""Main Page - The Stepping Stone Markov Chain Algorithm - MVC Stepping Stone Example"";
            }" + "</pre><p><strong>2. Make use of the \"description\" and \"keywords\" meta tags</strong></p><p>This takes a little more work. Here's my chosen approach: First, make sure each controller inherits from the <strong>BaseController</strong>. Then create two new classes, <strong>MetaDescriptionAttribute</strong> and <strong>MetaKeywordsAttribute</strong>, and inherit them from <strong>System.Attribute</strong></p><pre class=\"brush: csharp\">" +
            @"public class MetaDescriptionAttribute : Attribute
            {
             private readonly string _parameter;

             public MetaDescriptionAttribute(string parameter)
             {
              _parameter = parameter;
             }

             public string Parameter { get { return _parameter; } }
            }

            public class MetaKeywordsAttribute : Attribute
            {
             private readonly string _parameter;

             public MetaKeywordsAttribute(string parameter)
             {
              _parameter = parameter;
             }

             public string Parameter { get { return _parameter; } }
            }" +
            "</pre><p>In <strong>BaseController</strong>, override <strong>OnActionExecuting</strong></p><pre class=\"brush: csharp\">" +
            @"protected override void OnActionExecuting(ActionExecutingContext filterContext)
            {
             var keywords = filterContext.ActionDescriptor.GetCustomAttributes(typeof(MetaKeywordsAttribute), false);
             if (keywords.Length == 1)
              ViewData[""MetaKeywords""] = ((MetaKeywordsAttribute)(keywords[0])).Parameter;

             var description = filterContext.ActionDescriptor.GetCustomAttributes(typeof(MetaDescriptionAttribute), false);
             if (description.Length == 1)
              ViewData[""MetaDescription""] = ((MetaDescriptionAttribute)(description[0])).Parameter;

             base.OnActionExecuting(filterContext);
            }" + "</pre><p>Decorate the appropriate controller method with newly created attributes</p><pre class=\"brush: csharp\">" +
            @"[MetaKeywords(""C#, MVC, Markov Chain, Stepping Stone, Programming"")]
            [MetaDescription(""Stepping Stone Markov Chain model is an example that has been used in the study of genetics. In this model we have an n-by-n array of squares, and each square is initially any one of k different colors. For each step, a square is chosen at random. This square then chooses one of its eight neighbors at random and assumes the color of that neighbor"")]
            public ActionResult Index()
            {
             SteppingStoneHelpers.CreateNewTable();
             HtmlString table = new HtmlString(SteppingStoneHelpers.table.ToString());
             return View(table);
            }" + "</pre><p>Finally, in the <strong>_Layout.cshtml</strong>, add the following</p><pre class=\"brush: xml\">" +
            @"<meta charset=""utf-8"" name=""keywords"" value=""@ViewBag.MetaKeywords"" />
            <meta charset=""utf-8"" name=""description"" value=""@ViewBag.MetaDescription"" />" +
            "</pre><p>All done! There we go, the resulting html:</p><pre class=\"brush: xml\">" +
            @"<!DOCTYPE html>
            <html>
            <head>
                <meta charset=""utf-8"" name=""keywords"" value=""C#, MVC, Markov Chain, Stepping Stone, Programming"" />
                <meta charset=""utf-8"" name=""description"" value=""Stepping Stone Markov Chain model is an example that has been used in the study of genetics. In this model we have an n-by-n array of squares, and each square is initially any one of k different colors. For each step, a square is chosen at random. This square then chooses one of its eight neighbors at random and assumes the color of that neighbor"" />
                <title>Main Page - The Stepping Stone Markov Chain Algorithm - MVC Stepping Stone Example</title>
                <link href=""/Content/Site.css"" rel=""stylesheet"" type=""text/css"" />
                <script src=""/Scripts/jquery-1.8.0.min.js"" type=""text/javascript""></script>" +
            "</pre><p><strong>References:</strong></p><a href=\"http://googlewebmastercentral.blogspot.ch/2010/09/seo-starter-guide-updated.html\">Google Starter Guide</a><br/><a href=\"http://stackoverflow.com/questions/326628/asp-net-mvc-view-with-master-page-how-to-set-title\">ASP.NET MVC - View with master page, how to set title?</a><br/><a href=\"http://stackoverflow.com/questions/4263568/asp-net-mvc-strategy-for-including-seo-information-such-as-meta-keywords-and-d\">asp.net mvc - strategy for including SEO information such as meta keywords and descriptions</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_28112012_k = "MVC C# SEO meta keywords description";
        public const string content_28112012_d = "How to inject meta keywords and description into the MVC views";

        //"WebGrid: Stronly Typed with Server Paging"
        public const string content_25112012_b = "<p>Continuing with the <strong></strong>WebGrid, I first made it strongly typed. To achieve that, I created a derived type <strong></strong>WebGrid<T>. The source came from the reference at the end of the post, and the code I used is displayed below too. While it does not seem to change much in terms of functionality, the main advantage is that the IntelliSense and compiler checking will work with the grid now.</p>";
        public const string content_25112012_r = "<p>Next I added server paging. Why wouldn't I use the built-in paging? Well, the database table behind WebGrid may have hundreds of records. I wouldn't want to pass it all to my <strong>ViewModel</strong> and then to the view just to display 5 or 10 of the records I actually need. It is handy that the WebGrid paging is in the form of <strong>http://website/Items/ShowAll?page=3</strong>. This way my controller knows which page is to be displayed and can preselect the data just for this page only.</p><p>To implement paging, I added the TotalRows to the model - that will specify the total number of records in the database table.</p><p>The controller method now looks as follows:</p><pre class=\"brush: csharp\">" +
            @"public ActionResult Index(int page=1)
            {
             int totalRecords;
             List&lt;YahooData&gt; datas = GetData(out totalRecords, pageSize: 5, pageIndex: page - 1);
             List&lt;YahooSymbol&gt; symbols = db.YahooSymbols.ToList();
             YahooSymbol symbol = symbols.First();
             int id = symbol.YahooSymbolID;
             return View(new YahooViewModel(id, symbol, symbols, datas, totalRecords));
            }

            public List&lt;YahooData&gt; GetData(out int totalRecords, int pageSize, int pageIndex)
            {
             List&lt;YahooData&gt; data = GetData();
             totalRecords = data.Count;
             if(pageSize &gt; 0 && pageIndex &gt;=0)
             {
              data = data.Skip(pageIndex*pageSize).Take(pageSize).ToList();
             }
             return data.ToList();
            }" +
            "</pre><p>The concept is quite simple - get data from the database table, identify the records that will be displayed on the WebGrid page that is requested, and only pass those records to the view. The WebGrid part of the view now looks as follows</p><pre class=\"brush: csharp\">" +
            @"&lt;div id=""webgrid"" style=""float:left; min-width:500px;""&gt;
             @{ var grid = new WebGrid&lt;ViewModels.YahooData&gt;(null, rowsPerPage: 5, defaultSort: ""YahooSymbolName"");
                grid.Bind(Model.Datas, rowCount: Model.TotalRows, autoSortAndPage: false);
                @grid.GetHtml(columns: grid.Columns( 
                grid.Column(""DataName"", header:""Company"", format:@&lt;text&gt;@Html.ActionLink((string)item.DataName, ""Details"", ""Company"", new {id=item.SymbolId}, null)&lt;/text&gt;),
                grid.Column(""DateTime"", header:""Time"", style:""time"", format:@&lt;text&gt;@item.DateTime.ToString(""dd/MM/yyyy hh:mm"")&lt;/text&gt;), 
                grid.Column(""LTP""), grid.Column(""Volume""), grid.Column(""Ask""), grid.Column(""Bid""), grid.Column(""High""), grid.Column(""Low"")),
                tableStyle: ""webGrid"", headerStyle: ""header"", alternatingRowStyle: ""alt"");
              }
            &lt;/div&gt;" +
            "</pre><p>My plan from here is to implement AJAX updates to the WebGrid content.</p><p>The strongly typed WebGrid samples:</p><pre class=\"brush: csharp\">" +
            @"//Strongly Typed WebGrid
            public class WebGrid&lt;T&gt; : WebGrid
            {
             // Wrapper for System.Web.Helpers.WebGrid that preserves the item type from the data source
             public WebGrid(IEnumerable&lt;T&gt; source = null, IEnumerable&lt;string&gt; columnNames = null, string defaultSort = null, int rowsPerPage = 10, bool canPage = true, bool canSort = true, string ajaxUpdateContainerId = null, string ajaxUpdateCallback = null, string fieldNamePrefix = null, string pageFieldName = null, string selectionFieldName = null, string sortFieldName = null, string sortDirectionFieldName = null)
              : base(source.SafeCast&lt;object&gt;(), columnNames, defaultSort, rowsPerPage, canPage, canSort, ajaxUpdateContainerId, ajaxUpdateCallback, fieldNamePrefix, pageFieldName, selectionFieldName, sortFieldName, sortDirectionFieldName)
             {
             }
             public WebGridColumn Column(string columnName = null, string header = null, Func&lt;T, object&gt; format = null, string style = null, bool canSort = true)
             {
              Func&lt;dynamic, object&gt; wrappedFormat = null;
              if (format != null)
              {
               wrappedFormat = o =&gt; format((T)o.Value);
              }
              WebGridColumn column = base.Column(columnName, header, wrappedFormat, style, canSort);
              return column;
             }
             public WebGrid&lt;T&gt; Bind(IEnumerable&lt;T&gt; source, IEnumerable&lt;string&gt; columnNames = null, bool autoSortAndPage = true, int rowCount = -1)
             {
              base.Bind(source.SafeCast&lt;object&gt;(), columnNames, autoSortAndPage, rowCount);
              return this;
             }
            }

            public static class EnumerableExtensions
            {
             public static IEnumerable&lt;TTarget&gt; SafeCast&lt;TTarget&gt;(this IEnumerable source)
             {
              return source == null ? null : source.Cast&lt;TTarget&gt;();
             }
            }" + "</pre><pre class=\"brush: csharp\">" +
            @"//WebGrid extensions
            public static class WebGridExtensions
            {
             // Light-weight wrapper around the constructor for WebGrid so that we get take advantage of compiler type inference
             public static WebGrid&lt;T&gt; Grid&lt;T&gt;(this HtmlHelper htmlHelper, IEnumerable&lt;T&gt; source, IEnumerable&lt;string&gt; columnNames = null,
               string defaultSort = null, int rowsPerPage = 10, bool canPage = true, bool canSort = true,
               string ajaxUpdateContainerId = null, string ajaxUpdateCallback = null, string fieldNamePrefix = null,
               string pageFieldName = null, string selectionFieldName = null, string sortFieldName = null, string sortDirectionFieldName = null)
             {
              return new WebGrid&lt;T&gt;(source, columnNames, defaultSort, rowsPerPage,
                canPage, canSort, ajaxUpdateContainerId, ajaxUpdateCallback, fieldNamePrefix, 
                pageFieldName, selectionFieldName, sortFieldName, sortDirectionFieldName);
             }

             // Light-weight wrapper around the constructor for WebGrid so that we get take advantage of compiler type inference and to automatically call Bind to disable the automatic paging and sorting (use this for server-side paging)
             public static WebGrid&lt;T&gt; ServerPagedGrid&lt;T&gt;(this HtmlHelper htmlHelper, IEnumerable&lt;T&gt; source, int totalRows, IEnumerable&lt;string&gt; columnNames = null,
               string defaultSort = null, int rowsPerPage = 10, bool canPage = true, bool canSort = true, string ajaxUpdateContainerId = null, 
               string ajaxUpdateCallback = null, string fieldNamePrefix = null,
               string pageFieldName = null, string selectionFieldName = null, string sortFieldName = null, string sortDirectionFieldName = null)
             {
              var webGrid = new WebGrid&lt;T&gt;(null, columnNames, defaultSort, rowsPerPage, canPage,
                canSort, ajaxUpdateContainerId, ajaxUpdateCallback, fieldNamePrefix,
                pageFieldName, selectionFieldName, sortFieldName, sortDirectionFieldName);
              return webGrid.Bind(source, rowCount: totalRows, autoSortAndPage: false); ;
             }
            }" +
            "</pre><p><strong>References</strong></p><a href=\"http://msdn.microsoft.com/en-us/magazine/hh288075.aspx\">Get the Most out of WebGrid in ASP.NET MVC</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_25112012_k = "MVC C# WebGrid paging strongly typed";
        public const string content_25112012_d = "More advanced usage of WebGrid control";

        //"Starting with WebGrid"
        public const string content_13112012_b = "<p><strong>WebGrid</strong> is an HTML helper provided as part of the MVC framework to simplify rendering tabular data. It is actually very simple to start with WebGrid. The following is enough to create a complete working example:</p>";
        public const string content_13112012_r = "<pre class=\"brush: csharp\">" +
            @"@model YahooViewModel

            ...

            @{ var grid = new WebGrid(Model.Datas);
               @grid.GetHtml();
             }" + "</pre><p>Here the \"Datas\" is my list of <strong>YahooData</strong> entities. This, however, looks a little ugly, so I'll spend a few minutes on styling straight away. The following is a basic style for a WebGrid</p><pre class=\"brush: xml\">" +
            @"&lt;style type=""text/css""&gt;
                .webGrid {margin: 4px; border-collapse: collapse; width: 300px;}
                .header {background-color: #E8E8E8; font-weight: bold; color: #FFF;}
                .webGrid th, .webGrid td { border: 1px solid #C0C0C0; padding: 5px;}
                .alt {background-color: #E8E8E8; color: #000;}
            &lt;/style&gt;" +
            "</pre><p>The style is applied as follows</p><pre class=\"brush: csharp\">" +
            @"@{ var grid = new WebGrid(Model.Datas);
               @grid.GetHtml(tableStyle: ""webGrid"", headerStyle: ""header"", alternatingRowStyle: ""alt"");
             }" + "</pre><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/13112012_First_WebGrid.png\" alt=\"First Attempt at WebGrid\" /></div><p align=\"center\">First WebGrid</p><p>I don't want to show each and every column to the user. I can rewrite the WebGrid specifying the actual columns to show. Only specified columns will be displayed. Also, now the order of the columns is the same as the order I define them.</p><pre class=\"brush: csharp\">" +
            @"@{ var grid = new WebGrid(Model.Datas, 
                   columnNames: new[] {""DataName"", ""Date"", ""LTP"", ""Time"", ""Volume"", ""Ask"", ""Bid"", ""High"", ""Low""});
               @grid.GetHtml(tableStyle: ""webGrid"", headerStyle: ""header"", alternatingRowStyle: ""alt"");
             }" + "</pre><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/13112012_Specific_Columns.png\" alt=\"Showing Only Specific Columns in WebGrid\" /></div><p align=\"center\">Specific columns</p><p>Another way to do it is to actually define columns explicitly. First advantage is that I can now specify a name for the header.</p><pre class=\"brush: csharp\">" +
            @"@{ var grid = new WebGrid(Model.Datas, columnNames: new[] {""DataName"", ""Date"", ""LTP"", ""Time"", ""Volume"", ""Ask"", ""Bid"", ""High"", ""Low""});
               @grid.GetHtml(columns: grid.Columns( grid.Column(""DataName"", header: ""Company""), grid.Column(""Date""), grid.Column(""LTP""), grid.Column(""Time""), grid.Column(""Volume""), 
               grid.Column(""Ask""), grid.Column(""Bid""), grid.Column(""High""), grid.Column(""Low"")),
               tableStyle: ""webGrid"", headerStyle: ""header"", alternatingRowStyle: ""alt"");
             }" +
            "</pre><p>Finally, let's assume I want to let the user click the Company name and navigate to the page that provides some more information about the company. I can use format parameter of the Column to display an ActionLink.</p><pre class=\"brush: csharp\">" +
            @"@{ var grid = new WebGrid(Model.Datas, columnNames: new[] {""DataName"", ""Date"", ""LTP"", ""Time"", ""Volume"", ""Ask"", ""Bid"", ""High"", ""Low""});
               @grid.GetHtml(columns: grid.Columns( 
               grid.Column(""DataName"", header:""Company"", format:@&lt;text&gt;@Html.ActionLink((string)item.DataName, ""Details"", ""Company"", new {id=item.SymbolId}, null)&lt;/text&gt;),
               grid.Column(""Date""), grid.Column(""LTP""), grid.Column(""Time""), grid.Column(""Volume""), 
               grid.Column(""Ask""), grid.Column(""Bid""), grid.Column(""High""), grid.Column(""Low"")),
               tableStyle: ""webGrid"", headerStyle: ""header"", alternatingRowStyle: ""alt"");
             }" + "</pre><p>The ActionLink will be created in the following format: \"http://localhost/Company/Details/1\"</p><p>Finally (for today) I would like to combine Date and Time in a single column and format it. The last bit of code shows how to format the date in the column and how to apply the style to a specific column.</p><pre class=\"brush: csharp\">" +
            @"&lt;style type=""text/css""&gt;

            ...

                .time {width: 200px; font-weight:bold;}
            &lt;/style&gt;

            @{ var grid = new WebGrid(Model.Datas, columnNames: new[] {""DataName"", ""Date"", ""LTP"", ""Time"", ""Volume"", ""Ask"", ""Bid"", ""High"", ""Low""});
               @grid.GetHtml(columns: grid.Columns( 
               grid.Column(""DataName"", header:""Company"", format:@&lt;text&gt;@Html.ActionLink((string)item.DataName, ""Details"", ""Company"", new {id=item.SymbolId}, null)&lt;/text&gt;),
               grid.Column(""DateTime"", header:""Time"", style:""time"", format:@&lt;text&gt;@item.DateTime.ToString(""dd/MM/yyyy hh:mm"")&lt;/text&gt;), 
               grid.Column(""LTP""), grid.Column(""Volume""), grid.Column(""Ask""), grid.Column(""Bid""), grid.Column(""High""), grid.Column(""Low"")),
               tableStyle: ""webGrid"", headerStyle: ""header"", alternatingRowStyle: ""alt"");
             }" +
            "</pre><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/13112012_Better_Formatting.png\" alt=\"Better Formatted WebGrid\" /></div><p align=\"center\">Better formatting</p><p>The plan from here is to add server-side paging to reduce the stress on the view when the number of records is high.</p><p><strong>References</strong></p><a href=\"http://msdn.microsoft.com/en-us/magazine/hh288075.aspx\">Get the Most out of WebGrid in ASP.NET MVC</a><br/><a href=\"http://www.dotnetcurry.com/ShowArticle.aspx?ID=615\">WebGrid WebHelper in ASP.NET MVC 3 RC</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_13112012_k = "MVC C# WebGrid";
        public const string content_13112012_d = "Basics of using WebGrid control";

        //"SEO Basics: Linking My Content to Google+ Using rel='author'"
        public const string content_02122012_b = "<p>I've learned the first step of using rich snippets to make links to my content look better in search results. The process is not extremely complicated, but it also is not intuitive to me, so I'd better write it down. I linked the content on my Blogger blog and also on my website which I'm using as training grounds. There are several steps involved - I need to modify my Google+ account, and I need to modify the content where I publish it.</p>";
        public const string content_02122012_r = "<p><strong>1. Google+ account.</strong></p><p>Assuming I already have a Google+ profile with photo on it, I go to my <strong>Profile</strong>, <strong>About</strong> and select <strong>Edit Profile</strong>.</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/02122012_Edit_Profile.png\" alt=\"Edit Google Plus Profile\" /></div><p align=\"center\">Edit Profile</p>" +
            "<p>I scroll down to where <strong>Contributor to</strong> section is. In there I add the places I'm going to post my content. I edit this section to specify where my content is posted. Now Google+ knows where I'm posting, but that's not enough - I have to provide a way to verify that it's actually me.</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/02122012_Contributes_To.png\" alt=\"Edit Contributes To\" /></div><p align=\"center\">Edit Contributor</p>" +
            "<p><strong>2. My Website.</strong></p><p>Here I have full control! I can experiment without fear to break things beyond repair. I did a few simple things so far: In the <strong>_Layout.cshtml</strong>, the partial view that is rendered on every page, I added the link to my Google+ account</p><pre class=\"brush: xml\">" +
            @"&lt;head&gt;
                &lt;link rel='author' href='https://plus.google.com/112677661119561622427/posts'/&gt;
	            ...
            &lt;/head&gt;" +
            "</pre><p>Additionally (optional) I modified the view that will display my posts to update the <strong>MetaKeywords</strong> and <strong>MetaDescription</strong> (see my previous post) dynamically.</p><pre class=\"brush: csharp\">" +
            @"@{
                ViewBag.MetaDescription = 'Description of this post';
                ViewBag.MetaKeywords = 'Keywords of this post';
                ViewBag.Title = Model.Title;
            }" + "</pre><p>I'll add appropriate properties to the <strong>Model</strong> later, but that's beyond the scope of this post. I think that's all.</p><p><strong>3. Blogger.</strong></p><p>For reason I'll try to explain below, I had to add the following to the template of my blog in Blogger:</p><pre class=\"brush: xml\">" +
            @"&lt;a class='updated' expr:href='data:post.url' rel='bookmark' title='permanent link'&gt;&lt;abbr class='updated' expr:title='data:post.timestampISO8601'&gt;&lt;data:post.timestamp/&gt;&lt;/abbr&gt;&lt;/a&gt;" +
            "</pre><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/02122012_Edit_Blogger_Template.png\" alt=\"Edit Blogger Template\" /></div><p align=\"center\">Edit Blogger Template</p><p>I also added the same link as I did for my website - I'm not sure it's absolutely necessary though.</p><p>I'll also be adding the following to the end of my posts:</p><pre class=\"brush: xml\">" +
            @"by &lt;a title='Evgeny' rel='author' href='https://plus.google.com/112677661119561622427?rel=author' alt='Google+' title='Google+'&gt;Evgeny&lt;/a&gt;." +
            "</pre><p>With all that done I can publish this very post on my website and Blogger and then test the results.</p><p><strong>4. Testing</strong></p>" +

            "<p>Now I can test the results by entering the link to my post in the <a href=\"http://www.google.com/webmasters/tools/richsnippets\">Structured Data Testing Tool</a>. I enter the url and the tool tests the link for me.</p><p>This is the Blogger post.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/02122012_Blogger_Positive_Test.png\" alt=\"Test With Blogger - Positive Result\" /></div><p align=\"center\">Blogger - Positive Test Result</p><p>And this is my website.</p>" +
            "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/02122012_Website_Positive_Test.png\" alt=\"Test With Website - Positive Result\" /></div><p align=\"center\">Website - Positive Test Result</p>" +
            "<p>Finally, what would have happened if I hadn't added that bit to the Blogger template? I did not save the exact screenshot, but the error returned was \"Missing required field 'Updated'\" and looked similar to the image below.</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/02122012_Blogger_Negative_Test.png\" alt=\"Missing Required Field Updated\" /></div><p align=\"center\">Missing Required Field \"Updated\"</p>" +

            "<p><strong>References</strong></p><a href=\"http://www.netargument.com/2012/03/warning-missing-required-field-in.html\">Warning: Missing required field \"updated\" in Blogger Rich Snippet Webmaster Tool [Solved]</a><br/><a href=\"http://www.hanselman.com/blog/EmbraceAuthorshipTheImportanceOfRelmeAndRelauthorOnYourContentsSEOAndGoogle.aspx\">Embrace Authorship - The importance of rel=me and rel=author on your content's SEO and Google</a><br/><a href=\"http://www.wordtracker.com/academy/rich-snippets\">Rich snippets for idiots. And, er, you.</a><br/><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_02122012_d = "I describe how I linked the posts to Blogger and my own website to my Google+ account and improve the way they look in search results if anyone ever cares to search for them";
        public const string content_02122012_k = "Blog SEO Google rel author rich snippet";

        //"SEO Basics: Friendly URLs"
        public const string content_10122012_b = "<p>Implementing SEO-friendly URLs turned out to be much easier than I expected - MVC routing already takes care of the \"heavy lifting\". The developer only needs to provide a function that returnes the \"friendly urls\" from strings (product names, blog titles etc.) and to update action links.</p>";
        public const string content_10122012_r =
            "<p>1. Routing. A new route needs to be added. It has to be added above the default route so that MVC framework attempted to match it first. The seofriendly parameter can be pretty much anything that will satisfy valid url requirements.</p><pre class=\"brush:csharp\">" +
            @"routes.MapRoute(
	            name: ""SEOFriendly"",
	            url: ""{controller}/{action}/{id}/{seofriendly}"",
	            defaults: new { controller = ""Home"", action = ""Index"", id = UrlParameter.Optional, seofriendly = """" }
            );" +
            "</pre><p>2. Creating friendly urls. Here is an example I found on the web and added it \"as is\".</p><pre class=\"brush:csharp\">" +
            @"public static string ToSeoUrl(this string url)
            {
	            // make the url lowercase
	            string encodedUrl = (url ?? """").ToLower();

	            // replace & with and
	            encodedUrl = Regex.Replace(encodedUrl, @""\&+"", ""and"");

	            // remove characters
	            encodedUrl = encodedUrl.Replace(""'"", """");

	            // remove invalid characters
	            encodedUrl = Regex.Replace(encodedUrl, @""[^a-z0-9]"", ""-"");

	            // remove duplicates
	            encodedUrl = Regex.Replace(encodedUrl, @""-+"", ""-"");

	            // trim leading & trailing characters
	            encodedUrl = encodedUrl.Trim('-');

	            return encodedUrl; 
            }" + "</pre><p>3. Making use of the friendly url. Just adding an extra parameter to the object.</p><p>Before:</p><pre class=\"brush:xml\">" +
            @"&lt;div class=""display-button""&gt;@Html.ActionLink(""Edit"", ""Edit"", new { id=item.PostID }) &lt;/div&gt;
            &lt;div class=""display-button""&gt;@Html.ActionLink(""Details"", ""Details"", new { id = item.PostID }) &lt;/div&gt;
            &lt;div class=""display-button""&gt;@Html.ActionLink(""Delete"", ""Delete"", new { id = item.PostID }) &lt;/div&gt;" +
            "</pre><p>After:</p><pre class=\"brush:xml\">" +
            @"&lt;div class=""display-button""&gt;@Html.ActionLink(""Edit"", ""Edit"", new { id=item.PostID, seofriendly = item.Title.ToSeoUrl() }) &lt;/div&gt;
            &lt;div class=""display-button""&gt;@Html.ActionLink(""Details"", ""Details"", new { id = item.PostID, seofriendly = item.Title.ToSeoUrl() }) &lt;/div&gt;
            &lt;div class=""display-button""&gt;@Html.ActionLink(""Delete"", ""Delete"", new { id = item.PostID, seofriendly = item.Title.ToSeoUrl() }) &lt;/div&gt;" +
            "</pre><p><strong>References:</strong></p><a href=\"http://quysnhat.wordpress.com/2012/03/14/seo-friendly-urls-in-asp-net-mvc/\">SEO-Friendly URLs in ASP.Net MVC 3</a><br/><a href=\"http://stackoverflow.com/questions/217960/how-can-i-create-a-friendly-url-in-asp-net-mvc\">How can I create a friendly URL in ASP.NET MVC?</a><br/>by <a rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_10122012_d = "A brief guide on implementing \"SEO-friendly\", or human readable, URL using MVC framework";
        public const string content_10122012_k = "MVC C# SEO friendly url";

        //Implementing a Tree View - Small Case Study
        public const string content_20122012_b = "<p>Implementing the control that allows navigating my blog history could be roughly divided into 4 steps.</p><p><strong>1. Select and group posts from the database</strong></p><p>Here the LINQ grouping came handy. Starting with grouping posts by year published to create my top level in hierarchy, the query would look like this:</p><pre class=\"brush:csharp\">" + @"var results = from allPosts in db.Posts.OrderBy(p =&gt; p.DateCreated)
			  group allPosts by allPosts.DateCreated.Year into postsByYear;" + "</pre><p>Here <strong>results</strong> is the enumeration of groups - in my case, groups of posts which were published in the certain year. Posts are grouped by the key, which is defined in the <strong>IGrouping</strong> interface.</p><p>Moving further, I want to create child groups, in my case - posts by the month. I have to add a nested query along these lines</p><pre class=\"brush:csharp\">" + @"var results = from allPosts in db.Posts.OrderBy(p =&gt; p.DateCreated)
			  group allPosts by allPosts.DateCreated.Year into postsByYear

			  select new
			  {
				  postsByYear.Key,
				  SubGroups = from yearLevelPosts in postsByYear
							  group yearLevelPosts by yearLevelPosts.DateCreated.Month into postsByMonth;
			  };" + "</pre>";
        public const string content_20122012_r = "<p>This is still not too bad. The first level are posts by year. Each year has <strong>SubGroups</strong> property which stores the group of posts published in a certian month. Now I finally need to get all the posts published in a month. I end up with the following query:</p><pre class=\"brush:csharp\">" + @"var results = from allPosts in db.Posts.OrderBy(p =&gt; p.DateCreated)
			  group allPosts by allPosts.DateCreated.Year into postsByYear

			  select new
			  {
				  postsByYear.Key,
				  SubGroups = from yearLevelPosts in postsByYear
							  group yearLevelPosts by yearLevelPosts.DateCreated.Month into postsByMonth
							  select new
							  {
								  postsByMonth.Key,
								  SubGroups = from monthLevelPosts in postsByMonth
											  group monthLevelPosts by monthLevelPosts.Title into post
											  select post
							  }
			  };" + "</pre><p>It is fully functional and suits my purposes. It is on the edge of being unreadable, however, and if I had to add one more level of depth it would probably be beyond. Following the example from <a href=\"http://blogs.msdn.com/b/mitsu/\">Mitsu Furuta's blog</a>, I make the query generic. The <strong>GroupResult</strong> class holds the grouping key and the group items. The GroupByMany extension allows for an undefined number of group selectors. This is the code I need to make it work:</p><pre class=\"brush:csharp\">" + @"public static class MyEnumerableExtensions
{
	public static IEnumerable&lt;GroupResult&gt; GroupByMany&lt;TElement&gt;(this IEnumerable&lt;TElement&gt; elements, params Func&lt;TElement, object&gt;[] groupSelectors)
	{
		if (groupSelectors.Length &gt; 0)
		{
			var selector = groupSelectors.First();

			//reduce the list recursively until zero
			var nextSelectors = groupSelectors.Skip(1).ToArray();
			return
				elements.GroupBy(selector).Select(
					g =&gt; new GroupResult
					{
						Key = g.Key,
						Items = g,
						SubGroups = g.GroupByMany(nextSelectors)
					});
		}
		else
			return null;
	}
}

public class GroupResult
{
	public object Key { get; set; }
	public IEnumerable Items { get; set; }
	public IEnumerable&lt;GroupResult&gt; SubGroups { get; set; }
}" + "</pre><p>And now I can rewrite my query in one line:</p><pre class=\"brush:csharp\">" + @"var results = db.Posts.OrderBy(p =&gt; p.DateCreated).GroupByMany(p =&gt; p.DateCreated.Year, p =&gt; p.DateCreated.Month);" + "</pre><p><strong>2. Populate a tree structure that will be used to generate HTML</strong></p><p>I used a complete solution suggested by <a href=\"http://marktinderholt.wordpress.com/2011/10/09/rendering-a-tree-view-using-asp-net-mvc-3-razor/\">Mark Tinderhold</a> almost without changes.</p><p>The <strong>BlogEntry</strong> class has a <strong>Name</strong>, which will be rendered, and references to <strong>Children</strong> and <strong>Parent</strong> nodes.</p><pre class=\"brush:csharp\">" + @"public class BlogEntry : ITreeNode&lt;BlogEntry&gt;
{
	public BlogEntry()
	{
		Children = new List&lt;BlogEntry&gt;();
	}

	public string Name { get; set; }
	public BlogEntry Parent { get; set; }
	public List&lt;BlogEntry&gt; Children { get; set; }
}" + "</pre><p>A list of <strong>BlogEntry</strong> is populated from my query results</p><pre class=\"brush:csharp\">" + @"var entries = new List&lt;BlogEntry&gt;();

//years
foreach (var yearPosts in results)
{
	//create ""year-level"" item
	var year = new BlogEntry { Name = yearPosts.Key.ToString().ToLink(string.Empty) };
	entries.Add(year);

	//months
	foreach (var monthPosts in yearPosts.SubGroups)
	{
		var month = new BlogEntry { Name = new DateTime(2000, (int)monthPosts.Key, 1).ToString(""MMMM"").ToLink(string.Empty), Parent = year };
		year.Children.Add(month);

		foreach (var postEntry in monthPosts.Items)
		{
			//create ""blog entry level"" item
			var post = postEntry as Post;
			var blogEntry = new BlogEntry { Name = post.Title.ToLink(""/Post/"" + post.PostID + ""/"" + post.Title.ToSeoUrl()), Parent = month };
			month.Children.Add(blogEntry);
		}
	}
}" + "</pre><p><strong>3. Use the tree structure to generate HTML</strong></p><p>The TreeRenderer writes out HTML.</p><pre class=\"brush:csharp\">" + @"public interface ITreeNode&lt;T&gt;
{
	T Parent { get; }
	List&lt;T&gt; Children { get; }
}

public static class TreeRenderHtmlHelper
{
	public static string RenderTree&lt;T&gt;(this HtmlHelper htmlHelper, IEnumerable&lt;T&gt; rootLocations, Func&lt;T, string&gt; locationRenderer) where T : ITreeNode&lt;T&gt;
	{
		return new TreeRenderer&lt;T&gt;(rootLocations, locationRenderer).Render();
	}
}
public class TreeRenderer&lt;T&gt; where T : ITreeNode&lt;T&gt;
{
	private readonly Func&lt;T, string&gt; locationRenderer;
	private readonly IEnumerable&lt;T&gt; rootLocations;
	private HtmlTextWriter writer;
	public TreeRenderer(IEnumerable&lt;T&gt; rootLocations, Func&lt;T, string&gt; locationRenderer)
	{
		this.rootLocations = rootLocations;
		this.locationRenderer = locationRenderer;
	}
	public string Render()
	{
		writer = new HtmlTextWriter(new StringWriter());
		RenderLocations(rootLocations);
		return writer.InnerWriter.ToString();
	}
	/// &lt;summary&gt;
	/// Recursively walks the location tree outputting it as hierarchical UL/LI elements
	/// &lt;/summary&gt;
	/// &lt;param name=""locations""&gt;&lt;/param&gt;
	private void RenderLocations(IEnumerable&lt;T&gt; locations)
	{
		if (locations == null) return;
		if (locations.Count() == 0) return;
		InUl(() =&gt; locations.ForEach(location =&gt; InLi(() =&gt;
		{
			writer.Write(locationRenderer(location));
			RenderLocations(location.Children);
		})));
	}
	private void InUl(Action action)
	{
		writer.WriteLine();
		writer.RenderBeginTag(HtmlTextWriterTag.Ul);
		action();
		writer.RenderEndTag();
		writer.WriteLine();
	}
	private void InLi(Action action)
	{
		writer.RenderBeginTag(HtmlTextWriterTag.Li);
		action();
		writer.RenderEndTag();
		writer.WriteLine();
	}
}" + "</pre><p>The renderer will be called the following way from the view:</p><pre class=\"brush:csharp\">" + @"&lt;div id=""treeview"" class=""treeview""&gt;
    @MvcHtmlString.Create(Html.RenderTree(Model.BlogEntries, x =&gt; x.Name))
&lt;/div&gt;" + "</pre><p><strong>4. Render the HTML on the webpage</strong></p><p>After reviewing a couple of other options, I decided on a <a href=\"https://github.com/vakata/jstree\">jsTree</a>. It has rich capabilities, but to this point I only used the \"default\" options. I added the tree to the <strong>_Layout.cshtml</strong> by adding a line of code</p><pre class=\"brush:csharp\">" + @"@Html.Action(""BlogResult"", ""BlogEntry"")" + "</pre><p>This line calls the function in the <strong>BlogEntryController</strong></p><pre class=\"brush:csharp\">" + @"public PartialViewResult BlogResult()
{
	var results = db.Posts.OrderBy(p =&gt; p.DateCreated).GroupByMany(p =&gt; p.DateCreated.Year, p =&gt; p.DateCreated.Month);

	entries = new List&lt;BlogEntry&gt;();
	
	//code that populates entries - see above

	BlogEntryViewModel model = new BlogEntryViewModel(entries);

	return PartialView(model);
}" + "</pre><p>The <strong>BlogEntryViewModel</strong> is extremely simple.</p><pre class=\"brush:csharp\">" + @"public class BlogEntryViewModel
{
	public List&lt;BlogEntry&gt; BlogEntries { get; set; }

	public BlogEntryViewModel(List&lt;BlogEntry&gt; blogEntries)
	{
		BlogEntries = blogEntries;
	}

	public BlogEntryViewModel()
	{
	}
}" + "</pre><p>Finally, the partial view that is rendered</p><pre class=\"brush:csharp\">" + @"@model Recipes.ViewModels.BlogEntryViewModel

@{ Layout = null; }

&lt;link href=""@Url.Content(""~/Content/blogentry.css"")"" rel=""stylesheet"" type=""text/css"" /&gt;

&lt;!-- Tree View jstree --&gt;
&lt;script src=""@Url.Content(""~/Scripts/jquery.js"")"" type=""text/javascript""&gt;&lt;/script&gt;
&lt;script src=""@Url.Content(""~/Scripts/jquery.hotkeys.js"")"" type=""text/javascript""&gt;&lt;/script&gt;
&lt;script src=""@Url.Content(""~/Scripts/jquery.cookie.js"")"" type=""text/javascript""&gt;&lt;/script&gt;
&lt;script src=""@Url.Content(""~/Scripts/jquery.jstree.js"")"" type=""text/javascript""&gt;&lt;/script&gt;

&lt;script type=""text/javascript""&gt;
    jQuery(function ($) {
        $(""#treeview"").jstree({ ""plugins"": [""themes"", ""html_data""] });
    });
&lt;/script&gt;

&lt;div class=""blogheader""&gt;
&lt;h2&gt;Blog Archives&lt;/h2&gt;
&lt;/div&gt;
&lt;div id=""treeview"" class=""treeview""&gt;
    @MvcHtmlString.Create(Html.RenderTree(Model.BlogEntries, x =&gt; x.Name))
&lt;/div&gt;" + "</pre><p>What I had to make sure of to make it work:</p><p>And for information, this is the contents of <strong>blogentry.css</strong></p><pre class=\"brush:csharp\">" + @"div.treeview, div.blogheader {
    width: 14em;
    background: #eee;
  	overflow: hidden;
	text-overflow: ellipsis;
}

div.blogheader h2 
{
    font: bold 11px/16px arial, helvetica, sans-serif;
    display: block;
    border-width: 1px;
    border-style: solid;
    border-color: #ccc #888 #555 #bbb;
    margin: 0;
    padding: 2px 3px;
    
    color: #fff;
    background: #000;
    text-transform: uppercase;
}" + "</pre><p>The end result looks like that:</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2012/20122012_Resulting_Treeview.png\" alt=\"Resulting Treeview\" /></div><p align=\"center\">Resulting Treeview</p><p><strong>References:</strong></p><a href=\"http://stackoverflow.com/questions/2230202/how-can-i-hierarchically-group-data-using-linq\">How can I hierarchically group data using LINQ?</a><br/><a href=\"http://blogs.msdn.com/b/mitsu/archive/2007/12/22/playing-with-linq-grouping-groupbymany.aspx\">Playing with Linq grouping: GroupByMany ?</a><br/><a href=\"http://mikehadlow.blogspot.com.au/2008/10/rendering-tree-view-using-mvc-framework.html\">Rendering a tree view using the MVC Framework</a><br/><a href=\"http://jquery.bassistance.de/treeview/demo/\">jQuery Treeview Plugin Demo</a><br/><a href=\"http://tkgospodinov.com/jstree-part-1-introduction/\">jsTree – Part 1: Introduction</a><br/><a href=\"https://github.com/vakata/jstree\">jsTree on GitHub</a><br/><a href=\"http://stackoverflow.com/questions/5730795/best-place-to-learn-jstree\">Best place to learn JStree</a><br/>by <a title= \"Evgeny\" rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_20122012_d = "Implementing a Tree View on my Website";
        public const string content_20122012_k = "Implementing a Tree View jstree MVC C# Software Development LINQ grouping";

        //Use of PostgreSQL Indexes
        public const string content_05022013_b = "<p>I'm busy with investigating how indexing works in PostgreSQL and what are the ways to improve it. One particularly useful query I came across is this:</p>";
        public const string content_05022013_r = "<pre class=\"brush:csharp\">" + @"SELECT idstat.schemaname AS schema_name, idstat.relname AS table_name,
indexrelname AS index_name,
idstat.idx_scan AS times_used,
pg_size_pretty(pg_relation_size(idstat.relid)) AS table_size, pg_size_pretty(pg_relation_size(indexrelid)) AS index_size,
n_tup_upd + n_tup_ins + n_tup_del as num_writes,
indexdef AS definition
FROM pg_stat_user_indexes AS idstat JOIN pg_indexes ON (indexrelname = indexname AND idstat.schemaname = pg_indexes.schemaname)
JOIN pg_stat_user_tables AS tabstat ON idstat.relid = tabstat.relid
WHERE idstat.idx_scan  &lt; 200
	AND indexdef !~* 'unique'
ORDER BY idstat.relname, indexrelname;" + "</pre><p>It returns the following information for each index in the database:</p>" +
    "<ul><li>schema name</li><li>table name</li><li>index name</li><li>disk space used by the index, and the table</li><li>how many rows were inserted, deleted or updated</li><li>how many times the index was used</li></ul>" +
        "<p>If the database was used for some time, the information may help to find out which indexes are not used at all, or used rarely but occupy a lot of space on the disk. Or it may suggest that something is not working as designed - a rarely used index that was expected to be used a lot is probably a warning sign.</p><p>The unfortunate complication that I came across was that the query returned absolutely no data after the database restore. My current understanding is that PostgreSQL does not backup the pg_catalog, and also the indexes are rebuilt when the database is restored. Therefore, if I do not have direct access to the database, I have to either ask someone to run the script (and give them the PostgreSQL/pgAdmin password), or somehow obtain a file system level copy of the database. In the future, I'll need to create a utility that extracts this information and saves it to a file.</p><p><b>References</b></p><a href=\"http://instagram-engineering.tumblr.com/post/40781627982/handling-growth-with-postgres-5-tips-from-instagram\">Handling Growth with Postgres: 5 Tips From Instagram</a><br/><a href=\"https://devcenter.heroku.com/articles/postgresql-indexes\">Efficient Use of PostgreSQL Indexes</a><br/><a href=\"http://it.toolbox.com/blogs/database-soup/finding-useless-indexes-28796\">Finding Useless Indexes</a><br/><a href=\"http://www.postgresql.org/docs/8.1/static/functions-admin.html\">9.20. System Administration Functions</a><br/><a href=\"http://www.postgresql.org/docs/devel/static/monitoring-stats.html\">27.2. The Statistics Collector</a><br/><a href=\"http://stackoverflow.com/questions/14472065/postgresql-index-usage-backing-up-pg-catalog-data\">PostgreSQL index usage, backing up pg_catalog data</a><br/>"
        + "by <a title= \"Evgeny\" rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_05022013_d = "Investigating how indexing works in PostgreSQL and how usage of indexes may be improved";
        public const string content_05022013_k = "PostgreSQL SQL index database performance";

        //Photobox – CSS3 JQuery Image Gallery
        public const string content_12022013_b = "<p>I came across a nice image gallery script which is lightweight, hardware accelerated and generally looks good. Image can be zoomed in and out using mouse wheel and navigated using mouse move. Image 'alt' is shown at the bottom, and the row of thumbnail images is also displayed at the bottom. The autoplay is supported and time is configurable. The script can be downloaded from <a href=\"https://github.com/yairEO/photobox\">Photobox github</a>. It only supports IE 8 and higher, and does not look as good as in other browsers though.</p><p>The usage is very easy: jQuery, script and css have to be referenced as usual, i.e.</p>";

        public const string content_12022013_r = "<pre class=\"brush:csharp\">" + @"&lt;script src=""//ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"" type=""text/javascript""&gt;&lt;/script&gt; 
&lt;link href=""@Url.Content(""~/Scripts/photobox/photobox.css"")"" rel=""stylesheet"" type=""text/css""/&gt;
&lt;link href=""@Url.Content(""~/Scripts/photobox/photobox.ie.css"")"" rel=""stylesheet"" type=""text/css""/&gt;
&lt;script src=""@Url.Content(""~/Scripts/photobox/photobox.js"")"" type=""text/javascript""&gt;&lt;/script&gt;" + "</pre><p>A gallery with all default values (again, check <a href=\"https://github.com/yairEO/photobox\">Photobox github</a> for parameters) is included as follows</p><pre class=\"brush:csharp\">" + @"&lt;div id='gallery'&gt;
		&lt;a href=""../../../Content/photobox/P1.jpg""&gt;
			&lt;img src=""../../../Content/photobox/P1_small.jpg"" alt=""photo1 title""/&gt;
		&lt;/a&gt;

		...
		//More images
&lt;/div&gt;

&lt;script type=""text/javascript""&gt;
	$(document).ready(function () {
	    $('#gallery').photobox('a');
	});
&lt;/script&gt;" + "</pre><p>A more involved setup with parameters may look as follows</p><pre class=\"brush:csharp\">" + @"&lt;script type=""text/javascript""&gt;
	$(document).ready(function () {
	    $('#gallery').photobox('a:first', { thumbs:false, time:0 }, imageLoaded);
		function imageLoaded(){
			console.log('image has been loaded...');
		}
	});
&lt;/script&gt;" + "</pre><p>I added a sample gallery (photos courtesy of my wife) to my website: <a href=\"http://ynegve.info/Photobox/Index\">Photobox Example</a></p><p>The border around the images is totally optional</p><pre class=\"brush:csharp\">" + @"&lt;style type=""text/css""&gt;
img {
   padding:1px;
   border:1px solid #021a40;
   background-color:#ff0;
}
&lt;/style&gt;" + "</pre><p><b>References</b></p><a href=\"http://dropthebit.com/500/photobox-css3-image-gallery-jquery-plugin/\">Photobox – CSS3 JQuery Image Gallery</a><br/><a href=\"https://github.com/yairEO/photobox\">Photobox github</a><br/><a href=\"http://stackoverflow.com/questions/5168093/jquery-access-nested-div\">jquery access nested div</a><br/><a href=\"http://css-tricks.com/using-css-for-image-borders/\">Using CSS for Image Borders</a><br/>" +
            "by <a title= \"Evgeny\" rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_12022013_d = "How to use photobox - nice image gallery script which is lightweight, hardware accelerated and generally looks good.";
        public const string content_12022013_k = "Photobox CSS JQuery Image Gallery script programming";

        //On PostgreSQL Inverse mode and database audit table triggers
        public const string content_27022013_b = "<p>While working on a different issue, I noticed something strange about a history table. A history table keeps all changes done to the records in the main table - whenever something was inserted, updated or deleted, a record is added to the history table. However, each time something was inserted, there were two records added to the history table. First one had a null value in the main table foreign key field, and the second one had the correct value.</p>";
        public const string content_27022013_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2013/25022013_Duplicates_History_Table.png\" alt=\"Duplicates History Table\" /></div><p align=\"center\">Duplicates in history table</p><p>Turns out that the reason is the way <b>NHibernate</b> maps objects to data. In my case, the parent entity knows about the child entity. The child entity, however, has no knowledge of the parent entity. This can be most easily explained by looking at the following snippet:</p><pre class=\"brush:csharp\">" + @"public class Order
{
	public virtual int Id { get; set; }
	public virtual ICollection&lt;Detail&gt; Details { get; set; }
}

public class Detail
{
	public virtual int Id { get; set; }
	public virtual string Name { get; set; }
}

// ...
var order = new Order();
var detail = new Detail() {Name = ""Widget""};

session.Persist(detail);
order.Details.Add(detail);" + "</pre><p>While the <b>Order</b> knows about the <b>Details</b>, each <b>Detail</b> has no knowledge about the parent <b>Order</b>. PostgreSQL will perform the following sequence of actions:<ul><li>Insert the Order</li><li>Insert the Detail with OrderId = null</li><li>Update the Detail with actual OrderId</li></ul>which in my case looks like this:</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2013/25022013_Insert_Then_Update.png\" alt=\"Insert Then Update\" /></div><p align=\"center\">Insert, then Update</p><p>How can that be fixed so that only one <b>INSERT</b> statement is executed? Well, here is where <b></b>Inverse comes into play. Check the references at the end of the post for a proper explanation, but the definition is that setting <b>Inverse</b> to true places responsibility of the saving the relationship on the \"other side\", in this case - on the child. In the mapping, that will look as a change from</p><pre class=\"brush:csharp\">" + @"mapping.HasMany(x =&gt; x.Children).KeyColumn(""inventoryitemid"")
	.AsSet();" + "</pre><p>To</p><pre class=\"brush:csharp\">" + @"mapping.HasMany(x =&gt; x.Children).KeyColumn(""inventoryitemid"")
	.AsSet()
	.Inverse();	" + "</pre><p>And the code snippet should now look this way	</p><pre class=\"brush:csharp\">" + @"public class Order
{
	public virtual int Id { get; set; }
	public virtual ICollection&lt;Detail&gt; Details { get; set; }
}

public class Detail
{
	public virtual Order Order { get; set; }
	public virtual int Id { get; set; }
	public virtual string Name { get; set; }
}

// ...
var order = new Order();
var detail = new Detail() {Name = ""Widget"", Order = order};

session.Persist(detail);
order.Details.Add(detail);" + "</pre><p>Note how the Order is added to Detail and also is passed when the Detail object is created. In my case, the profiler now shows the following:</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2013/25022013_Insert_Only.png\" alt=\"Insert Only\" /></div><p align=\"center\">Insert only</p><p>So, problem solved? Well, yes and no. While profiling the modified solution, I found that now a lot more actions were logged in the same scenario - most of them were second level cache access. The impact on performance is not immediately obvious, but hardly beneficial.</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2013/25022013_Second_Level_Cache.png\" alt=\"Second Level Cache\" /></div><p align=\"center\">Second level cache access</p><p>It is likely that by adding a reference to the parent I forced NHibernate to retrieve the whole parent entity. Eventually, not having enough time to evaluate full impact of the change, a somewhat \"compromise\" solution was chosen: modify a trigger that inserts into a \"history\" table to check if the value of a foreign key is not null.</p><p>So, a trigger that looked like this</p><pre class=\"brush:sql\">" + @"CREATE OR REPLACE FUNCTION process_emp_audit() RETURNS TRIGGER AS $emp_audit$
    BEGIN
        IF (TG_OP = 'INSERT') THEN
            INSERT INTO emp_audit SELECT 'I', now(), user, NEW.*;
            RETURN NEW;
		...
        RETURN NULL; -- result is ignored since this is an AFTER trigger
    END;
$emp_audit$ LANGUAGE plpgsql;" + "</pre><p>would now be modified as follows</p><pre class=\"brush:sql\">" + @"CREATE OR REPLACE FUNCTION process_emp_audit() RETURNS TRIGGER AS $emp_audit$
    BEGIN
        IF (TG_OP = 'INSERT' AND NEW.foreignkeyfield IS NOT NULL) THEN
            INSERT INTO emp_audit SELECT 'I', now(), user, NEW.*;
            RETURN NEW;
		...
        RETURN NULL; -- result is ignored since this is an AFTER trigger
    END;
$emp_audit$ LANGUAGE plpgsql;" + "</pre><p>Looks like a dirty hack, which falls into the \"best we can do in the least time\" category.</p><p><b>References:</b></p><a href=\"http://www.emadashi.com/2008/08/nhibernate-inverse-attribute/\">NHibernate Inverse attribute</a><br/><a href=\"http://marekblotny.blogspot.com.au/2009/02/fluent-nhbernate-and-collections.html\">Fluent NHibernate and Collections Mapping</a><br/><a href=\"http://martin.podval.eu/2010/10/nhibernate-performance-issues-list.html\">NHibernate performance issues #1: evil List (non-inverse relationhip)</a><br/><a href=\"http://notherdev.blogspot.com.au/2012/04/nhibernates-inverse-what-does-it-really.html\">NHibernate's inverse - what does it really mean?</a><br/><a href=\"http://www.postgresql.org/docs/9.2/static/plpgsql-trigger.html\">39.9. Trigger Procedures</a><br/>" + "by <a title= \"Evgeny\" rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_27022013_d = "Understanding PostgreSQL entity to data mapping, inverse mode and audit tables";
        public const string content_27022013_k = "PostgreSQL, audit table, trigger, cache, entity, mapping, inverse";


        //Fixing PostgreSQL index bloating with scheduled REINDEX via pgAgent
        public const string content_11032013_b = "<p>There is a problem generally called <i>index bloating</i> that occurs in <b>PostgreSQL</b> certain circumstances when there are continuous inserts and deletes happening in a table. It is described as follows \"B-tree index pages that have become completely empty are reclaimed for re-use. However, there is still a possibility of inefficient use of space: if all but a few index keys on a page have been deleted, the page remains allocated. Therefore, a usage pattern in which most, but not all, keys in each range are eventually deleted will see poor use of space. For such usage patterns, periodic reindexing is recommended\".</p><p>This looks exactly like the problem I came across, when a table with ~2K rows had an index of over 120MB and another table with ~80K rows had 4 indexes on it, with total size approaching 3GB. The <b>AUTOVACUUM</b> was running as configured by default but apparently not enough to prevent index bloating.</p><p>Eventually, I decided configuring a <b>REINDEX</b> to run monthly on said tables. <b>pgAgent</b> is the job scheduler to use with PostgreSQL, but with PostgreSQL 9.1 I could install it following the documentation - the tables were created and the service was running, but I could not find any UI for it. So here's an example script that I used to create a scheduled job.</p>";
        public const string content_11032013_r = "<pre class=\"brush:sql\">" + @"SET search_path = pgagent;

INSERT INTO pga_jobclass VALUES (6, 'Scheduled Tasks');

INSERT INTO pga_job VALUES (5, 6, 'TableReindex', 'Reindex tables', '', true, 
	'2013-03-27 10:00:00.000+11', --date created
	'2013-03-07 10:00:00.000+11', --date changed
	NULL, NULL, NULL);

INSERT INTO pga_schedule VALUES (3, 5, 'TableReindexSchedule', 'Reindex tables', 
	true, --enabled
	'2013-03-27 10:00:00.000+11', --start date
	NULL, --end (never)
	'{t,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f}', --minutes: 't' for run on the first minute of an hour
	'{t,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f}', --hours: 't' to run at 3 AM
	'{t,t,t,t,t,t,t}', -- weekdays: don't care, all false
	'{t,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f}', -- monthdays: 't' to run on the first day
	'{t,t,t,t,t,t,t,t,t,t,t,t}'); -- months: all true to run on the first day on each month

INSERT INTO pga_jobstep VALUES (5, 5, 'TableReindexInfo', '', true, 's', 'REINDEX TABLE mytable1;REINDEX TABLE mytable2;', '', '@@DATABASE_NAME@@', 'f', NULL);" + "</pre><p>To verify, I checked the pga_job table and found '2013-04-01 03:00:00+11' in jobnextrun column - that's when I want to run it, at 3 AM on the first of next month.</p><p>I still have a question though - I tried using the </p><pre class=\"brush:sql\">" + @"'{f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,t}', -- monthdays: 't' " + "</pre><p>to run on the first day pattern, because the last value is supposedly used for setting the \"last day\" - run on the last day of the month.</p><p>This, however, returns me quite a bunch of errors which suggest that PostgreSQL has some troubles calculating the next time the job will run.</p><pre class=\"brush:sql\">" + @"ERROR:  value ""32768"" is out of range for type smallint
CONTEXT:  PL/pgSQL function ""pga_next_schedule"" line 1254 at assignment
SQL statement ""SELECT                                                    MIN(pgagent.pga_next_schedule(jscid, jscstart, jscend, jscminutes, jschours, jscweekdays, jscmonthdays, jscmonths))
               FROM pgagent.pga_schedule
              WHERE jscenabled AND jscjobid=OLD.jobid""
PL/pgSQL function ""pga_job_trigger"" line 24 at SQL statement
SQL statement ""UPDATE pgagent.pga_job
           SET jobnextrun = NULL
         WHERE jobenabled AND jobid=NEW.jscjobid""
PL/pgSQL function ""pga_schedule_trigger"" line 60 at SQL statement

********** Error **********

ERROR: value ""32768"" is out of range for type smallint
SQL state: 22003
Context: PL/pgSQL function ""pga_next_schedule"" line 1254 at assignment
SQL statement ""SELECT                                                    MIN(pgagent.pga_next_schedule(jscid, jscstart, jscend, jscminutes, jschours, jscweekdays, jscmonthdays, jscmonths))
               FROM pgagent.pga_schedule
              WHERE jscenabled AND jscjobid=OLD.jobid""
PL/pgSQL function ""pga_job_trigger"" line 24 at SQL statement
SQL statement ""UPDATE pgagent.pga_job
           SET jobnextrun = NULL
         WHERE jobenabled AND jobid=NEW.jscjobid""
PL/pgSQL function ""pga_schedule_trigger"" line 60 at SQL statement" + "</pre><p>The simplest way to reproduce it is to run an UPDATE similar to the following</p><pre class=\"brush:sql\">" + @"UPDATE pgagent.pga_schedule
SET
jscmonthdays = '{f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,f,t}'
WHERE jscid = 3" + "</pre><p>Since I don't really care about running it on a first or last day, I won't dig deep into it. Could be a bug in PostgreSQL for all I know.</p><p><b>References:</b></p><a href=\"http://www.postgresql.org/docs/9.1/static/routine-reindex.html\">23.2. Routine Reindexing</a><br/><a href=\"http://www.mkyong.com/database/how-to-install-pgagent-on-windows-postgresql-job-scheduler/\">How To Install PgAgent On Windows (PostgreSQL Job Scheduler)</a><br/><a href=\"http://www.postgresonline.com/journal/archives/19-Setting-up-PgAgent-and-Doing-Scheduled-Backups.html\">Setting up PgAgent and Doing Scheduled Backups</a><br/><a href=\"http://wiki.postgresql.org/wiki/Automated_Backup_on_Windows\">Automated Backup on Windows</a><br/>" +
            
            "by <a title= \"Evgeny\" rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_11032013_d = "Fix the index bloating in PostgreSQL by running a scheduled reindex. Schedule reindex by using the pgAgent. Configure the scheduled job in pgAgent by running a database query directly.";
        public const string content_11032013_k = "PostgreSQL 9.1. index bloating reindex autovacuum pgagent pga_job pga_schedule pga_jobstep";

        //Improving a PostgreSQL report performance: Part 1 - RETURN QUERY EXECUTE
        public const string content_21032013_b = "<p>I was working on optimising a report which had a very poor performance. The \"heart\" of the report was a fairly complex query which I will briefly refer to as follows</p>";
        public const string content_21032013_r = "<pre class=\"brush:sql\">" + @"select Column1 as Name1, Column2 as Name2
from sometable tbl
inner join ...
where ...
and ...
and $1 &lt;= somedate
and $2 &gt;= somedate
group by ...
order by ...;" + "</pre><p>In fact, the query joined seven tables and several <b>WHERE</b> conditions, grouping on four fields and finally sorting the results. I went through the usual stuff with analyzing the query plan, verifying that all required indexes were in place (a couple of joins on particularly large tables, unfortunately, were on the 'varchar' fields, but modifying the database at this stage is out of the question so I had to do what I could). Eventually what limited amount of tricks I had at my disposal was depleted, and the performance only slightly improved. However, when I measured the performance of the report when called from the application and compared it to running the query directly against the database, there was a significant overhead in case of the report. When the report was ran from code, it sent the following query to the database:</p><pre class=\"brush:sql\">" + @"select * from myreportsubfunction ('2013-03-13', '2013-03-20');" + "</pre><p>And the <b>myreportsubfunction</b> was declared similar to the following:</p><pre class=\"brush:sql\">" + @"CREATE OR REPLACE FUNCTION myreportsubfunction(IN from timestamp without time zone, IN to timestamp without time zone)
  RETURNS TABLE(Name1 character varying, Name2 character varying) AS
$BODY$
select Column1 as Name1, Column2 as Name2
from sometable tbl
inner join ...
where ...
and ...
and $1 &lt;= somedate
and $2 &gt;= somedate
group by ...
order by ...;
$BODY$
  LANGUAGE sql VOLATILE" + "</pre><p>So - what's the trick here? The function seems to return the result of the query, but takes way much longer to execute compared to the raw query. And here is the reason: when the database prepares a query plan for the function, it does not know anything about the parameters. The result is likely to be a bad query plan, especially if the query is complex. The solution is to change <b>sql</b> language to <b>plpgsql</b> and make use of the <b>RETURN QUERY EXECUTE</b> command. Now the <b>myreportsubfunction</b> looks like the following:</p><pre class=\"brush:sql\">" + @"CREATE OR REPLACE FUNCTION myreportsubfunction(IN from timestamp without time zone, IN to timestamp without time zone)
  RETURNS TABLE(Name1 character varying, Name2 character varying) AS
$BODY$
BEGIN
RETURN QUERY EXECUTE
'select Column1 as Name1, Column2 as Name2
from sometable tbl
inner join ...
where ...
and ...
and $1 &lt;= somedate
and $2 &gt;= somedate
group by ...
order by ...;' USING $1, $2;
END
$BODY$
  LANGUAGE plpgsql VOLATILE" + "</pre><p>The function now takes as much time to run as the \"raw\" query, significantly improving the performance.</p><p><b>References</b></p><a href=\"http://www.postgresql.org/docs/9.1/static/plpgsql-statements.html\">39.5. Basic Statements</a><br/><a href=\"http://stackoverflow.com/questions/3342587/postgresql-slow-on-custom-function-php-but-fast-if-directly-input-on-psql-using\">Postgresql Slow on custom function, php but fast if directly input on psql using text search with gin index</a><br/>"
  + "by <a title= \"Evgeny\" rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_21032013_d = "Improving a performance of PostgreSQL function by utilizing the RETURN QUERY EXECUTE command";
        public const string content_21032013_k = "PostgreSQL query function performance return execute plan database";


        //Implementing a Simple Logging Engine with MVC 4
        public const string content_25032013_b = "<p>I was investigating the simple logging mechanism for the MVC application. First I came up with some requirements for the logging engine:</p><p><u>High-level requirements:</u><ul><li>Create logging engine that can log exceptions and other messages and store them in the database</li><li>Display a filterable grid of all messages</li></ul></p><p><u>Logging engine</u><ul><li>Should allow logging of exceptions, including handled and unhandled</li><li>Should allow logging of custom messages</li></ul></p><p><u>Filterable grid</u><ul><li>Should allow paged display of all exceptions and log messages in the database</li><li>Should allow the option to filter messages based on the date logged and severity</li></ul></p><p>I started with these simple classes that will allow handling of messages and exception:</p>";
        public const string content_25032013_r = "<pre class=\"brush:csharp\">" + @"//actual entry, each will correspond to a line in the grid
public class LogEntry
{
	public int Id { get; set; }
	public DateTime TimeStamp { get; set; }
	public string Path { get; set; }
	public string RawUrl { get; set; }
	public string Source { get; set; }
	public string Message { get; set; }
	public string StackTrace { get; set; }
	public string TargetSite { get; set; }

	public int TypeId { get; set; }
	public virtual LogType LogType { get; set; }
}

//a ""helper"" class for types like Warning, Information etc.
public class LogType
{
	public int LogTypeId { get; set; }
	public string Type { get; set; }
}

//finally, an enum of all supported log message types
public enum LogTypeNames
{
	All = 0,
	Info = 1,
	Warn = 2,
	Debug = 3,
	Error = 4,
	Fatal = 5,
	Exception = 6
}" + "</pre><p>These will be reflected in two database tables - the main table for saving all log messages, and a helper table to keep names of message severity levels.</p><pre class=\"brush:csharp\">" + @"public DbSet&lt;LogType&gt; LogTypes { get; set; }
public DbSet&lt;LogEntry&gt; LogEntries { get; set; }" + "</pre><p>Next, it is time to mention logging of handled and unhandled exceptions, which can be divided into handled and unhandled exceptions.</p><p><b>Develop mechanism for logging exceptions:</b></p><p><u>1. Log unhandled exceptions</u></p><p>Unhandled exceptions are, well, exceptions that are not handled in the source code. First, the site web.config must be modified to add a line in the <system.web> section: <customErrors mode=\"On\"/></p><p>Here's how it works: a method is called in <b>Global.asax</b> file:</p><pre class=\"brush:csharp\">" + @"public static void RegisterGlobalFilters(GlobalFilterCollection filters)
{
    filters.Add(new HandleErrorAttribute());
}" + "</pre><p>It registers the <b>HandleErrorAttribute</b> as global action filter. The HandleErrorAttribute checks the customErrors mode, and if it is off, shows the \"yellow screen of death\". If it is on, the <b></b>Error view is rendered and a <b>Model</b> is passed to it containing exceptions stack trace. Therefore, an <b>Error.cshtml</b> should be added under <b>Views/Shared</b>, and in its simplest form may look as follows</p><pre class=\"brush:xml\">" + @"@using Recipes.Logging
@using Recipes.Models
@{
    Layout = null;
    ViewBag.Title = ""Error"";
    Logger.WriteEntry(Model.Exception);
}

&lt;!DOCTYPE html&gt;
&lt;html&gt;
&lt;head&gt;
    &lt;title&gt;Error&lt;/title&gt;
&lt;/head&gt;
&lt;body&gt;
    &lt;h2&gt;
        Sorry, an error occurred while processing your request. The details of the error were logged.
    &lt;/h2&gt;
&lt;/body&gt;
&lt;/html&gt;" + "</pre><p>For simplicity, all log messages - exceptions, handled and unhandled, and all other custom messages - will be saved in a single database table.</p><p><u>2. Log handled exceptions</u></p><p>The handled exceptions are caught by code and handled directly. The following is the Logger class which handles exceptions and custom messages and saves them to the database:</p><pre class=\"brush:csharp\">" + @"public static class Logger
{
	public static void WriteEntry(Exception ex)
	{
		LogEntry entry = BuildExceptionLogEntry(ex);
		SaveLogEntry(entry);        
	}

	public static void WriteEntry(string mesage, string source, int logType)
	{
		LogEntry entry = BuildLogEntry(mesage, source, logType);
		SaveLogEntry(entry);
	}

	private static void SaveLogEntry(LogEntry entry)
	{
		using (RecipesEntities context = new RecipesEntities())
		{
			context.LogEntries.Add(entry);
			context.SaveChanges();
		}
	}

	private static LogEntry BuildLogEntry(string message, string source, int logType)
	{
		LogEntry entry = BuildLogEntryTemplate();

		entry.Message = message;
		entry.Source = source;
		entry.LogType = GetLogEntryType((LogTypeNames)logType);
		entry.TypeId = logType;

		return entry;
	}

	private static LogEntry BuildExceptionLogEntry(Exception x)
	{
		Exception logException = GetInnerExceptionIfExists(x);
		LogEntry entry = BuildLogEntryTemplate();

		entry.Message = logException.Message;
		entry.Source = logException.Source ?? string.Empty;
		entry.StackTrace = logException.StackTrace ?? string.Empty;
		entry.TargetSite = logException.TargetSite == null ? string.Empty : logException.TargetSite.ToString();
		entry.LogType = GetLogEntryType(LogTypeNames.Exception);
		entry.TypeId = (int) LogTypeNames.Exception;

		return entry;
	}

	private static LogEntry BuildLogEntryTemplate()
	{
		return new LogEntry
				   {
					   Path = HttpContext.Current.Request.Path,
					   RawUrl = HttpContext.Current.Request.RawUrl,
					   TimeStamp = DateTime.Now,
				   };
	}

	public static string BuildExceptionMessage(Exception x)
	{
		Exception logException = GetInnerExceptionIfExists(x);

		string strErrorMsg = Environment.NewLine + ""Error in Path :"" + HttpContext.Current.Request.Path;
		// Get the QueryString along with the Virtual Path
		strErrorMsg += Environment.NewLine + ""Raw Url :"" + HttpContext.Current.Request.RawUrl;
		// Get the error message
		strErrorMsg += Environment.NewLine + ""Message :"" + logException.Message;
		// Source of the message
		strErrorMsg += Environment.NewLine + ""Source :"" + logException.Source;
		// Stack Trace of the error
		strErrorMsg += Environment.NewLine + ""Stack Trace :"" + logException.StackTrace;
		// Method where the error occurred
		strErrorMsg += Environment.NewLine + ""TargetSite :"" + logException.TargetSite;
		return strErrorMsg;
	}

	private static LogType GetLogEntryType(LogTypeNames name)
	{
		return new LogType{LogTypeId = (int)name, Type = name.ToString()};
	}

	private static Exception GetInnerExceptionIfExists(Exception x)
	{
		if (x.InnerException != null)
			return x.InnerException;
		return x;
	}
}" + "</pre><p>With this basic structure in place, I can start adding user interface for displaying the log. I decided to only have two views, <b>Index</b> for the main grid which contains all log messages, and a <b>Details</b> for a detailed information about a single message. Details will be linked from the line in a grid that corresponds to a log message.</p><p><b>Index view.</b></p><p>The view will have several main parts, wrapped in a form.</p><pre class=\"brush:csharp\">" + @"@using (Html.BeginForm(""Index"", ""Logging"", new { CurrentPageIndex = 1 }, FormMethod.Get, new { id = ""myform"" }))
{

}" + "</pre><p>First is the div that shows the number of records found and gives an option to choose how many records per page will be displayed.</p><pre class=\"brush:xml\">" + @"&lt;div class=""grid-header""&gt;
    &lt;div class=""grid-results""&gt;
        &lt;div class=""inner""&gt;
            &lt;span style=""float: left""&gt;
                @string.Format(""{0} records found. Page {1} of {2}"", Model.LogEvents.TotalItemCount, Model.LogEvents.PageNumber, Model.LogEvents.PageCount)
            &lt;/span&gt;

            &lt;span style=""float: right""&gt;
                Show @Html.DropDownListFor(model =&gt; model.PageSize, new SelectList(FormsHelper.PagingPageSizes, ""Value"", ""Text"", Model.PageSize), new { onchange = ""document.getElementById('myform').submit()"" }) results per page
            &lt;/span&gt;
            
            &lt;div style=""clear: both""&gt;&lt;/div&gt;
        &lt;/div&gt; &lt;!-- inner --&gt;
    &lt;/div&gt;  &lt;!-- grid-results --&gt;
 &lt;/div&gt;  &lt;!-- grid-header --&gt;" + "</pre><p>The second allows to filter records by date logged and severity</p><pre class=\"brush:xml\">" + @" &lt;div class=""grid-filter""&gt;        
    &lt;div class=""inner""&gt;
        Level : @Html.DropDownList(""LogLevel"", new SelectList(FormsHelper.LogLevels, ""Value"", ""Text""))

        For : @Html.DropDownList(""Period"", new SelectList(FormsHelper.CommonTimePeriods, ""Value"", ""Text""))
        
        &lt;input id=""btnGo"" name=""btnGo"" type=""submit"" value=""Apply Filter"" /&gt;                      
    &lt;/div&gt;
 &lt;/div&gt;   " + "</pre><p>Next is the \"pager\" div, which allows navigation if multiple pages are reqiured</p><pre class=\"brush:xml\">" + @"  &lt;div class=""paging""&gt;
    &lt;div class=""pager""&gt;
        @Html.Pager(ViewData.Model.LogEvents.PageSize, ViewData.Model.LogEvents.PageNumber, ViewData.Model.LogEvents.TotalItemCount, new { LogType = ViewData[""LogType""], Period = ViewData[""Period""], PageSize = ViewData[""PageSize""] })
    &lt;/div&gt;
 &lt;/div&gt;" + "</pre><p>Finally, the main part is the actual grid which displays the messages.</p><pre class=\"brush:xml\">" + @" @if (Model.LogEvents.Count() == 0)
 {
 &lt;p&gt;No results found&lt;/p&gt;
 }
 else
 {
 &lt;div class=""grid-container""&gt;
 &lt;table class=""grid""&gt;
    &lt;tr&gt;
        &lt;th&gt;&lt;/th&gt;
        &lt;th&gt;#&lt;/th&gt;
        &lt;th&gt;Source&lt;/th&gt;
        &lt;th&gt;Date&lt;/th&gt;
        &lt;th style='white-space: nowrap;'&gt;Time ago&lt;/th&gt;
        &lt;th&gt;Message&lt;/th&gt;
        &lt;th&gt;Type&lt;/th&gt;
    &lt;/tr&gt;

 @{int i = 0;}
     @foreach (var item in Model.LogEvents)
     {
     &lt;tr class=""@(i++ % 2 == 1 ? ""alt"" : """")""&gt;
     &lt;td&gt;
        @Html.ActionLink(""Details"", ""Details"", new { id = item.Id.ToString(), loggerProviderName = ""Go To Details"" /*item.LoggerProviderName*/ })
     &lt;/td&gt;
     &lt;td&gt;
        @i.ToString()
     &lt;/td&gt;
     &lt;td&gt;
        @item.Source
     &lt;/td&gt;
     &lt;td style='white-space: nowrap;'&gt;
        @String.Format(""{0:g}"", item.TimeStamp.ToLocalTime())
     &lt;/td&gt;
     &lt;td style='white-space: nowrap;'&gt;
        @item.TimeStamp.ToLocalTime().TimeAgoString()
     &lt;/td&gt;
     &lt;td&gt;
        &lt;pre&gt;@item.Message.WordWrap(80)&lt;/pre&gt;
     &lt;/td&gt;
     &lt;td&gt;
        @item.LogType.Type
     &lt;/td&gt;
     &lt;/tr&gt;
     }

 &lt;/table&gt;
 &lt;/div&gt; &lt;!-- grid-container --&gt;
}" + "</pre><p>A few points of interest:</p><p>The <b>Index</b> method in the controller returns a <b>ViewModel</b>. By default, all configurable parameters (page size, time period, page number and log level) are not set, and all log messages are displayed with the default page size of 20 entries. When a value is set in the UI and the form is submitted, a corresponding parameter is passed to the controller.</p><pre class=\"brush:csharp\">" + @"public ActionResult Index(string Period = null, int? PageSize = null, int? page = null, string LogLevel = null)
{
	string defaultPeriod = Session[""Period""] == null ? ""All"" : Session[""Period""].ToString();
	string defaultLogLevel = Session[""LogLevel""] == null ? ""All"" : Session[""LogLevel""].ToString();

	LoggingIndexModel model = new LoggingIndexModel();

	model.Period = Period ?? defaultPeriod;
	model.LogLevel = LogLevel ?? defaultLogLevel;
	model.CurrentPageIndex = page.HasValue ? page.Value - 1 : 0;
	model.PageSize = PageSize.HasValue ? PageSize.Value : 20;

	TimePeriod timePeriod = TimePeriodHelper.GetUtcTimePeriod(model.Period);

	model.LogEvents = repository.GetByDateRangeAndType(model.CurrentPageIndex, model.PageSize, timePeriod.Start, timePeriod.End, model.LogLevel);

	ViewData[""Period""] = model.Period;
	ViewData[""LogLevel""] = model.LogLevel;
	ViewData[""PageSize""] = model.PageSize;

	Session[""Period""] = model.Period;
	Session[""LogLevel""] = model.LogLevel;

	return View(model);
}" + "</pre><p><b>GetByDateRangeAndType</b> does the work for selecting appropriate set of log messages from the database.</p><pre class=\"brush:csharp\">" + @"public IPagedList&lt;LogEntry&gt; GetByDateRangeAndType(int pageIndex, int pageSize, DateTime start, DateTime end, string logLevel)
{
	IQueryable&lt;LogEntry&gt; list;
	IPagedList&lt;LogEntry&gt; pagedList;

	list = db.LogEntries.Where(e =&gt;
			(e.TimeStamp &gt;= start && e.TimeStamp &lt;= end));

	if (logLevel != LogTypeNames.All.ToString())
	{
		list = list.Where(e =&gt; e.LogType.Type.ToLower() == logLevel.ToLower());
	}

	list = list.OrderByDescending(e =&gt; e.TimeStamp);
	pagedList = new PagedList&lt;LogEntry&gt;(list, pageIndex, pageSize);
	return pagedList;
}" + "</pre><p>The data is returned in the form of a <b>PagedList</b> which is implemented as follows:</p><pre class=\"brush:csharp\">" + @"public interface IPagedList&lt;T&gt; : IList&lt;T&gt;
{
	int PageCount { get; }
	int TotalItemCount { get; }
	int PageIndex { get; }
	int PageNumber { get; }
	int PageSize { get; }

	bool HasPreviousPage { get; }
	bool HasNextPage { get; }
	bool IsFirstPage { get; }
	bool IsLastPage { get; }
}" + "</pre><p>Main part of the <b>PagedList</b> class:</p><pre class=\"brush:csharp\">" + @"public class PagedList&lt;T&gt; : List&lt;T&gt;, IPagedList&lt;T&gt;
{
	public PagedList(IEnumerable&lt;T&gt; source, int index, int pageSize)
		: this(source, index, pageSize, null)
	{
	}

	#region IPagedList Members

	public int PageCount { get; private set; }
	public int TotalItemCount { get; private set; }
	public int PageIndex { get; private set; }
	public int PageNumber { get { return PageIndex + 1; } }
	public int PageSize { get; private set; }
	public bool HasPreviousPage { get; private set; }
	public bool HasNextPage { get; private set; }
	public bool IsFirstPage { get; private set; }
	public bool IsLastPage { get; private set; }

	#endregion

	protected void Initialize(IQueryable&lt;T&gt; source, int index, int pageSize, int? totalCount)
	{
		//### argument checking
		if (index &lt; 0)
		{
			throw new ArgumentOutOfRangeException(""PageIndex cannot be below 0."");
		}
		if (pageSize &lt; 1)
		{
			throw new ArgumentOutOfRangeException(""PageSize cannot be less than 1."");
		}

		//### set source to blank list if source is null to prevent exceptions
		if (source == null)
		{
			source = new List&lt;T&gt;().AsQueryable();
		}

		//### set properties
		if (!totalCount.HasValue)
		{
			TotalItemCount = source.Count();
		}
		PageSize = pageSize;
		PageIndex = index;
		if (TotalItemCount &gt; 0)
		{
			PageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
		}
		else
		{
			PageCount = 0;
		}
		HasPreviousPage = (PageIndex &gt; 0);
		HasNextPage = (PageIndex &lt; (PageCount - 1));
		IsFirstPage = (PageIndex &lt;= 0);
		IsLastPage = (PageIndex &gt;= (PageCount - 1));

		//### add items to internal list
		if (TotalItemCount &gt; 0)
		{
			AddRange(source.Skip((index) * pageSize).Take(pageSize).ToList());
		}
	}
}" + "</pre><p><b>PagedList</b> uses some helper methods from the <b>Pager</b> class to render HTML and generate links to other pages of the log.</p><pre class=\"brush:csharp\">" + @"public class Pager
{
	.....

	/// &lt;summary&gt;
	/// Rendes HTML to display a ""pager"" control (used at the top and bottom of the list of logged messages)
	/// &lt;/summary&gt;
	/// &lt;returns&gt;String of HTML&lt;/returns&gt;
	public string RenderHtml()
	{
		int pageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
		const int nrOfPagesToDisplay = 10;

		var sb = new StringBuilder();

		// Previous
		if (currentPage &gt; 1)
		{
			sb.Append(GeneratePageLink(""&lt;"", this.currentPage - 1));
		}
		else
		{
			sb.Append(""&lt;span class=\""disabled\""&gt;&lt;&lt;/span&gt;"");
		}

		int start = 1;
		int end = pageCount;

		if (pageCount &gt; nrOfPagesToDisplay)
		{
			int middle = (int)Math.Ceiling(nrOfPagesToDisplay / 2d) - 1;
			int below = (currentPage - middle);
			int above = (currentPage + middle);

			if (below &lt; 4)
			{
				above = nrOfPagesToDisplay;
				below = 1;
			}
			else if (above &gt; (pageCount - 4))
			{
				above = pageCount;
				below = (pageCount - nrOfPagesToDisplay);
			}

			start = below;
			end = above;
		}

		if (start &gt; 3)
		{
			sb.Append(GeneratePageLink(""1"", 1));
			sb.Append(GeneratePageLink(""2"", 2));
			sb.Append(""..."");
		}
		for (int i = start; i &lt;= end; i++)
		{
			if (i == currentPage)
			{
				sb.AppendFormat(""&lt;span class=\""current\""&gt;{0}&lt;/span&gt;"", i);
			}
			else
			{
				sb.Append(GeneratePageLink(i.ToString(), i));
			}
		}
		if (end &lt; (pageCount - 3))
		{
			sb.Append(""..."");
			sb.Append(GeneratePageLink((pageCount - 1).ToString(), pageCount - 1));
			sb.Append(GeneratePageLink(pageCount.ToString(), pageCount));
		}

		// Next
		if (currentPage &lt; pageCount)
		{
			sb.Append(GeneratePageLink(""&gt;"", (currentPage + 1)));
		}
		else
		{
			sb.Append(""&lt;span class=\""disabled\""&gt;&gt;&lt;/span&gt;"");
		}
		return sb.ToString();
	}

	/// &lt;summary&gt;
	/// Generates a link to a page
	/// &lt;/summary&gt;
	/// &lt;param name=""linkText""&gt;Text displayed on the page&lt;/param&gt;
	/// &lt;param name=""pageNumber""&gt;Number of the page the link leads to&lt;/param&gt;
	/// &lt;returns&gt;&lt;/returns&gt;
	private string GeneratePageLink(string linkText, int pageNumber)
	{
		var pageLinkValueDictionary = new RouteValueDictionary(linkWithoutPageValuesDictionary) {{""page"", pageNumber}};
		var virtualPathData = RouteTable.Routes.GetVirtualPath(this.viewContext.RequestContext, pageLinkValueDictionary);

		if (virtualPathData != null)
		{
			const string linkFormat = ""&lt;a href=\""{0}\""&gt;{1}&lt;/a&gt;"";
			return String.Format(linkFormat, virtualPathData.VirtualPath, linkText);
		}
		return null;
	}
}" + "</pre><p><b>Details view.</b></p><p>There is nothing special about the details view - it's a usual MVC view that displays entity data.</p><pre class=\"brush:xml\">" + @"@model Recipes.Models.LogEntry
@{
    ViewBag.Title = ""Details"";
}

&lt;link href=""@Url.Content(""~/Content/logging.css"")"" rel=""stylesheet"" type=""text/css"" /&gt;

&lt;h2&gt;Details&lt;/h2&gt;

&lt;p&gt;        
    @Html.ActionLink(""Back to List"", ""Index"")
&lt;/p&gt;

&lt;fieldset&gt;
    &lt;legend&gt;Fields&lt;/legend&gt;
        
    &lt;div class=""display-label""&gt;Id&lt;/div&gt;
    &lt;div class=""display-field""&gt;@Model.Id&lt;/div&gt;
        
    &lt;div class=""display-label""&gt;LogDate&lt;/div&gt;
    &lt;div class=""display-field""&gt;@String.Format(""{0:g}"", Model.TimeStamp)&lt;/div&gt;
        
    &lt;div class=""display-label""&gt;Source&lt;/div&gt;
    &lt;div class=""display-field""&gt;@Model.Source&lt;/div&gt;
        
    &lt;div class=""display-label""&gt;Type&lt;/div&gt;
    &lt;div class=""display-field""&gt;@Model.LogType.Type&lt;/div&gt;
        
    &lt;div class=""display-label""&gt;Message&lt;/div&gt;
    &lt;div class=""display-field""&gt;
        &lt;pre&gt;@Model.Message.WordWrap(80)&lt;/pre&gt;
    &lt;/div&gt;
        
    &lt;div class=""display-label""&gt;StackTrace&lt;/div&gt;
    &lt;div class=""display-field""&gt;@Model.StackTrace&lt;/div&gt;                      
        
&lt;/fieldset&gt;

&lt;p&gt;        
    @Html.ActionLink(""Back to List"", ""Index"")
&lt;/p&gt;" + "</pre><p>Details Controller</p><pre class=\"brush:xml\">" + @"public ActionResult Details(string loggerProviderName, string id)
{
	LogEntry logEvent = repository.GetById(id);

	return View(logEvent);
}" + "</pre><p>The final result is represented in the image below:</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2013/25032013_MVC_logging_engine.png\" alt=\"MVC logging engine\" /></div><p align=\"center\">MVC logging engine</p><p><b>References</b></p><a href=\"http://www.asp.net/web-forms/tutorials/deployment/deploying-web-site-projects/processing-unhandled-exceptions-cs\">Processing Unhandled Exceptions (C#)</a><br/><a href=\"http://dotnetdarren.wordpress.com/2010/07/27/logging-on-mvc-part-1/\">Logging in MVC Part 1- Elmah (and other posts of the series)</a><br/><a href=\"http://www.codeproject.com/Articles/545026/MVC-Basic-Site-Step-2-Exceptions-Management\">MVC Basic Site: Step 2 - Exceptions Management</a><br/><a href=\"http://stackoverflow.com/questions/11851328/how-is-error-cshtml-called-in-asp-net-mvc\">How is Error.cshtml called in ASP.NET MVC?</a><br/>" +
            "by <a title= \"Evgeny\" rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_25032013_d = "Implementing a simple logging engine with MVC 4";
        public const string content_25032013_k = "MVC logging C# exception message grid filter";

        //Improving a PostgreSQL report performance: Part 2 - Temporary Table
        public const string content_28032013_b =
            "<p>The report I was working on still did not live up to expectations. There was something else going on. I had to dig a little deeper.</p><p>The report was generated by <a href=\"http://www.devexpress.com/Products/NET/Reporting/\">XTraReports</a> and I have no authority to edit it. The ReportDataSource contains functions for retrieving datasets for main report and subreport.</p>";
        public const string content_28032013_r = "<pre class=\"brush:csharp\">" + @"public class ReportDataSource : BaseDataSource
{
	public DataSet GetPrimaryReportData(DateTime fromDate, DateTime toDate)
	{
		string commandText = ""select * from myreportfunction;"";
		var reportDataSet = ExecuteQuery(""ReportDataSet"", commandText, new[] { ""MainDataSet"" });
		return reportDataSet;
	}

	public DataSet GetSubReportData(DateTime fromDate, DateTime toDate)
	{
		string commandText = String.Format(""select * from myreportsubfunction"");
		return ExecuteQuery(""SubReportDataSet"", commandText, new[] { ""SubDataSet"" });
	}
}" + "</pre><p>And here's how the PostgreSQL functions looked like.</p><p>The <b>myreportsubfunction</b> is the one I worked on already so now it looked like the following</p><pre class=\"brush:sql\">" + @"CREATE OR REPLACE FUNCTION myreportsubfunction(IN from timestamp without time zone, IN to timestamp without time zone)
  RETURNS TABLE(Name1 character varying, Name2 character varying) AS
$BODY$
BEGIN
RETURN QUERY EXECUTE
'select Column1 as Name1, Column2 as Name2
from sometable tbl
inner join ...
where ...
and ...
and $1 &lt;= somedate
and $2 &gt;= somedate
group by ...
order by ...;' USING $1, $2;
END
$BODY$
  LANGUAGE plpgsql VOLATILE" + "</pre><p>And there was the <b>myreportfunction</b></p><pre class=\"brush:sql\">" + @"CREATE FUNCTION myreportfunction ( FROMdate timestamp without time zone, todate timestamp without time zone )
 RETURNS TABLE ( name character varying, somevalue1 integer, somevalue2 real ) AS $body$
 SELECT
    something,
    sum(somevalue1)::int as somevalue1,
    sum(somevalue2)::real as somevalue2
 FROM myreportsubfunction($1, $2)      group by something;
  $body$ LANGUAGE sql;" + "</pre><p>What's going on here? Well, looks like first the <b>myreportfunction</b> is called, it calls the <b>myreportsubfunction</b> and returns aggregated results. But then the <b>myreportsubfunction</b> is called separately and essentially executes the same huge query again! No wonder the performance is nowhere near acceptable. Anyway, to satisfy the report requirements, I need to have the aggregated data first, which means that I need to save the results of the huge complex query, aggregate the results and return them for the main report, and then return the saved results of the query as subreport, or \"detailed\" data. My approach is to use the temporary table.</p><p>Here is what the functions will do:</p><p><u>myreportfunction</u></p><p><ul><li>if a temptable exists, drop it</li><li>create a temptable</li><li>run a complex query and save results in the temptable</li><li>run a query that returns the aggregated data for the report</li></ul></p><p><u>myreportsubfunction</u></p><p><ul><li>if the temptable exists, return everything from the table, then drop the table</li></ul></p><p>And the resulting PostgreSQL code</p><p><u>myreportfunction</u></p><pre class=\"brush:sql\">" + @"CREATE OR REPLACE FUNCTION myreportfunction(IN fromdate timestamp without time zone, IN todate timestamp without time zone)
  RETURNS TABLE(""name"" character varying, somevalue1 character varying, somevalue2 character varying) AS
$BODY$
BEGIN
DROP TABLE IF EXISTS temptable;
CREATE TEMPORARY TABLE temptable(""name"" character varying, somevalue1 character varying, somevalue2 character varying);
DELETE FROM temptable;

EXECUTE '
insert into temptable(name, somevalue1, somevalue2)		
select Column1 as Name1, Column2 as Name2
from sometable tbl
inner join ...
where ...
and ...
and $1 &lt;= somedate
and $2 &gt;= somedate
group by ...
order by ...;' USING $1, $2;

RETURN QUERY EXECUTE 'SELECT name, somevalue1, somevalue2 FROM temptable group by name;';
END
$BODY$
  LANGUAGE plpgsql VOLATILE" + "</pre><p><u>myreportsubfunction</u></p><pre class=\"brush:sql\">" + @"CREATE OR REPLACE FUNCTION myreportsubfunction(IN timestamp without time zone, IN timestamp without time zone)
  RETURNS TABLE(name character varying, somevalue1 integer, somevalue2 real) AS
$BODY$
BEGIN
IF EXISTS (SELECT 1 FROM temptable) THEN 
	RETURN QUERY EXECUTE'select * from temptable';
	DELETE FROM temptable;
	DROP TABLE IF EXISTS temptable;
END IF;
END
$BODY$
  LANGUAGE plpgsql VOLATILE" + "</pre><p>Now hoping for the performance improvement by at least 50% ...</p><p><b>References</b></p><a href=\"http://www.postgresql.org/docs/9.2/static/sql-createtable.html\">CREATE TABLE</a><br/><a href=\"http://www.ynegve.info/Post/Display/174/improving-a-postgresql-report-performance-part-1-return-query-execute\">Improving a PostgreSQL report performance: Part 1 - RETURN QUERY EXECUTE</a><br/>" +
            "by <a title= \"Evgeny\" rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_28032013_d = "Improving a performance of PostgreSQL report by utilizing the temporary table";
        public const string content_28032013_k = "PostgreSQL query function performance temporary table plan database";


        //Project ROSALIND: Rabbits and Recurrence Relations
        public const string content_06042013_b = "<p>I came across the project <a href=\"http://rosalind.info\">ROSALIND</a> which is described as learning bioinformatics through problem solving. It is intriguing and well-designed, so I started with solving some introductory ones.</p><p>The first interesting problem was modified <a href=\"http://rosalind.info/problems/fib/\">Fibonacchi sequence</a>.  Actually, I did not know that the background of the Fibonacci sequence was modelling of rabbit reproduction. It assumed that rabbits reach reproductive age after one month, and that every mature pair of rabbits produced a pair of newborn rabbits each month. A modified problem, however, suggested that every mature pair of rabbits produced <i>k</i> pairs of newborn rabbits each month. The task is to calculate a total number of rabbit pairs after <i>n</i> months, assuming we have one pair of newborn rabbits at the start.</p><p>While the problem could be solved by recursion, the cost of calculation would be high. Every successive month the program would re-calculate the full solution for each previous month. A better approach is dynamic programming (which, in essence, is just remembering and reusing the already calculated values). Here is the modified solution in C#.</p>";
        
        public const string content_06042013_r = "<pre class=\"brush:csharp\">" + @"/// &lt;summary&gt;
/// Modified Fibonacchi problem: each rabbit pair matures in 1 month and produces ""pairs"" of newborn rabbit pairs each month
/// &lt;/summary&gt;
/// &lt;param name=""pairs""&gt;Number of newborn rabbit pairs produced by a mature pair each month&lt;/param&gt;
/// &lt;param name=""to""&gt;Number of months&lt;/param&gt;
/// &lt;returns&gt;Total number of rabbit pairs after ""to"" months&lt;/returns&gt;
static Int64 Fibonacci(int pairs, int to)
{
	if (to == 0)
	{
		return 0;
	}

	Int64 mature = 0;
	Int64 young = 1;

	Int64 next_mature;
	Int64 next_young;
	Int64 result = 0;
	for (int i = 0; i &lt; to; i++)
	{
		result = mature + young;

		next_mature = mature + young;
		next_young = mature * pairs;

		mature = next_mature;
		young = next_young;
	}
	return result;
}" + "</pre><p>Note: the result grows fast! When trying to use the default Int32 (32 bit, or up to ~2 billion) and calculate the result for 4 pairs and 32 months, the value overflowed at around month 23.</p><p>The next problem was another variation on the rabbit simulation. In this case, the rabbits are mortal and die after <i>k</i> months. My solution was to have a counter for rabbits of each age at each step. I keep the counters in the dictionary, where the key is the age of a rabbit pair and the value is the number of rabbit pairs of that age on that step.</p><pre class=\"brush:csharp\">" + @"/// &lt;summary&gt;
/// Mortal Rabbits Fibonacci sequence variation
/// &lt;/summary&gt;
/// &lt;param name=""months""&gt;How many months does the simulation run for&lt;/param&gt;
/// &lt;param name=""lifespan""&gt;Rabbit lifespan&lt;/param&gt;
/// &lt;returns&gt;A count of rabbit pairs alive at the end&lt;/returns&gt;
static UInt64 MortalRabbits(int months, int lifespan)
{
	Dictionary&lt;int, UInt64&gt; dRabbits = GetEmptyDictionary(lifespan);
	dRabbits[0]++;

	for (int i = 0; i &lt; months - 1; i++)
	{
		Dictionary&lt;int, UInt64&gt; newRabbits = GetEmptyDictionary(lifespan);
		foreach (KeyValuePair&lt;int, UInt64&gt; pair in dRabbits)
		{
			int age = pair.Key;

			if (age == 0)
			{
				newRabbits[1] = newRabbits[1] + dRabbits[age];
			}
			else if (age &gt; 0 && age &lt; lifespan - 1)
			{
				newRabbits[age + 1] = newRabbits[age + 1] + dRabbits[age];
				newRabbits[0] = newRabbits[0] + dRabbits[age];
			}
			else if (age == lifespan - 1)
			{
				newRabbits[0] = newRabbits[0] + dRabbits[age];
			}
		}
		dRabbits = newRabbits;
	}

	UInt64 count = 0;
	foreach (KeyValuePair&lt;int, UInt64&gt; pair in dRabbits)
	{
		count = count + pair.Value;
	}

	return count;
}

/// &lt;summary&gt;
/// Creates an dictionary where keys are integers from 0 to lifespan - 1, and all values are zeros
/// &lt;/summary&gt;
/// &lt;param name=""lifespan""&gt;&lt;/param&gt;
/// &lt;returns&gt;An empty dictionary&lt;/returns&gt;
static Dictionary&lt;int, UInt64&gt; GetEmptyDictionary(int lifespan)
{
	Dictionary&lt;int, UInt64&gt; dRabbits = new Dictionary&lt;int, UInt64&gt;();

	for (int i = 0; i &lt; lifespan; i++)
	{
		dRabbits.Add(i, 0);
	}
	return dRabbits;
}" + "</pre><p><b>References</b></p><a href=\"http://rosalind.info/\">Project ROSALIND</a><br/><a href=\"http://rosalind.info/problems/fib/\">Modified Fibonacci Problem</a><br/><a href=\"http://rosalind.info/problems/fibd/\">Mortal Fibonacci Rabbits</a><br/><a href=\"http://en.algoritmy.net/article/45658/Fibonacci-series\">Fibonacci Series</a><br/>" +
   "by <a title= \"Evgeny\" rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_06042013_d = "Solving some of the problems presented on the project ROSALIND website";
        public const string content_06042013_k = "ROSALIND, bioinformatics, fibonacci, problem, solve, c#, code";

        //Google as my automated testing tool
        public const string content_07042013_b = "<p>It is probably a well-known fact, but Google webmaster tools record errors when they crawl the website. Of course, first of all the website has to be submittet to Google. After that, the crawl errors can be accessed by selecting the website of interest and clicking <b>Health</b>. On the left side, <b>Crawl Errors</b> will be available.</p><p>I did not even know that this was available until I received an email from Google which informed me that there was an increase in server errors and provided me a link to review those errors.</p>";
        public const string content_07042013_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2013/06042013_Google_Webmaster_Tools.png\" alt=\"Google Webmaster Tools\" /></div><p align=\"center\">Google Webmaster Tools</p><p>Looks like the website was generating about ~70 errors when crawled, and I did not know. When the number of errors increased to ~90, I got an email. The errors are listed and can be marked as fixed as I deal with the root cause.</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2013/06042013_Website_Crawl_Errors.png\" alt=\"Website Crawl Errors\" /></div><p align=\"center\">Website Crawl Errors</p><p>Most errors were caused by my refactoring where I replaced direct access to the database with using the repository pattern, and did it carelessly.</p><p>Another reason was that I used a tool I found on the web to generate my website map and did not review it before uploading on the website. The map contained some links that were not supposed to be accessed directly. It is probably a good idea to review those links to verify they are really needed or can be replaced and if they are needed, remove them from the site map. I'll be tracking the errors from now on.</p><p><b>References</b></p><a href=\"https://www.google.com/webmasters/tools/home?hl=en\">Webmaster tools</a><br/><a href=\"http://support.google.com/webmasters/bin/answer.py?hl=en&answer=35120\">Crawl errors</a><br/><a href=\"http://www.xml-sitemaps.com/\">Sitemap Generator</a><br/>" + 
            "by <a title= \"Evgeny\" rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_07042013_d = "using google webmaster tools to identify server errors";
        public const string content_07042013_k = "google webmaster tool server error crawl website";

        //Customising Windows Installation
        public const string content_14052013_b = "<p>In some cases - for example, where the only purpose of the PC is to run a specific software package - certain Windows features are customised with the provider's brand. Such features may include logon screen background, individual desktop backgrounds for each user and user account logon pictures (tile images). Therefore it may be useful to know where those are stored and how to update them. The following description is for <b>Windows 7</b> and <b>Windows Server 2008</b>.</p>";
        public const string content_14052013_r = "<p><b>1. Windows logon screen background.</b></p><p>This is the easiest one. It should be copied to the following location: <b>C:\\Windows\\system32\\oobe\\info\\backgrounds\\backgroundDefault.jpg</b>. A registry key <b>HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Authentication\\LogonUI\\Background\\OEMBackground</b> should exist with a dword value of 00000001.</p><p><b>2. Desktop backgrounds (wallpaper) for each user</b></p><p>Check the following registry key: <b>HKEY_CURRENT_USER\\Control Panel\\Desktop\\Wallpaper</b>. What I found was the following: <b>C:\\Users\\Account_Name\\AppData\\Roaming\\Microsoft\\Windows\\Themes\\TranscodedWallpaper.jpg</b></p><p>Therefore, for each Account_Name the image has to be replaced with the desired one.</p><p><b>3. User Account logon pictures</b></p><p>This turned out to be trickier. There is a description on MSDN on how to do it manually [1]. Automating the task is not so obvious. Turns out, there is a function in the <b>shell32.dll</b> that sets a user login picture. It can be called easily from C# code by P/Invoke. In fact, the following is the full code of a console application that will update the user login picture.</p><pre class=\"brush:csharp\">" + @"using System;
        using System.Runtime.InteropServices;

        namespace UserPictureUpdater
        {
            class Program
            {
                [DllImport(""shell32.dll"", EntryPoint = ""#262"", CharSet = CharSet.Unicode, PreserveSig = false)]
                public static extern void SetUserTile(string username, int notneeded, string picturefilename);
                [STAThread]
                static void Main(string[] args)
                {
                    if (args.Length == 2)
                    {
                        SetUserTile(args[0], 0, args[1]);
                    }
                }
            }
        }" + "</pre><p>The application can run from command line</p><p><b>UserPictureUpdater Administrator adminnew.png</b></p><p>Or, in my case, I'm running it from <b>PowerShell</b> script the following way</p><p><b>Start-Process $command $params</b></p><p>Where <b>$command</b> is the full path, i.e. \"C:\\Folder\\UserPictureUpdater.exe\", and <b>$params</b> is the command line parameters, i.e. \"Administrator adminnew.png\".</p><p>Also, turns out someone figured out the way to do the whole thing in PowerShell [2]. But I was done with my approach by the time I found out.</p><p><b>References</b></p><a href=\"http://msdn.microsoft.com/en-us/library/bb776892.aspx\">About User Profiles</a><br/><a href=\"http://iammarkharrison.wordpress.com/2012/01/14/setting-the-user-tile-image-in-windows-7-and-server-2008-r2/\">Setting the user tile image in Windows 7 and Server 2008 R2</a><br/>" + "by <a title= \"Evgeny\" rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_14052013_d = "Customising Windows Installation - Logon Screen, Desktop Backgrounds, User account pictures icons";
        public const string content_14052013_k = "Windows PowerShell Login Registry Tile Desktop Background User Picture Icon Tile Account";

        //Optimising Windows Updates Installation
        public const string content_25062013_b = "<p>It's a good thing that I wrote about <a href=\"http://justmycode.blogspot.com.au/2012/08/installing-windows-updates-via-shell.html\">Installing Windows Updates via Shell Script</a> some time ago because today I needed to reuse that bit of a script and could not find it anywhere on my PC or our corporate network.</p><p>This time I'm reusing most of the functionality, but additionally do the following:</p><p><ul><li>Make sure that the Windows Update service is started</li><li>Run a PowerShell script that passes a folder where the Windows update files are stored to the VbScript file</li><li>Execute VbScript to install all updates in a folder</li><li>Repeat. (I want to keep my \"required\" and \"optional\" updates separate</li></ul></p>" ;
        public const string content_25062013_r = "<p>I was caught for a while trying to use PowerShell <b>Set-Service</b> and <b>Start-Service</b> commands and getting permission errors. I did not quite solve it, but found a simple workaround by utilising a command line:</p><pre class=\"brush:sql\">" + @"@ECHO OFF

sc config wuauserv start= auto
net start wuauserv" + "</pre><p>Next, the PowerShell script is used to pass parameters to VbScript:</p><pre class=\"brush:sql\">" + @"cscript .\Common\InstallUpdates.vbs $updatesFolder" + "</pre><p>Finally, the VbScript is almost the same as in the previous version, but note how the argument passed by PowerShell is parsed. The argument is the name of the folder where I placed the updates downloaded from <a href=\"http://catalog.update.microsoft.com/v7/site/DownloadInformation.aspx\">Microsoft Update Catalog</a></p><pre class=\"brush:csharp\">" + @"Set args = WScript.Arguments
sfolder = args.Item(0)

Dim objfso, objShell
Dim iSuccess, iFail
Dim files, folderidx, Iretval, return
Dim fullFileName

Set objfso = CreateObject(""Scripting.FileSystemObject"")
Set folder = objfso.GetFolder(sfolder)
Set objShell = CreateObject(""Wscript.Shell"")

With (objfso)
	If .FileExists(""C:\log.txt"") Then
		Set logFile = objfso.OpenTextFile(""C:\log.txt"", 8, TRUE)
	Else
		Set logFile = objfso.CreateTextFile(""C:\log.txt"", TRUE)
	End If
End With

Set files = folder.Files
iSuccess = 0
iFail = 0

For each folderIdx In files

fullFileName = sfolder & ""\"" & folderidx.name

 If Ucase(Right(folderIdx.name,3)) = ""MSU"" then
  logFile.WriteLine(""Installing "" & folderidx.name & ""..."")
  iretval=objShell.Run (""wusa.exe "" & fullFileName & "" /quiet /norestart"", 1, True)
  If (iRetVal = 0) or (iRetVal = 3010) then
   logFile.WriteLine(""Success."")
   iSuccess = iSuccess + 1
  Else
   logFile.WriteLine(""Failed."")
   iFail = iFail + 1
  End If
 ElseIf Ucase(Right(folderIdx.name,3)) = ""EXE"" Then
  logFile.WriteLine(""Installing "" & folderidx.name & ""..."")
  iretval = objShell.Run(fullFileName & "" /q /norestart"", 1, True)
  If (iRetVal = 0) or (iRetVal = 3010) then
   logFile.WriteLine(""Success."")
   iSuccess = iSuccess + 1
  Else
   logFile.WriteLine(""Failed."")
   iFail = iFail + 1
  End If
 End If
Next
 
wscript.echo iSuccess & "" update(s) installed successfully and "" & iFail & "" update(s) failed. See C:\log.txt for details.""" + "</pre><p>Disable the Windows Update service again if necessary</p><pre class=\"brush:sql\">" + @"net stop wuauserv
sc config wuauserv start= disabled" + "</pre><p><b>References:</b></p><a href=\"http://www.jasonn.com/enable_windows_services_command_line\">Managing Windows Services from the command line</a><br/><a href=\"http://technet.microsoft.com/en-us/library/ee156618.aspx\">Working with Command-Line Arguments</a><br/><a href=\"http://stackoverflow.com/questions/13859858/how-do-you-pass-a-variable-to-vbs-script-in-a-powershell-command\">How do you pass a variable to VBS script in a powershell command?</a><br/><a href=\"http://support.risualblogs.com/blog/2011/06/13/enabledisable-a-service-via-powershell/\">Enable/Disable a Service via PowerShell</a><br/><a href=\"http://gallery.technet.microsoft.com/scriptcenter/PowerShell-queryService-94ecfac6\">PowerShell queryService – Wait for a Dependency Starting Service</a><br/>" + "by <a title= \"Evgeny\" rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_25062013_d = "Automatic installation of windows updates with the help of PowerShell and a VbScript";
        public const string content_25062013_k = "Windows Updates PowerShell VbScript autmated script";

        //Final Hurdles While Installing Windows Updates
        public const string content_29062013_b = "<p>There are three different PC models here, all configured in the same way, all have to have Windows Updates installed on them automatically. However, while two of the models were happily updated, the third decided it does not like some of the updates and presented me with the screen similar to the following:</p>";
        public const string content_29062013_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2013/29062013_Windows_Updates_errors_80073712_643.png\" alt=\"Windows Updates errors 80073712 643\" /></div><p align=\"center\">Windows Updates errors 80073712 643</p><p>Turns out these two error codes are quite known problems.</p><p>The first one is solved, among some other options, by downloading and installing a specific update: , which is over 300MB in size. So in my case I had to do a check for the PC model by reading it via PowerShell</p><pre class=\"brush:csharp\">" + @"$strModel = (Get-WmiObject Win32_ComputerSystem).Model.Trim()" + "</pre><p>and if the model was the \"weird\" one, install this update.</p><p>The second one is solved by repairing the .NET Framework 4 installation. Fortunately, this can be done either silently or with unattended option. All in all, the fix for my two problems was applied as the following addition to the script and, fortunately, no additional restarts were required and after running this bit I could proceed to install updates as per my previous post.</p><pre class=\"brush:csharp\">" + @"if($strModel -Like ""*WeirdModel*"")
{
	Write-Host ""Verifying .NET Framework 4 ...""
	Start-Process ""C:\Windows\Microsoft.NET\Framework64\v4.0.30319\SetupCache\Client\setup.exe"" ""/repair /x86 /x64 /ia64 /parameterfolder Client /passive /norestart"" -Wait
	Write-Host ""Done.""
	Write-Host ""Installing System Update Readiness Tool ...""
	$readinessTool = Join-Path $win7Folder ""Windows6.1-KB947821-v27-x64.msu""
	$toolCommand = $readinessTool + "" /quiet /norestart""
	Write-Host $toolCommand
	Start-Process ""wusa.exe"" $toolCommand -Wait
	Write-Host ""Done.""
}" + "</pre><p><b>References:</b></p><a href=\"http://support.microsoft.com/kb/957310?wa=wsignin1.0\">Error Code 0x80073712 occurs in Windows Update or Microsoft Update</a><br/><a href=\"http://www.microsoft.com/en-au/download/details.aspx?id=3132\">System Update Readiness Tool for Windows 7 (KB947821) [May 2013] </a><br/><a href=\"http://support.microsoft.com/kb/976982\">Error codes “0x80070643” or “0x643” occur when you install the .NET Framework updates</a><br/><a href=\"http://blogs.msdn.com/b/astebner/archive/2009/04/16/9553804.aspx\">Silent install, repair and uninstall command lines for each version of the .NET Framework</a><br/>" + "by <a title= \"Evgeny\" rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_29062013_d = "Fixing some windows updates issues";
        public const string content_29062013_k = "Windows updates powershell system update readiness tool";

        //In SQL Query, Only Return Every n-th Record
        public const string content_09072013_b = "<p>A little SQL trick that helps in some cases. In my case, I wanted to select some data that is logged into the table every several seconds, and then quickly plot it in Excel, but over a date range of a few months. So, I run a query</p>";
        public const string content_09072013_r = "<pre class=\"brush:sql\">" + @"SELECT ""timestamp"", valueiwant
FROM mytable
order by timestamp" + "</pre><p>And that potentially leaves me with thousands or hundreds of thousands of rows. However, to visualise a trend over time, I don't need to plot each and every value on the graph. I'll be happy with 1/10 or even 1/100 of records. Here is how I can use <b>ROW_NUMBER</b> to achieve that.</p><pre class=\"brush:sql\">" + @"SELECT * FROM
(
	SELECT ""timestamp"", instrumenttimestamp, ROW_NUMBER() OVER (order by timestamp) AS rownum
    FROM mytable
	order by timestamp
) AS t
WHERE t.rownum % 25 = 0" + "</pre><p>Row number returns the sequential number of a row in the result set. The <b>WHERE</b> clause then checks if the number is a multiple of 25, therefore only rows 25, 50, 75, 100 etc. will be returned by the outer query.</p><p><b>References</b></p><a href=\"http://msdn.microsoft.com/en-us/library/ms186734.aspx\">ROW_NUMBER (Transact-SQL)</a><br/><a href=\"http://stackoverflow.com/questions/4799816/return-row-of-every-nth-record\">Return row of every n'th record</a><br/>" + "by <a title= \"Evgeny\" rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_09072013_d = "How to select every nth record from SQL result set";
        public const string content_09072013_k = "SQL SELECT every nth record ROW_NUMBER";

        //A Little Strangeness in the Way A Script is Run by Registry
        public const string content_14072013_b = "<p>An observation from today.</p><p>Currently <b>net.exe</b> is executed when the user logs in and maps a network drive (may also use credentials based on the user, etc.). This is achieved as follows:</p>";
        public const string content_14072013_r = "<p><b>Scenario 1. Normal conditions</b></p><p>Under the registry key <b>HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Run</b>. Create a string value name <b>MountDrive</b> and data <b>net.exe use b: /persistent:no \\\\localhost\\mydrive</b></p><p>Behaviour: Command runs, drive is mapped.</p><p>Now suppose command fails occasionally. To understand why, I would like to redirect the output of <b></b>net.exe into the text file. That should be easy to do.</p><p><b>Scenario 2. Modified Value in Registry</b></p><p>I modified the registry entry to</p><p><b>net.exe use b: /persistent:no \\\\localhost\\mydrive >>C:\\netlog.txt</b></p><p>Behaviour: Command runs, drive is mapped, but no output file. If I run the same command from command line, the drive is mapped <b>and</b> output file is created (as expected).</p><p>Next, I came up with a workaround</p><p><b>Scenario 3. Run a cmd file from Registry.</b></p><p><ul><li>create <b>map.cmd</b> with the contents <b>net.exe use b: /persistent:no \\\\localhost\\mydrive >>C:\\netlog.txt</b></li><li>place it on <b>C:</b> drive</li><li>modify the registry entry to <b>C:\\map.cmd</b></li></ul></p><p>Behaviour: Command runs, drive is mapped, output file is created with the contents <b>The command completed successfully</b>. Why does it behave like that - I don't know.</p>" + "by <a title= \"Evgeny\" rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
        public const string content_14072013_d = "running net.exe from the registry key and redirecting output to a text file";
        public const string content_14072013_k = "net.exe registry regedit32 script text file log";

        //Using Active Setup to Update Anything in HKEY_CURRENT_USER
public const string content_12082013_b = "<p>Following my last post, I had next to make sure that every user's entry in the registry was updated, and that change had to be scripted. This turned out to be a non-trivial task and took some research. First of all, the entry is located in <b>HKEY_CURRENT_USER</b> registy hive. Therefore, being logged in as Admin I cannot directly set an entry for Bob because Bob is not the current user at the moment. Then what can I do? The <b>HKEY_CURRENT_USER</b> is a kind of shortcut to <b>HKEY_USERS</b>. Under <b>HKEY_USERS</b> I can see the following structure</p>";
public const string content_12082013_r = "<pre class=\"brush:sql\">" + @"HKEY_USERS\.DEFAULT
HKEY_USERS\S-1-5-18
HKEY_USERS\S-1-5-19
HKEY_USERS\S-1-5-20
HKEY_USERS\S-1-5-21-0123456789-012345678-0123456789-1004
HKEY_USERS\S-1-5-21-0123456789-012345678-0123456789-1004_Classes" + "</pre><p>The first 4 entries correspond to built-in system accounts, and the rest are real user accounts on a PC. So, one way to make the change I need is to loop through all users and make the changes as requested. Someone even <a href=\"http://micksmix.wordpress.com/2012/01/13/update-a-registry-key-for-all-users-on-a-system/\">wrote a VB script which does exactly that</a>. My case is a bit different, though. I only have a small handful of users, but the change I'm making in the registry key depends on the user. So, maybe I can map a username to the registry key.</p><p>If I run the following from the command line <b>wmic useraccount get name,sid</b>, I will see a table similar to the following</p><pre class=\"brush:sql\">" + @"Name            SID
Administrator   S-1-5-21-1180699209-877415012-3182924384-500
Guest           S-1-5-21-1180699209-877415012-3182924384-501
Tim             S-1-5-21-1180699209-877415012-3182924384-1004" + "</pre><p>Great. Now I can script my change and run it. However - it does not work. It appears that user hives are usually only loaded for currently logged in users. That complicates things.</p><p>Fortunately, I came across the alternative solution - use <b>Active Setup</b>. It's original use is, likely, to check if a specific version of the software is installed to help installers to install, uninstall and repair software. It can, however, be used to write pretty much anything in the HKCU of the user who logs on. Here's how it works:</p><p>When the user logs on, the following registry key is checked: <b>HKCU\\Software\\Microsoft\\Active Setup\\Installed Components\\<UID></b></p><p>If the HKCU key is not found in the registry then the contents of the string value <b>StubPath</b> is executed. This is essentially all that's important, so here is my example.</p><pre class=\"brush:sql\">" + @"reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Active Setup\Installed Components\MountDrive"" /v ""Version"" /d ""1"" /t REG_SZ /f

reg add ""HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Active Setup\Installed Components\MountDrive"" /v ""StubPath"" /d ""reg add HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run /v ""MountDrive"" /d ""C:\map.cmd"" /t REG_SZ /f"" /f" + "</pre><p>Or, translating the <b>reg add</b> commands into PowerShell script</p><pre class=\"brush:sql\">" + @"$mapcmd = ""C:\map.cmd""
$regKey = ""HKLM:\SOFTWARE\Microsoft\Active Setup\Installed Components\MountDrive""
New-Item -path $regKey | Out-Null
$regKey = ""HKLM:\SOFTWARE\Microsoft\Active Setup\Installed Components\MountDrive""
$regName = ""Version""
$value = ""1""
New-ItemProperty -path $regKey -name $regName -value $value | Out-Null
$regName = ""StubPath""
$value =""reg add HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run /v MountDrive /d $mapcmd /f""
New-ItemProperty -path $regKey -name $regName -value $value | Out-Null" + "</pre><p>Here's what happens when the user logs on:<br><ul><li>HKCU\\Software\\Microsoft\\Active Setup\\Installed Components\\MountDrive is checked. There is nothing there.</li><li>string value in StubPath is executed. The value is \"reg add\" command and it creates a MountDrive string under Run key, with a value \"C:\\map.cmd\". Therefore, this cmd script will run on user logon.</li><li>Also, a Version entry is created in HKCU\\Software\\Microsoft\\Active Setup\\Installed Components\\MountDrive with a value of 1.</li><li>Next time the user logs on, step 1 find the Version entry, thefore no actions will be performed.</li></ul></p><p>Seems a little complicated, but after running once and observing the changes as they are made in the registry, it becomes clear.</p><p><b>References:</b></p><a href=\"http://micksmix.wordpress.com/2012/01/13/update-a-registry-key-for-all-users-on-a-system/\">Update a registry key for ALL users on a system</a><br/><a href=\"http://pcsupport.about.com/od/termshm/g/hkey_users.htm\">HKEY_USERS</a><br/><a href=\"http://pcsupport.about.com/od/termss/g/security-identifier.htm\">Security Identifier</a><br/><a href=\"http://pcsupport.about.com/od/registry/ht/find-user-security-identifier.htm\">How To Find a User's Security Identifier (SID) in Windows</a><br/><a href=\"http://wpkg.org/Adding_Registry_Settings#Adding_entries_to_HKCU_for_all_users\">Adding Registry Settings</a><br/>" + "by <a title= \"Evgeny\" rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
public const string content_12082013_d = "A neat way to update, create or delete a registry key and value that is set in the HKEY_CURRENT_USER registry hive.";
public const string content_12082013_k = "Windows registry HKEY_CURRENT_USER active setup key value create delete update";

public const string content_26122013_b = "<p>A popular problem to introduce dynamic programming is the minimal change problem. Suppose a cashier needs to give me a certain amount of change and wants to do it with the minimal amount of coins possible. The input is a set of denominations and the amount, and the output is the set of coins.</p><p>For example, I may need to give 45 cents change and available coins are 1, 5 and 20. A solution intuitively is 20 + 20 + 5, but what is the best way to achieve it?</p>";
public const string content_26122013_r = "<p>A recursive solution may be the first to try. A minimal collection of coins definitely belongs to the following set:</p><p><ul><li>minimal collection of coins totalling 44 cents, plus 1 cent coin</li><li>minimal collection of coins totalling 40 cents, plus 5 cent coin</li><li>minimal collection of coins totalling 25 cents, plus 20 cent coin</li></ul></p><p>So on the next step we will apply the same logic to our new change amounts: 44, 40 and 25. This looks like a classic recursion problem, which can be illustrated by the following image </p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2013/26122013_Solving_Minimum_Change_with_Recursion.png\" alt=\"Solving Minimum Change with Recursion\" /></div><p align=\"center\">Solving Minimum Change with Recursion</p><p>and described by the following algorithm / pseudocode (where |coins| is the number of denominations available)</p><pre class=\"brush:csharp\">" + @"RECURSIVECHANGE(money, coins)
	if money = 0
		return 0
	MinNumCoins ← ∞
	for i ← 1 to |coins|
		if money ≥ coini
			NumCoins ← RECURSIVECHANGE(money − coini, coins)
			if NumCoins + 1 &lt; MinNumCoins
				MinNumCoins** ← NumCoins + 1
	 output MinNumCoins*" + "</pre><p>This should work, but is there something wrong with this approach? Well, one can see that the recursive algorithm will calculate the full solution for 19 cents 6 times on the third step only, and it will only get worse on the following steps. If the input value is large enough, the memory and time required to compute the solution will be huge. So, this is a classic example of the benefits of dynamic programming. I came across dynamic programming a few times before, but just couldn't properly figure it out.</p><p>Finally I found a good explanation. It came as a part of a free course in Bioinformatics Algorithms. Here's how it goes:</p>The key to dynamic programming is to take a step that may seem counterintuitive. Instead of computing <b>MinNumCoins</b>(m) for every value of m from 45 downward toward m = 1 via recursive calls, we will invert our thinking and compute <b>MinNumCoins</b>(m) from m = 1 upward toward 45, storing all these values in an array so that we only need to compute <b>MinNumCoins</b>(m) once for each value of m. <b>MinNumCoins</b>(m) is still computed via the same recurrence relation.</p><p><b>MinNumCoins</b>(m) = min{<b>MinNumCoins</b>(m − 20) + 1, <b>MinNumCoins</b>(m - 5) + 1, <b>MinNumCoins</b>(m - 1) + 1}</p><p>For example, assuming that we have already computed <b>MinNumCoins</b>(m) for m < 6, <b>MinNumCoins</b>(6) is equal to one more than the minimum of <b>MinNumCoins</b>(6 - 5) = 1 and <b>MinNumCoins</b>(6 - 1) = 5. Thus, <b>MinNumCoins</b>(6) is equal to 1 + 1 = 2.</p><p>This translates into the following algorithm / pseudocode</p><pre class=\"brush:csharp\">" + @"DPCHANGE(money, coins)
 MinNumCoins(0) ← 0
 for m ← 1 to money
        MinNumCoins(m) ← ∞
        for i ← 1 to |coins|
            if m ≥ coini
                if MinNumCoins(m - coini) + 1 &lt; MinNumCoins(m)
                    MinNumCoins(m) ← MinNumCoins(m - coini) + 1
    output MinNumCoins(money)" + "</pre><p>And a further C# implementation, which takes a comma-separated string of denominations available, and the target amount.</p><pre class=\"brush:csharp\">" + @"public static void DPCHANGE(int val, string denoms)
{
	int[] idenoms = Array.ConvertAll(denoms.Split(','), int.Parse);
	Array.Sort(idenoms);
	int[] minNumCoins = new int[val + 1];

	minNumCoins[0] = 0;
	for (int m = 1; m &lt;= val; m++)
	{
		minNumCoins[m] = Int32.MaxValue - 1;
		for (int i = 1; i &lt;= idenoms.Count() - 1; i++)
		{
			if (m &gt;= idenoms[i])
			{
				if (minNumCoins[m - idenoms[i]] + 1 &lt; minNumCoins[m])
				{
					minNumCoins[m] = minNumCoins[m - idenoms[i]] + 1;
				}
			}
		}
	}
}" + "</pre><p><b>References</b></p><a href=\"https://www.coursera.org/course/bioinformatics\">Bioinformatics Algorithms</a><br/><a href=\"https://beta.stepic.org/Bioinformatics-Algorithms-2/An-Introduction-to-Dynamic-Programming-The-Change-Problem-243/#step-6\">An Introduction to Dynamic Programming: The Change Problem</a><br/>" + "by <a title= \"Evgeny\" rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
public const string content_26122013_d = "Understanding dynamic programming on the example of a minimal change problem. When can dynamic programming work better than recursion.";
public const string content_26122013_k = "Dynamic programming minimal change algorithms c#";

//Manhattan Tourist problem
public const string content_11032014_b = "<p>An introductory exercise to aligning amino acid sequences is the Manhattan tourist problem. Suppose a tourist starts his route in the top left corner of the map and wants to visit as many attractions on the way as possible, and finish in the down right corner. There is a restriction however - he can only move either to the right or down.</p>";
public const string content_11032014_r = "<div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2014/11032014_A_Simplified_Map_of_Manhattan.png\" alt=\"A Simplified Map of Manhattan\" /></div><p align=\"center\">A Simplified Map of Manhattan</p><p>To describe the problem in numbers and data structures, the map is converted to the directed grid shown below. A tourist can move along the edges of the grid and the score of 1 is added if he passes an attraction, which are assigned to the bold edges. Of all possible paths we are interested in the one with the highest score.</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2014/11032014_Map_of_Manhattan_as_a_Graph.png\" alt=\"Map of Manhattan as a Graph\" /></div><p align=\"center\">Map of Manhattan as a Graph</p><p>The solution to this problem is equivalent to a more general problem. This time every \"street\", or the edge of the graph, is assigned a score. The goal is to find a path which gains the highest score.</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2014/11032014_Generic_Manhattan_Problem.png\" alt=\"Generic Manhattan Problem\" /></div><p align=\"center\">Generic Manhattan Problem</p><p>The problem can be brute forced, of course, by calculating the score for all possible paths. This is not practical for the larger graphs of course. A better approach is to calculate the highest possible score at every intersection, starting from the top left corner. Here is how the logic goes: If the tourist goes only towards the left in the grid, or only down, there are no choices for him. So it is easy to calculate the highest scores for the top and left row of intersections. Now we have these scores calculated:</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2014/11032014_Generic_Manhattan_Problem_-_First_Row_and_Column_Solved.png\" alt=\"Generic Manhattan Problem - First Row and Column Solved\" /></div><p align=\"center\">Generic Manhattan Problem - First Row and Column Solved</p><p>Now when that's done, we can proceed to the next row. How can the tourist get to intersection (1, 1)? He can either go to (0, 1) first, and gain 3 + 0 = 3 score, or go to (1, 0) and gain 1 + 3 = 4 score. Therefore, the maximum score he can gain at (1, 1) is 4. </p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2014/11032014_Generic_Manhattan_Problem_-_Cell_(1,1)_Solved.png\" alt=\"Generic Manhattan Problem - Cell (1,1) Solved\" /></div><p align=\"center\">Generic Manhattan Problem - Cell (1,1) Solved</p><p>The generic formula to use is quite intuitive: To reach an intersection, you have to arrive from one of two possible previous locations, and traverse a corresponding edge. You choose the path where the sum of the score of a previous location, plus the score of the edge, is higher.</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2014/11032014_Generic_Manhattan_Formula.png\" alt=\"Generic Manhattan Formula\" /></div><p align=\"center\">Generic Manhattan Formula</p><p>We continue calculations along the column all the way down. After that, we can move on to the next row.</p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2014/11032014_Generic_Manhattan_Problem_-_Second_Column_Solved.png\" alt=\"Generic Manhattan Problem - Second Column Solved\" /></div><p align=\"center\">Generic Manhattan Problem - Second Column Solved</p><p>Following this logic, it is easy to compute the maximum score for each intersection and find out the maximum score that can be gained while traversing Manhattan. </p><div class=\"separator\" style=\"clear: both; text-align: center;\"><img src=\"../../../Content/images/blog/pr/2014/11032014_Generic_Manhattan_Problem_-_Fully_Solved.png\" alt=\"Generic Manhattan Problem - Fully Solved\" /></div><p align=\"center\">Generic Manhattan Problem - Fully Solved</p><p>Along the way the algorithm also becomes clear: the maximum score at any intersection is the maximum of the following two values:</p><pre class=\"brush:csharp\">" + @"MANHATTANTOURIST(n, m, down, right)
	s0, 0 ← 0
	for i ← 1 to n
		si, 0 ← si-1, 0 + downi, 0
	for j ← 1 to m
		s0, j ← s0, j−1 + right0, j
	for i ← 1 to n
		for j ← 1 to m
			si, j ← max{si - 1, j + downi, j, si, j - 1 + righti, j}
	return sn, m" + "</pre><p>The following C# function implements the algorithm and returns the highest possible score, assuming we input all the scores for the edges towards the right and all the edges pointing down in the form of two-dimension arrays.</p><pre class=\"brush:csharp\">" + @"public static int ManhattanProblem(int[,] RightMatrix, int[,] DownMatrix)
{
	int n = RightMatrix.GetLength(0) + 1;
	int m = DownMatrix.GetLength(1) + 1;
	int[,] ManhattanMatrix = new int[n, m];

	ManhattanMatrix[0, 0] = 0;

	for (int i = 1; i &lt;= n; i++)
	{
		ManhattanMatrix[i, 0] = ManhattanMatrix[i - 1, 0] + DownMatrix[i - 1, 0];
	}

	for (int j = 1; j &lt;= m; j++)
	{
		ManhattanMatrix[0, j] = ManhattanMatrix[0, j - 1] + RightMatrix[0, j - 1];
	}

	for (int i = 1; i &lt;= n; i++)
	{
		for (int j = 1; j &lt;= m; j++)
		{
			ManhattanMatrix[i, j] =
				Math.Max(ManhattanMatrix[i - 1, j] + DownMatrix[i - 1, j], 
				ManhattanMatrix[i, j - 1] + RightMatrix[i, j - 1]);
		}
	}

	return ManhattanMatrix.Cast&lt;int&gt;().Max();
}" + "</pre><p>Understanding this problem turns out to be important to understanding sequence alignment in bioinformatics.</p>" + "by <a title= \"Evgeny\" rel=\"author\" href=\"https://plus.google.com/112677661119561622427?rel=author\" alt=\"Google+\" title=\"Google+\">Evgeny</a>";
public const string content_11032014_d = "A manhattan tourist problem is a fun and easy introduction into sequence alignment";
public const string content_11032014_k = "Bioinformatics sequence alignment manhattan tourist problem";    
    }
}