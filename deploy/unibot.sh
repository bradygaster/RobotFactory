docker build --rm -f "../src/UniBot/Dockerfile" -t $registry/unibot:latest ../src/UniBot
docker push $registry/unibot:latest
kubectl apply -f ../src/UniBot/k8s.yaml
kubectl delete pod -l app=unibot