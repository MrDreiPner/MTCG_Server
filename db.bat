docker stop MTCG_DB 
docker rm MTCG_DB 
docker run -d --name MTCG_DB -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=Aurora -p 10002:10002 postgres

timeout /t 5 /nobreak

docker exec -i MTCG_DB createdb -U postgres MTCG_DB