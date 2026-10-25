import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  server: {
    allowedHosts: true,
    proxy: {
      '/api': {
        target: 'https://localhost:7078', // confira a porta no launchSettings.json
        changeOrigin: true,
        secure: false, // aceita o certificado de desenvolvimento do ASP.NET
      },
    },
  },
})