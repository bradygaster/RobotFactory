docker build --rm -f "../src/RepairBot/Dockerfile" -t $registry/repairbot:latest ../src/RepairBot
docker push $registry/repairbot:latest
kubectl apply -f ../src/RepairBot/k8s.yaml
kubectl delete pod -l app=repairbot