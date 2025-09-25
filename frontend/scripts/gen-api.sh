wget --no-check-certificate -O - -o /dev/null https://localhost:7290/openapi/v1.json \
> libs/contact-service/schema.json \
&& npx orval --config libs/contact-service/orval.config.js  \
&& npx barrelsby --directory libs/contact-service/src/lib