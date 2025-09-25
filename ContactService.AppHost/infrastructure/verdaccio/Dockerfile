# TODO: Wait until outage is resolved, https://www.dockerstatus.com/pages/533c6539221ae15e3f000031
FROM public.ecr.aws/docplanner/verdaccio:5.8.0

COPY ./conf/ /verdaccio/conf
RUN mkdir -p /verdaccio/plugins
# ADD docker.yaml /verdaccio/conf/config.yaml
ADD ./conf/config.yaml /verdaccio/conf/config.yaml
USER root
RUN npm install --global verdaccio-minio
# RUN npm install verdaccio-minio  
RUN ln -s /usr/local/lib/node_modules/verdaccio-minio /verdaccio/plugins/verdaccio-minio
# RUN ln -s /usr/local/share/.config/yarn/global/node_modules/verdaccio-minio /verdaccio/plugins/verdaccio-minio
USER $VERDACCIO_USER_UID