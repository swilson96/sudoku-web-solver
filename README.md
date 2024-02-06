sudoku-web-solver
=================

A website which solves sudoku. Currenty deployed at www.swilson.co.uk/sudoku

The first (non-coursework) program I wrote many years ago was a sudoku solver, and I've converted it into a web-app and put it online, just for fun.

It was originally in C, then shortly afterwards converted to a C# desktop app. This project is to incorporate it in a C# MVC.NET website. That worked for a while, although deploying .net on heroku or other free providers was tricky and I didn't really get it working reliably. I've now converted it to a static react SPA to deply on github pages. At each stage I haven't bothered to improve or update the code, I've just done the minimum to port it to the new language/framework. Oddly, it has improved a bit as a result.

Note it will run locally at http://localhost:3000/sudoku-web-solver

The interesting sudoku bit (also the oldest bit I first wrote as a student) is [src/app/logic/solver.ts](src/app/logic/solver.ts)

---

Here's the generated Next.js readme stuff:

This is a [Next.js](https://nextjs.org/) project bootstrapped with [`create-next-app`](https://github.com/vercel/next.js/tree/canary/packages/create-next-app).

## Getting Started

First, run the development server:

```bash
npm run dev
# or
yarn dev
# or
pnpm dev
# or
bun dev
```

Open [http://localhost:3000/sudoku-web-solver](http://localhost:3000/sudoku-web-solver) with your browser to see the result.

You can start editing the page by modifying `app/page.tsx`. The page auto-updates as you edit the file.

This project uses [`next/font`](https://nextjs.org/docs/basic-features/font-optimization) to automatically optimize and load Inter, a custom Google Font.

## Learn More

To learn more about Next.js, take a look at the following resources:

- [Next.js Documentation](https://nextjs.org/docs) - learn about Next.js features and API.
- [Learn Next.js](https://nextjs.org/learn) - an interactive Next.js tutorial.

You can check out [the Next.js GitHub repository](https://github.com/vercel/next.js/) - your feedback and contributions are welcome!

## Deploy on Vercel

The easiest way to deploy your Next.js app is to use the [Vercel Platform](https://vercel.com/new?utm_medium=default-template&filter=next.js&utm_source=create-next-app&utm_campaign=create-next-app-readme) from the creators of Next.js.

Check out our [Next.js deployment documentation](https://nextjs.org/docs/deployment) for more details.
