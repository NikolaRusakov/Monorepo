module.exports = {
  petstore: {
    output: {
      mode: 'tags-split',
      target: 'src/lib/api/api.ts',
      schemas: 'src/lib/model',
      client: 'angular',
      useInfiniteQueryParam: 'id',
      mock: { indexMockFiles: true },
    },
    input: {
      target: './schema.json',
      // override: {
      //   transformer: './src/lib/orval/add-version.js',
      // },
    },
  },
};
