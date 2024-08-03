# Midjourney Proxy

[中文](README.md) | **English**

Proxy for Midjourney's Discord channels, enabling API-based AI drawing calls, a public benefit project offering free use of drawing APIs.

Fully open-source without any partially open or closed source components, pull requests are welcome.

The most feature-rich, secure, and memory-efficient (100MB+) Midjourney Proxy API~~

## 交流群

Due to the current documentation not being fully comprehensive, there may be issues with usage and deployment. You are welcome to join our discussion group to talk about and solve problems together.

[Midjourney公益群](https://qm.qq.com/q/k88clCkyMS)（QQ group number：565908696）

<img src="./docs/screenshots/565908696.png" alt="welcome" width="360"/>

## Key Features

- [x] Supports Imagine commands and related actions [V1/V2.../U1/U2.../R]
- [x] Supports adding images in base64 format during Imagine calls
- [x] Supports Blend (image mixing) and Describe (image-to-text) commands
- [x] Real-time progress tracking of tasks
- [x] Chinese prompt translation, requires Baidu Translate configuration
- [x] Pre-check for sensitive words in prompts, with support for adjustments
- [x] User-token connection via wss, enabling retrieval of error messages and full functionality
- [x] Supports Shorten (prompt analysis) command
- [x] Supports movement focus: Pan ⬅️➡⬆️⬇️
- [x] Supports local redraw: Vary (Region) 🖌
- [x] Supports almost all related button actions
- [x] Image zoom customization: Zoom 🔍
- [x] Retrieval of image seed values
- [x] Speed mode settings per account: RELAX | FAST | TURBO
- [x] Multi-account configuration, each with its task queue, supports account selection modes: BestWaitIdle | Random | Weight | Polling
- [x] Persistent account pool, dynamically maintained
- [x] Access to account /info, /settings
- [x] Account settings adjustments
- [x] Supports niji・journey Bot and Midjourney Bot
- [x] zlib-stream for secure transmission <https://discord.com/developers/docs/topics/gateway>
- [x] Embedded MJ management webpage, multi-language support <https://github.com/trueai-org/midjourney-proxy-webui>
- [x] MJ account management functionalities: add, delete and modify
- [x] Support for detailed information inquiry and account synchronization operations for MJ accounts.
- [x] Concurrent queue settings for MJ accounts
- [x] MJ account settings configuration
- [x] MJ task query support
- [x] Comprehensive drawing test page available
- [x] Compatible with mainstream drawing clients and API calls in the market.
- [x] Tasks can include parent task information
- [x] 🎛️ Remix mode and auto-submit in 🎛️ Remix mode
- [x] Built-in image saving to local storage and built-in CDN acceleration
- [x] automatically simulate reading the unread messages if there are too many unread messages
- [x] Support for PicReader and Picread commands for image-to-text regeneration, as well as batch image-to-text regeneration commands (no need for fast mode)
- [x] Support for BOOKMARK and other commands
- [x] Support for specifying instance drawing, filtering accounts for drawing at specific speeds, and filtering accounts for drawing in `remix` mode, etc. For details, refer to the Swagger `accountFilter` field
- [x] Reverse generation of system task information based on job ID or image
- [x] Support for account sorting, concurrency, queue numbers, maximum queue size, task execution intervals, and other configurations
- [x] Support for client path specification, default address example: `https://{BASE_URL}/mj/submit/imagine`, `/mj-turbo/mj` for turbo mode, `/mj-relax/mj` for relax mode, `/mj-fast/mj` for fast mode, `/mj` without specifying the mode
- [x] CloudFlare manual human verification, which triggers automatic account locking, and can be verified directly via the GUI or through email notification verification
- [x] CloudFlare automatic human verification, configure the verification server address (automatic verifier only supports Windows deployment)
- [x] Support for work time period configuration, continuous 24-hour non-stop drawing may trigger warnings. It is recommended to rest for 6-8 hours, example: `09:10-23:55, 13:00-08:10`
- [x] Built-in IP rate limiting, IP segment rate limiting, blacklist, whitelist, automatic blacklisting, and other features

## Online Preview

Public interface is in slow mode, free to use. The account pool is provided by sponsors, so please use it reasonably.

- Admin panel: <https://ai.trueai.org>
- Username & password: `none`
- Public interface: <https://ai.trueai.org/mj>
- API documentation: <https://ai.trueai.org/swagger>
- API key: `none`
- CF automatic verification server address: [http://47.76.110.222:8081](http://47.76.110.222:8081)
- CF automatic verification server documentation: [http://47.76.110.222:8081/swagger](http://47.76.110.222:8081/swagger)

> CF Automatic Verification Configuration Example (Free automatic human verification)

```json
"CaptchaServer": "http://47.76.110.222:8081", 
"CaptchaNotifyHook": "https://ai.trueai.org"  // Notification callback, defaults to the current domain
```

## Preview Screenshots

![Welcome](./docs/screenshots/ui1.png)

![Account](./docs/screenshots/ui2.png)

![Tasks](./docs/screenshots/ui3.png)

![Test](./docs/screenshots/ui4.png)

![Logs](./docs/screenshots/ui5.png)

![API](./docs/screenshots/uiswagger.png)

## Recommended Clients

- **ChatGPT Web Midjourney Proxy**: <https://github.com/Dooy/chatgpt-web-midjourney-proxy> 
  - Visit <https://vercel.ddaiai.com> -> Settings -> MJ Drawing Interface URL -> <https://ai.trueai.org>

- **GoAmzAI**: <https://github.com/Licoy/GoAmzAI>
  - Open backend -> Drawing Management -> Add -> MJ Drawing Interface URL -> <https://ai.trueai.org/mj>

## Installation and Usage

### Quick Start

> Docker version

**Ensure that the mapping files and paths are correct without errors. ⚠⚠**

```bash
# Aliyun image (recommended for use in China)
docker pull registry.cn-guangzhou.aliyuncs.com/trueai-org/midjourney-proxy

# Public demo site startup configuration example

# 1. Download and rename configuration file (example configuration)
wget -O /root/mjopen/appsettings.Production.json https://raw.githubusercontent.com/trueai-org/midjourney-proxy/main/src/Midjourney.API/appsettings.json

# Or use curl to download and rename configuration file (example configuration)
curl -o /root/mjopen/appsettings.Production.json https://raw.githubusercontent.com/trueai-org/midjourney-proxy/main/src/Midjourney.API/appsettings.json

# 2. Stop and remove old Docker containers
docker stop mjopen && docker rm mjopen

# 3. Start a new Docker container
# Please remove the DEMO variable during deployment, otherwise it will enter demonstration mode.
docker run -m 1g --name mjopen -d --restart=always \
 -e DEMO=true \
 -p 8086:8080 --user root \
 -v /root/mjopen/logs:/app/logs:rw \
 -v /root/mjopen/data:/app/data:rw \
 -v /root/mjopen/attachments:/app/wwwroot/attachments:rw \
 -v /root/mjopen/ephemeral-attachments:/app/wwwroot/ephemeral-attachments:rw \
 -v /root/mjopen/appsettings.Production.json:/app/appsettings.Production.json:ro \
 -e TZ=Asia/Shanghai \
 -v /etc/localtime:/etc/localtime:ro \
 -v /etc/timezone:/etc/timezone:ro \
 registry.cn-guangzhou.aliyuncs.com/trueai-org/midjourney-proxy

# Example of Production Environment Startup Configuration
docker run --name mjopen -d --restart=always \
 -p 8086:8080 --user root \
 -v /root/mjopen/logs:/app/logs:rw \
 -v /root/mjopen/data:/app/data:rw \
 -v /root/mjopen/attachments:/app/wwwroot/attachments:rw \
 -v /root/mjopen/ephemeral-attachments:/app/wwwroot/ephemeral-attachments:rw \
 -v /root/mjopen/appsettings.Production.json:/app/appsettings.Production.json:ro \
 -e TZ=Asia/Shanghai \
 -v /etc/localtime:/etc/localtime:ro \
 -v /etc/timezone:/etc/timezone:ro \
 registry.cn-guangzhou.aliyuncs.com/trueai-org/midjourney-proxy

# GitHub mirror
docker pull ghcr.io/trueai-org/midjourney-proxy
docker run --name mjopen -d --restart=always \
 -p 8086:8080 --user root \
 -v /root/mjopen/logs:/app/logs:rw \
 -v /root/mjopen/data:/app/data:rw \
 -v /root/mjopen/attachments:/app/wwwroot/attachments:rw \
 -v /root/mjopen/ephemeral-attachments:/app/wwwroot/ephemeral-attachments:rw \
 -v /root/mjopen/appsettings.Production.json:/app/appsettings.Production.json:ro \
 -e TZ=Asia/Shanghai \
 -v /etc/localtime:/etc/localtime:ro \
 -v /etc/timezone:/etc/timezone:ro \
 ghcr.io/trueai-org/midjourney-proxy

# DockerHub mirror
docker pull trueaiorg/midjourney-proxy
docker run --name mjopen -d --restart=always \
 -p 8086:8080 --user root \
 -v /root/mjopen/logs:/app/logs:rw \
 -v /root/mjopen/data:/app/data:rw \
 -v /root/mjopen/attachments:/app/wwwroot/attachments:rw \
 -v /root/mjopen/ephemeral-attachments:/app/wwwroot/ephemeral-attachments:rw \
 -v /root/mjopen/appsettings.Production.json:/app/appsettings.Production.json:ro \
 -e TZ=Asia/Shanghai \
 -v /etc/localtime:/etc/localtime:ro \
 -v /etc/timezone:/etc/timezone:ro \
 trueaiorg/midjourney-proxy
```

> Windows version

```bash
a. Download the latest no-install version for Windows from https://github.com/trueai-org/midjourney-proxy/releases, e.g., midjourney-proxy-win-x64.zip
b. Unzip and run Midjourney.API.exe
c. Open http://localhost:8080
d. Optional: Deploy to IIS, add the site to IIS, deploy the folder to IIS, configure the application pool to `No Managed Code`, and start the site.
e. Optional: Use the built-in `Task Scheduler` to create a basic task, select the `.exe` program, and ensure `Do not start multiple instances` is selected to keep only one task running.
```

> Linux version

```bash
a. Download the latest no-install version for Linux from https://github.com/trueai-org/midjourney-proxy/releases, e.g., midjourney-proxy-linux-x64.zip
b. Extract to the current directory: tar -xzf midjourney-proxy-linux-x64-<VERSION>.tar.gz
c. Execute: run_app.sh
c. Startup method 1: sh run_app.sh
d. Startup method 2: chmod +x run_app.sh && ./run_app.sh
```

> macOS version

```bash
a. Download the latest no-install version for macOS from https://github.com/trueai-org/midjourney-proxy/releases, e.g., midjourney-proxy-osx-x64.zip
b. Extract to the current directory: tar -xzf midjourney-proxy-osx-x64-<VERSION>.tar.gz
c. Execute: run_app_osx.sh
c. Startup method 1: sh run_app_osx.sh
d. Startup method 2: chmod +x run_app_osx.sh && ./run_app_osx.sh
```

> Installation Script for Linux（❤Thanks to [@dbccccccc](https://github.com/dbccccccc)）

```bash
# Method 1
wget -N --no-check-certificate https://raw.githubusercontent.com/trueai-org/midjourney-proxy/main/scripts/linux_install.sh && chmod +x linux_install.sh && bash linux_install.sh

# Method 2
curl -o linux_install.sh https://raw.githubusercontent.com/trueai-org/midjourney-proxy/main/scripts/linux_install.sh && chmod +x linux_install.sh && bash linux_install.sh
```

## Configuration Parameters

- `appsettings.json` default configuration
- `appsettings.Production.json` production environment configuration
- `/app/data` data directory, stores accounts, tasks, etc.
    - `/app/data/mj.db` database file
- `/app/logs` log directory
- `/app/wwwroot` Static Files Directory
    - `/app/wwwroot/attachments` image Files Directory
    - `/app/wwwroot/ephemeral-attachments` describe spanwned image Files Directory

```json
{
  "Demo": null, // Set the website to demo mode
  "UserToken": "", // User drawing token, can access drawing interface, optional
  "AdminToken": "", // Admin backend token, can access drawing interface and admin accounts
  "mj": {
    "AccountChooseRule": "BestWaitIdle", // BestWaitIdle | Random | Weight | Polling = Best wait idle rule | Random | Weight | Polling
    "Discord": { // Discord settings, default can be null
      "GuildId": "125652671***", // Server ID
      "ChannelId": "12565267***", // Channel ID
      "PrivateChannelId": "1256495659***", // MJ private channel ID, for receiving seed values
      "NijiBotChannelId": "1261608644***", // NIJI private channel ID, for receiving seed values
      "UserToken": "MTI1NjQ5N***", // User token
      "BotToken": "MTI1NjUyODEy***", // Bot token
      "UserAgent": "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36",
      "Enable": true, // Whether to enable by default
      "CoreSize": 3, // Concurrent number
      "QueueSize": 10, // Queue size
      "MaxQueueSize": 100, // Maximum queue size
      "TimeoutMinutes": 5, // Task timeout in minutes
      "Mode": null, // RELAX | FAST | TURBO specify generation speed mode --fast, --relax, or --turbo parameter at the end.
      "Weight": 1 // Weight
    },
    "NgDiscord": { // NG Discord configuration, default can be null
      "Server": "",
      "Cdn": "",
      "Wss": "",
      "ResumeWss": "",
      "UploadServer": "",
      "SaveToLocal": false, // Whether to save images locally, if enabled, use the address of the local deployment, you can also configure the CDN address at the same time
      "CustomCdn": "" // If not filled in, and local saving is enabled, it defaults to the root directory, it is recommended to fill in your own domain name address
    },
    "Proxy": { // Proxy configuration, default can be null
      "Host": "",
      "Port": 10809
    },
    "Accounts": [], // Account pool configuration
    "BaiduTranslate": { // Baidu Translate configuration, default can be null
      "Appid": "", // your_appid
      "AppSecret": "" // your_app_secret
    },
    "TranslateWay": "NULL", // NULL | GTP | BAIDU, translation configuration, default: NULL
    "ApiSecret": "", // your_api_secret
    "NotifyHook": "", // your_notify_hook, callback configuration
    "NotifyPoolSize": 10,
    "Smtp": {
      "Host": "smtp.mxhichina.com", // SMTP server information
      "Port": 465, // SMTP port, generally 587 or 465, depending on your SMTP server
      "EnableSsl": true, // Set according to your SMTP server requirements
      "FromName": "system", // Sender nickname
      "FromEmail": "system@***.org", // Sender email address
      "FromPassword": "", // Your email password or application-specific password
      "To": "" // Recipient
    },
    "CaptchaServer": "", // CF verification server address
    "CaptchaNotifyHook": "" // CF verification notification address (callback notification after verification passes, default is your current domain name)
  },
  // IP/IP segment rate limiting configuration, can be used to limit the access frequency of a certain IP/IP segment
  // After triggering rate limiting, a 429 status code will be returned
  // Blacklist directly returns a 403 status code
  // Blacklist and whitelist support IP and CIDR format IP segments, for example: 192.168.1.100, 192.168.1.0/24
  "IpRateLimiting": {
    "Enable": false,
    "Whitelist": [], // Permanent whitelist "127.0.0.1", "::1/10", "::1"
    "Blacklist": [], // Permanent blacklist
    "IpRules": {
      // Limit all interfaces under the mj/submit interface
      "*/mj/submit/*": {
        "3": 1, // At most 1 access per 3 seconds
        "60": 6, // At most 6 accesses per 60 seconds
        "600": 20, // At most 20 accesses per 600 seconds
        "3600": 60, // At most 60 accesses per 3600 seconds
        "86400": 120 // At most 120 accesses per day
      }
    },
    "IpRangeRules": {
      // Limit all interfaces under the mj/submit interface
      "*/mj/submit/*": {
        "5": 10, // At most 10 accesses per 5 seconds
        "60": 30, // At most 30 accesses per 60 seconds
        "600": 100, // At most 100 accesses per 600 seconds
        "3600": 300, // At most 300 accesses per 3600 seconds
        "86400": 360 // At most 360 accesses per day
      }
    }
  },
  // IP blacklist rate limiting configuration, after triggering, the IP will be automatically blocked, support block time configuration
  // After triggering rate limiting, joining the blacklist will return a 403 status code
  // Blacklist and whitelist support IP and CIDR format IP segments, for example: 192.168.1.100, 192.168.1.0/24
  "IpBlackRateLimiting": {
    "Enable": false,
    "Whitelist": [], // Permanent whitelist "127.0.0.1", "::1/10", "::1"
    "Blacklist": [], // Permanent blacklist
    "BlockTime": 1440, // Block time, unit: minutes
    "IpRules": {
      "*/mj/*": {
        "1": 30,
        "60": 900
      }
    },
    "IpRangeRules": {
      "*/mj/*": {
        "1": 90,
        "60": 3000
      }
    }
  },
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Default": "Warning",
        "System": "Warning",
        "Microsoft": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "logs/log.txt",
          "rollingInterval": "Day",
          "fileSizeLimitBytes": null,
          "rollOnFileSizeLimit": false,
          "retainedFileCountLimit": 31
        }
      },
      {
        "Name": "Console"
      }
    ]
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "urls": "http://*:8080" // Default port
}
```

## CloudFlare Validator Deployment
 
Windows deployment supportedonly (and supports TLS 1.3, system requirements are Windows 11 or Windows Server 2022). Since the CloudFlare validator requires the use of the Chrome browser, it needs to be deployed in a Windows environment. Deployment in a Linux environment would depend on many libraries, so Linux deployment is temporarily not supported.

Note: Self-deployment requires providing the API Key for 2captcha.com, otherwise it cannot be used. Price: 1000 times/9 yuan, official website: <https://2captcha.cn/p/cloudflare-turnstile>
 
Tip: The first startup will download the Chrome browser, which will be slow, so please be patient.
 
> `appsettings.json` configuration reference
 
```json
{
  "Demo": null, // Website configuration in demo mode
  "Captcha": {
    "Headless": true, // Whether chrome runs in the background
    "TwoCaptchaKey": "" // API Key for 2captcha.com
  },
  "urls": "http://*:8081" // Default port
}

```

## Bot Token (Required)

This project uses a Discord bot token to connect via wss, retrieving error messages and full functionality, ensuring high availability of messages.

```
1. Create an application
https://discord.com/developers/applications

2. Set application permissions (ensure read content permission, refer to screenshot)
[Bot] Settings -> Enable all

3. Add application to channel server (refer to screenshot)

client_id can be found on the application details page, it is the APPLICATION ID

https://discord.com/oauth2/authorize?client_id=xxx&permissions=8&scope=bot

4. Copy or reset Bot Token to configuration file
```

Set application permissions (ensure read content permission, refer to screenshot)

![Set application permissions](./docs/screenshots/gjODn5Nplq.png)

Add application to channel server (refer to screenshot)

![Add application to channel server](./docs/screenshots/ItiWgaWIaX.png)

## Related Documentation
1. [API Documentation](./docs/api.md)

## Roadmap

- [ ] Publish a video tutorial
- [ ] Make bot token an optional configuration, usable without configuration
- [ ] Optimize commands and progress display
- [ ] Optimize reminders when tasks and queues are full
- [ ] Optimize potential issues with concurrent queues for shared accounts
- [ ] Built-in vocabulary management, batch modification
- [ ] Integrate official drawing API support
- [ ] Add statistics dashboard, drawing statistics, visitor statistics
- [ ] Built-in user system, registerable and manageable, rate limiting, maximum times, etc.
- [ ] Integrate GPT translation
- [ ] Add translation support for final prompts in Chinese
- [ ] Support separate proxies for accounts
- [ ] Enable registration, enable guest access
- [ ] Multi-database support: MySQL, Sqlite, SqlServer, MongoDB, PostgeSQL, Redis, etc.
- [ ] Payment integration support, support WeChat, Alipay, support drawing pricing strategies, etc.
- [ ] Add announcement function
- [ ] Add idle/relaxed mode for accounts to avoid high-frequency operations (In this mode, new drawings cannot be created, but other commands can still be executed. It can be configured for multiple time periods or scheduled strategies)
- [ ] Handle seed values for text-to-image
- [ ] Automatically read private messages
- [ ] After service restart, if there are unfinished tasks, add them to the execution queue
- [ ] Allow shared channels or sub-channels for drawing, even if the account is banned, the previous drawing can continue. The banned account's channel can be used as a sub-channel for normal accounts, save permanent invitation links, and sub-channel links, support batch modification, can directly input invitation links, or shared channel addresses, the system automatically joins the channel conversion. Or achieve this by transferring ownership.

## Support and Sponsorship

- If you find this project helpful, please give it a Star⭐
- You can also provide temporarily idle drawing accounts for public benefit (sponsor slow mode) to support the development of this project😀