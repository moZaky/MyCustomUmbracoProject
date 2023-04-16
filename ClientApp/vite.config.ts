
/// <reference types="node" />

import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import path from 'path';

const root = path.join(__dirname, './');
const main = path.resolve(__dirname, 'src', 'main.tsx');
const outDir = path.join(root, "dist/umbraco-app");
console.log('root', root);
console.log('main', main);
console.log('outDir', outDir);
 
// https://vitejs.dev/config/
export default defineConfig(({ command, mode }) => ({
  plugins: [react()],
  publicDir: command === 'build' ? false : 'src/assets',
  // base: command === 'build' ? '/etc.clientlibs/<project>/clientlibs/' : '/',

  root: root,
  resolve: {
    extensions: ['.js', '.jsx', '.tsx', '.ts'],
  },
  server: {
    port: 3000
  },

  build: {
      //outDir: './dist/umbraco-app',
      outDir: outDir,
    //minify: mode === 'development' ? false : 'terser',
    brotliSize: false,
    manifest: false,
    sourcemap: command === 'serve' ? 'inline' : false,
    rollupOptions: {
      input: {
        main: main
      },
      output: {
        // assetFileNames: '[ext]/[name][extname]',
        assetFileNames: 'assets/[name][extname]',
        chunkFileNames: 'chunks/[name].[hash].js',
        entryFileNames: 'js/[name].js',
      },
    },
  },

}));
