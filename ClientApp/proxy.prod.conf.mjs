export default [
  {
    context: [
        '/bff'
        ],
    target: 'https://api.otterlystudios.net/',
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    },
    "changeOrigin": true,
  }
];
