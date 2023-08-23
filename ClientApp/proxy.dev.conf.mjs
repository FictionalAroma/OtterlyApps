export default [
  {
    context: [
        '/bff'
        ],
    target: 'https://localhost:7161',
    secure: true,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
];
