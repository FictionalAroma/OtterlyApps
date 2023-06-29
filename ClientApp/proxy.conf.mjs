export default [
  {
    context: [
        '/bff'
        ],
    target: 'https://localhost:7161',
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
];
