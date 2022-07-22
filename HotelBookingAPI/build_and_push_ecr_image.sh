#! /bin/bash
set -e

aws ecr get-login-password --region us-west-2 --profile hotel-api-agent | docker login --username AWS --pasword-stdin 314113446773.dkr.ecr.us-west-2.amazonaws.com
docker build -f ./Dokerfile -t hotel-api:latest .
docker tag hotel-api:latest 314113446773.dkr.ecr.us-west-2.amazonaws.com/hotel-booking-api:latest
docker push 314113446773.dkr.ecr.us-west-2.amazonaws.com/hotel-booking-api:latest

