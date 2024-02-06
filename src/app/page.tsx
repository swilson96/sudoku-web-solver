import Solver from './components/solver'

export default function Home() {
  return (
    <main className="flex min-h-screen flex-col items-center justify-between p-24">
      <div className="z-10 max-w-5xl w-full items-center justify-between font-mono text-sm lg:flex">
        <p>Simon&apos;s Sudoku Solver</p>
      </div>

      <div className="">
        <Solver />
      </div>
    </main>
  )
}
