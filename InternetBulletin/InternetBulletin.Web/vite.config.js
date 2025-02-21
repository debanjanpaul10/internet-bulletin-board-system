import { defineConfig } from "vite";
import basicSsl from "@vitejs/plugin-basic-ssl";
import react from "@vitejs/plugin-react";
import path from "path";

export default defineConfig(() => {
	return {
		plugins: [react(), basicSsl()],
		build: {
			target: "esnext",
			minify: "terser",
			lib: {
				entry: "React/index.jsx",
				formats: ["es"],
			},
			rollupOptions: {
				external: ["react", "react-dom"],
				output: {
					globals: {
						react: "React",
						"react-dom": "ReactDOM",
					},
				},
			},
		},
		optimizeDeps: {
			include: ["has-hover"],
			esbuildOptions: {
				loader: {
					".js": "jsx",
					".css": "css",
				},
			},
		},
		resolve: {
			alias: {
				"@": path.resolve(__dirname, "./React"),
				"@components": path.resolve("./React/Components"),
				"@helpers": path.resolve("./React/Helpers"),
				"@store": path.resolve("./React/Store"),
				"@assets": path.resolve("./React/Assets"),
			},
		},
		server: {
			https: true,
			port: 6970,
		},
        define: {
            'global': "window"
        }
	};
});
