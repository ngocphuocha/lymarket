# Ly Market API
Chạy Docker Compose với biến môi trường tương ứng

Create ENV file from .env.example
```
cp .env.example .env.dev && cp .env.example .env.prod
```

For development
```
docker compose --env-file .env.development up
```

For production
```
docker compose --env-file .env.production up
```
