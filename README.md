# RedditTopMemes
Web app to show Reddit top 20 memes and send text file to Telegram group using .NET Core and Angular. 

## Running on Local
1. Clone project to local
2. Open **aspnet-core/RedditTopMemes.sln** with Visual Studio
3. Change **ConnectionStrings** if necessary at **appsettings.json**
4. Run migrations  
a. Open **Package Manager Console**  
b. Change to **RedditTopMemes.EntityFrameworkCore** in Default Project  
c. Type **Update-Database** and press Enter  
d. When **Done** message appear, select **RedditTopMemes.Web.Host** as default project and run with **IIS Express**  
5. **Swagger UI** should now run in browser.
6. For frontend, it is tested on Node.js **14.20.0**
7. Open **angular** folder in VS Code or in Terminal
8. Run **npm-install**, then **npm-start**
9. Angular should run in http://localhost:4200


## How to use
1. Naviage to http://localhost:4200
2. At login, **Username**: admin, **Password**: 123qwe
3. Click **Get Current Top 20** to see the memes
4. Get report file on telegram by clicking **Send to Telegram**
5. Click on **Rank Histories** to visualize how particular meme is progressing over time

## Telegram
1. Create new group on telegram and add **TopRedditMemesBot** to group
2. Send **/hello** to group so that system will auto-detect **ChatId** of group
3. Click **Send to Telegram**. Report file should arrive after short time
4. Alternatively, custom telegram chat bot and group ids can be configured at **Telegram** section of **appsettings.json**
