docker build --rm -f "../src/Dashboard/Dockerfile" -t $registry/dashboard:latest ../src/Dashboard
docker push $registry/dashboard:latest
kubectl apply -f ../src/Dashboard/k8s.yaml
kubectl delete pod -l app=dashboard