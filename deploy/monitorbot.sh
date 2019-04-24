docker build --rm -f "../src/MonitorBot/Dockerfile" -t $registry/monitorbot:latest ../src/MonitorBot
docker push $registry/monitorbot:latest
kubectl apply -f ../src/MonitorBot/k8s.yaml
kubectl delete pod -l app=monitorbot