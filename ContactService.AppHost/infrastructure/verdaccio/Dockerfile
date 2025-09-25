# TODO: Wait until outage is resolved, https://www.dockerstatus.com/pages/533c6539221ae15e3f000031
FROM public.ecr.aws/docplanner/verdaccio:5.8.0
ADD docker.yaml /verdaccio/conf/config.yaml
USER root
RUN npm install --global verdaccio-static-token \
  && npm install --global verdaccio-aws-s3-storage \
  && npm install --global verdaccio-minio
USER $VERDACCIO_USER_UID