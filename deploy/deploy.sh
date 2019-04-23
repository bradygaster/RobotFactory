dockerHubAccount=bradygaster

docker build --rm -f "../src/Dashboard/Dockerfile" -t $dockerHubAccount/dashboard:latest ../src/Dashboard
docker build --rm -f "../src/ChargeBot/Dockerfile" -t $dockerHubAccount/chargebot:latest ../src/ChargeBot
docker build --rm -f "../src/MonitorBot/Dockerfile" -t $dockerHubAccount/monitorbot:latest ../src/MonitorBot
docker build --rm -f "../src/RepairBot/Dockerfile" -t $dockerHubAccount/repairbot:latest ../src/RepairBot
docker build --rm -f "../src/UniBot/Dockerfile" -t $dockerHubAccount/unibot:latest ../src/UniBot

#docker push $dockerHubAccount/dashboard:latest
#docker push $dockerHubAccount/chargebot:latest
#docker push $dockerHubAccount/monitorbot:latest
#docker push $dockerHubAccount/repairbot:latest
#docker push $dockerHubAccount/unibot:latest

#kubectl apply -f ../src/Dashboard/k8s.yaml
#kubectl apply -f ../src/ChargeBot/k8s.yaml
#kubectl apply -f ../src/MonitorBot/k8s.yaml
#kubectl apply -f ../src/RepairBot/k8s.yaml
#kubectl apply -f ../src/UniBot/k8s.yaml