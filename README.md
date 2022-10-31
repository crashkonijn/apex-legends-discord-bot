`docker build -t webapi .`

```yaml
---
version: "3"
services:
  apex-bot:
    image: webapi
    container_name: apex-bot
    environment:
      - ApplicationModule__ConfigLocation=../config
      - DiscordModule__Token=discord_token
      - DiscordModule__GuildId=discord_guild_id
      - DiscordModule__ChannelName=general
      - TrackerModule__Token=tracker_gg_token
    volumes:
      - ./config:/config
    ports:
      - 5000:80
    restart: unless-stopped
```