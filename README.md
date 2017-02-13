# uritester

cd into the application 
docker build -t dotnetapp .
docker run "STATSDSERVER=server" -it -p 5001:80 --rm dotnetapp

