FROM verdaccio/verdaccio:6
ADD docker.yaml /verdaccio/conf/config.yaml
USER root
RUN npm install --global verdaccio-static-token \
  && npm install --global verdaccio-aws-s3-storage \
  && npm install --global verdaccio-minio
USER $VERDACCIO_USER_UID