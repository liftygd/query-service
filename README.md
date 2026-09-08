## Развертывание приложения

### Требования
- Docker
- Docker Compose

### Этапы развертывания

1. **Клонируйте репозиторий**
   ```sh
   git clone https://github.com/liftygd/query-service.git
   cd query-service
   ```

2. **Запустите Docker Compose**
   ```sh
   docker-compose build
   docker-compose up -d
   ```

3. **Проверьте статус сервисов**
   ```sh
   docker-compose ps
   ```

4. **Остановите приложение**
   ```sh
   docker-compose down
   ```

5. Доступ:
   ```sh
   Сервис: localhost:5000
   Postgres: localhost:5435
   Swagger: localhost:5000/swagger/index.html
   ```
